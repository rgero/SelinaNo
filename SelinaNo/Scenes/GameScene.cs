using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace SelinaNo.Scenes
{
    class GameScene
    {
        //  Declaring Selina texture and rectangle
        Texture2D selinaSprite;
        static Rectangle selinaRect;

        // Declaring Bex's texture
        Texture2D bexSprite;

        //Declaring Laser Sprite
        public static Texture2D laserSprite;

        //Declaring the List of Bex
        List<Becky> bexList = new List<Becky>();
        static List<Projectile> projectileList = new List<Projectile>();

        //Declaring the ONE random that will be used the entire game.
        Random rand = new Random();

        //Declaring the sound effects
        SoundEffect merhSound;
        SoundEffect yumSound;
        SoundEffect hitSound;

        //Declaring Scoreboard Font

        Vector2 scoreboardLoc = new Vector2(GameConstants.SCOREBOARD_X, GameConstants.SCOREBOARD_Y);
        Vector2 healthLoc = new Vector2(GameConstants.HEALTH_X, GameConstants.HEALTH_Y);
        Vector2 controlDisplay = new Vector2(GameConstants.CONTROL_X, GameConstants.CONTROL_Y);

        //Declaring Scoreboard
        static int score = 0;
        string scoreMessage = GameConstants.SCORE_PREFIX + score.ToString();

        //Declaring the Health
        static int health = GameConstants.INITIAL_HEALTH;
        string healthMessage = GameConstants.HEALTH_PREFIX + health.ToString();
        bool selinaAlive = true;

        public static int Health
        {
            get { return health; }
            set
            {
                health = value;
                if (health < 0)
                {
                    health = 0;
                }
                if (health > GameConstants.INITIAL_HEALTH)
                {
                    health = GameConstants.INITIAL_HEALTH;
                }
            }
        }

        public static Rectangle SelinaHitBox
        {
            get { return selinaRect; }
        }


        //CONSTRUCTOR
        public GameScene() { }

        public void LoadContent(ContentManager Content)
        {
            //Loading the Selina Sprite
            selinaSprite = Content.Load<Texture2D>(@"Sprites\Selina");

            //Loading Laser Sprite
            laserSprite = Content.Load<Texture2D>(@"Sprites\laser");



            //Loading the sound effects
            merhSound = Content.Load<SoundEffect>(@"Sounds\Merh");
            yumSound = Content.Load<SoundEffect>(@"Sounds\Yum");
            hitSound = Content.Load<SoundEffect>(@"Sounds\Explosion");
            SoundEffect.MasterVolume = 0.5f;

            //Positioning stuff. This will be useful a lot.
            int halfSelinaX = selinaSprite.Width / 2;
            int halfSelinaY = selinaSprite.Height / 2;

            //Using Resize factor so we can play with the size of Selina.
            selinaRect = new Rectangle(GameConstants.SCREEN_WIDTH / 2 - halfSelinaX / GameConstants.RESIZE_FACTOR,
                                       GameConstants.SCREEN_HEIGHT / 2 - halfSelinaY / GameConstants.RESIZE_FACTOR,
                                       selinaSprite.Width / GameConstants.RESIZE_FACTOR,
                                       selinaSprite.Height / GameConstants.RESIZE_FACTOR);

            //Loading the Bex Sprite
            bexSprite = Content.Load<Texture2D>(@"Sprites\Bex");

        }

        public void Update(GameTime gameTime)
        {
            if (selinaAlive)
            {

                #region Selina Movement


                // Selina Update Position based on mouse location
                if (SelinaNoGame.currentControls == ControlScheme.Mouse)
                {
                    MouseState mouse = Mouse.GetState();
                    selinaRect.X = mouse.X - (selinaSprite.Width / (2 * GameConstants.RESIZE_FACTOR));
                    selinaRect.Y = mouse.Y - (selinaSprite.Height / (2 * GameConstants.RESIZE_FACTOR));
                }
                else if (SelinaNoGame.currentControls == ControlScheme.Keyboard)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.W))
                    {
                        selinaRect.Y -= GameConstants.MAX_SELINA_SPEED;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.S))
                    {
                        selinaRect.Y += GameConstants.MAX_SELINA_SPEED;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                        selinaRect.X -= GameConstants.MAX_SELINA_SPEED;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.D))
                    {
                        selinaRect.X += GameConstants.MAX_SELINA_SPEED;
                    }
                }

                //Clamping Selina to the Screen
                if (selinaRect.Left < 0)
                {
                    selinaRect.X = 0;
                }
                if (selinaRect.Top < 0)
                {
                    selinaRect.Y = 0;
                }
                if (selinaRect.Right > GameConstants.SCREEN_WIDTH)
                {
                    selinaRect.X = GameConstants.SCREEN_WIDTH - selinaSprite.Width / GameConstants.RESIZE_FACTOR;
                }
                if (selinaRect.Bottom > GameConstants.SCREEN_HEIGHT)
                {
                    selinaRect.Y = GameConstants.SCREEN_HEIGHT - selinaSprite.Height / GameConstants.RESIZE_FACTOR;
                }

                #endregion

                //Handling Collisions
                foreach (Becky bex in bexList)
                {
                    if (selinaRect.Contains(bex.CollisionRectangle))
                    {
                        yumSound.Play();
                        bex.Active = false;
                        score += 10;
                        scoreMessage = GameConstants.SCORE_PREFIX + score.ToString();
                    }

                }

            }

            #region Code Related to Becky

            //Adding new Becky
            if (bexList.Count < 10)
            {
                spawnBex();
            }

            //Removing Dead Beckys
            for (int i = bexList.Count - 1; i >= 0; i--)
            {
                if (!bexList[i].Active)
                {
                    bexList.RemoveAt(i);
                }
            }

            foreach (Becky bex in bexList)
            {
                bex.Update(gameTime);
            }

            #endregion

            foreach (Projectile projectile in projectileList)
            {
                projectile.Update(gameTime);
            }

            //Removing Dead Projectiles
            for (int k = projectileList.Count - 1; k >= 0; k--)
            {
                if (!projectileList[k].Active)
                {
                    projectileList.RemoveAt(k);
                }
            }

            //Handling Collisions
            foreach (Projectile projectile in projectileList)
            {
                if (selinaRect.Contains(projectile.CollisionRectangle))
                {
                    hitSound.Play();
                    projectile.Active = false;
                    Health -= 5;
                    healthMessage = GameConstants.HEALTH_PREFIX + Health.ToString();
                    checkDeath();
                }

            }
        }

        private void spawnBex()
        {
            int BexX = rand.Next(0, GameConstants.SCREEN_WIDTH - bexSprite.Width);
            int BexY = rand.Next(0, GameConstants.SCREEN_HEIGHT - bexSprite.Height);
            Becky Bex = new Becky(bexSprite, BexX, BexY, rand, merhSound);
            bexList.Add(Bex);
        }

        public static void addProjectile(Projectile projectile)
        {
            projectileList.Add(projectile);
        }

        public void checkDeath()
        {
            if (Health <= 0)
            {
                SelinaNoGame.setScore(score);
                SelinaNoGame.currentState = GameState.LostNoHigh;
            }

        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont scoreboardFont)
        {

            //Drawing the Becky's
            foreach (Becky bex in bexList)
            {
                bex.Draw(spriteBatch);
            }

            //Drawing the Projectiles
            foreach (Projectile projectile in projectileList)
            {
                projectile.Draw(spriteBatch);

            }

            //Drawing Selina
            spriteBatch.Draw(selinaSprite, selinaRect, Color.White);

            //Drawing the ScoreBoard and Health
            spriteBatch.DrawString(scoreboardFont, scoreMessage, scoreboardLoc, Color.White);
            spriteBatch.DrawString(scoreboardFont, healthMessage, healthLoc, Color.White);
        }



    }
}
