using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SelinaNo
{
    public class Projectile
    {
        Texture2D sprite;
        Rectangle drawRectangle;
        int x;
        int y;
        Vector2 targetLocation;
        Vector2 normalizedVector;
        bool alive = true;

        public bool Active
        {
            get { return alive; }
            set { alive = value; }
        }

        public Rectangle CollisionRectangle
        {
            get { return drawRectangle; }
        }


        public Projectile(Texture2D sprite, int spawnX, int spawnY, Vector2 targetLocation)
        {
            this.sprite = sprite;
            x = spawnX;
            y = spawnY;
            this.targetLocation = targetLocation;

            normalizedVector = new Vector2(targetLocation.X - x, targetLocation.Y - y);
            normalizedVector.Normalize();

            drawRectangle = new Rectangle(spawnX, spawnY, sprite.Width, sprite.Height);

        }

        public void Update(GameTime gameTime)
        {
            if (alive)
            {
                drawRectangle.X += (int)(normalizedVector.X * gameTime.ElapsedGameTime.Milliseconds);
                drawRectangle.Y += (int)(normalizedVector.Y * gameTime.ElapsedGameTime.Milliseconds);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (alive)
            {
                spriteBatch.Draw(sprite, drawRectangle, Color.Red);
            }
        }
    }
}
