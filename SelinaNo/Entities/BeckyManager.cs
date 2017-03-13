using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace SelinaNo.Entities
{
    class BeckyManager
    {

        // Declaring Bex's texture
        Texture2D bexSprite;

        //Declaring Laser Sprite
        public static Texture2D laserSprite;

        SoundEffect merhSound;

        //Declaring the List of Bex
        List<Becky> bexList = new List<Becky>();
        static List<Projectile> projectileList = new List<Projectile>();


        public BeckyManager()
        {
            bexList = new List<Becky>();
        }

        public void LoadContent(ContentManager Content)
        {
            //Loading Laser Sprite
            laserSprite = Content.Load<Texture2D>(@"Sprites\laser");

            merhSound = Content.Load<SoundEffect>(@"Sounds\Merh");

            //Loading the Bex Sprite
            bexSprite = Content.Load<Texture2D>(@"Sprites\Bex");

        }

        public void Update(GameTime gameTime)
        {

            //Removing Dead Beckys
            for (int i = bexList.Count - 1; i >= 0; i--)
            {
                if (!bexList[i].Active)
                {
                    bexList.RemoveAt(i);
                }
            }

            //Adding new Becky
            if (bexList.Count < 10)
            {
                spawnBex();
            }

            foreach (Becky bex in bexList)
            {
                bex.Update(gameTime);
            }

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

        }

        public void Draw(SpriteBatch spriteBatch)
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
        }

        //Helper Functions

        public static void addProjectile(Projectile projectile)
        {
            projectileList.Add(projectile);
        }

        public List<Becky> getBexList()
        {
            return bexList;
        }

        public List<Projectile> getProjectiles()
        {
            return projectileList;
        }

        private void spawnBex()
        {
            int BexX = GameConstants.rand.Next(0, GameConstants.SCREEN_WIDTH - bexSprite.Width);
            int BexY = GameConstants.rand.Next(0, GameConstants.SCREEN_HEIGHT - bexSprite.Height);
            Becky Bex = new Becky(bexSprite, BexX, BexY, GameConstants.rand, merhSound);
            bexList.Add(Bex);
        }

    }
}
