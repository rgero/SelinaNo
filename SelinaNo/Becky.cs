using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SelinaNo
{
    class Becky
    {

        Texture2D sprite;
        Rectangle drawRectangle;
        Random rand;
        bool alive = true;
        int counter = 0;
        int goalTime;
        float velocityX;
        float velocityY;
        SoundEffect soundeffect;

        public bool Active
        {
            get { return alive; }
            set { alive = value; }
        }

        public Rectangle CollisionRectangle
        {
            get { return drawRectangle; }
        }

        public Rectangle DrawRectangle
        {
            get { return drawRectangle; }
        }

        public Vector2 Velocity
        {
            get { return new Vector2(velocityX, velocityY); }
            set { velocityX = value.X; velocityY = value.Y; }
        }

        public Becky(Texture2D sprite, int x, int y, Random rand, SoundEffect sound)
        {
            this.soundeffect = sound;
            this.sprite = sprite;
            drawRectangle = new Rectangle(x, y, sprite.Width, sprite.Height);
            this.rand = rand;

            velocityX = (float)(rand.NextDouble() * GameConstants.MAX_SPEED);
            velocityY = (float)(rand.NextDouble() * GameConstants.MAX_SPEED);
            goalTime = rand.Next(2000, 5000);
        }

        public void Update(GameTime gameTime)
        {
            if (alive)
            {
                if (counter > goalTime)
                {
                    soundeffect.Play();
                    counter = 0;
                    goalTime = rand.Next(2000, 5000);
                    Vector2 target = new Vector2(SelinaNoGame.SelinaHitBox.Center.X, SelinaNoGame.SelinaHitBox.Center.Y);
                    Projectile projectile = new Projectile(SelinaNoGame.laserSprite, drawRectangle.Center.X,
                                                            drawRectangle.Center.Y,
                                                            target);
                    SelinaNoGame.addProjectile(projectile);
                }
                counter += gameTime.ElapsedGameTime.Milliseconds;

                drawRectangle.X += (int)(velocityX * gameTime.ElapsedGameTime.Milliseconds);
                drawRectangle.Y += (int)(velocityY * gameTime.ElapsedGameTime.Milliseconds);



                //Handling the Bounces
                BounceLeftRight();
                BounceTopBottom();
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (alive)
            {
                spriteBatch.Draw(sprite, drawRectangle, Color.White);
            }
        }

        #region Class Specific Private Functions

        private void BounceTopBottom()
        {
            if (drawRectangle.Y < 0)
            {
                // bounce off top
                drawRectangle.Y = 0;
                velocityY *= -1;
            }
            else if ((drawRectangle.Y + drawRectangle.Height) > GameConstants.SCREEN_HEIGHT)
            {
                // bounce off bottom
                drawRectangle.Y = GameConstants.SCREEN_HEIGHT - drawRectangle.Height;
                velocityY *= -1;
            }
        }
        /// <summary>
        /// Bounces the Becky off the left and right window borders if necessary
        /// </summary>
        private void BounceLeftRight()
        {
            if (drawRectangle.X < 0)
            {
                // bounc off left
                drawRectangle.X = 0;
                velocityX *= -1;
            }
            else if ((drawRectangle.X + drawRectangle.Width) > GameConstants.SCREEN_WIDTH)
            {
                // bounce off right
                drawRectangle.X = GameConstants.SCREEN_WIDTH - drawRectangle.Width;
                velocityX *= -1;
            }
        }

        #endregion


    }
}
