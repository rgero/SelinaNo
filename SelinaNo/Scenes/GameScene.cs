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
using SelinaNo.Entities;

namespace SelinaNo.Scenes
{
    class GameScene
    {
        SelinaNoGame parent;

        //Declare the managers
        BeckyManager beckyManager;
        ProjectileManager projectileManager;

        //  Declaring Selina texture and rectangle
        Texture2D selinaSprite;
        static Rectangle selinaRect;

        //Declaring the ONE random that will be used the entire game.
        Random rand;

        //Declaring the sound effects
        SoundEffect yumSound;
        SoundEffect hitSound;

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


        public GameScene(SelinaNoGame game) {
            parent = game;
            beckyManager = new BeckyManager(this);
            projectileManager = new ProjectileManager(this);
            rand = new Random();
        }

        public void LoadContent(ContentManager Content)
        {

            beckyManager.LoadContent(Content);

            //Loading the Selina Sprite
            selinaSprite = Content.Load<Texture2D>(@"Sprites\Selina");

            //Loading the sound effects
            hitSound = Content.Load<SoundEffect>(@"Sounds\Explosion");
            yumSound = Content.Load<SoundEffect>(@"Sounds\Yum");
            
            //Positioning stuff. This will be useful a lot.
            int halfSelinaX = selinaSprite.Width / 2;
            int halfSelinaY = selinaSprite.Height / 2;

            //Using Resize factor so we can play with the size of Selina.
            selinaRect = new Rectangle(GameConstants.SCREEN_WIDTH / 2 - halfSelinaX / GameConstants.RESIZE_FACTOR,
                                       GameConstants.SCREEN_HEIGHT / 2 - halfSelinaY / GameConstants.RESIZE_FACTOR,
                                       selinaSprite.Width / GameConstants.RESIZE_FACTOR,
                                       selinaSprite.Height / GameConstants.RESIZE_FACTOR);



        }

        public void Update(GameTime gameTime)
        {
            if (selinaAlive)
            {

                #region Selina Movement


                // Selina Update Position based on mouse location
                if (parent.getControls() == ControlScheme.Mouse)
                {
                    MouseState mouse = Mouse.GetState();
                    selinaRect.X = mouse.X - (selinaSprite.Width / (2 * GameConstants.RESIZE_FACTOR));
                    selinaRect.Y = mouse.Y - (selinaSprite.Height / (2 * GameConstants.RESIZE_FACTOR));
                }
                else if (parent.getControls() == ControlScheme.Keyboard)
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

                beckyManager.Update(gameTime);


                //Handling Collisions
                foreach (Becky bex in beckyManager.getBexList() )
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

            projectileManager.Update(gameTime);

            //Handling Collisions
            foreach (Projectile projectile in projectileManager.getProjectiles() )
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

        public void checkDeath()
        {
            if (Health <= 0)
            {
                SelinaNoGame.setScore(score);
                parent.setState(GameState.LostNoHigh);
            }

        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont scoreboardFont)
        {

            beckyManager.Draw(spriteBatch);
            projectileManager.Draw(spriteBatch);

            //Drawing Selina
            spriteBatch.Draw(selinaSprite, selinaRect, Color.White);

            //Drawing the ScoreBoard and Health
            spriteBatch.DrawString(scoreboardFont, scoreMessage, scoreboardLoc, Color.White);
            spriteBatch.DrawString(scoreboardFont, healthMessage, healthLoc, Color.White);
        }

        public BeckyManager getBeckyManager()
        {
            return beckyManager;
        }

        public ProjectileManager getProjectileManager()
        {
            return projectileManager;
        }

        public Random getRandom()
        {
            return rand;
        }



    }
}
