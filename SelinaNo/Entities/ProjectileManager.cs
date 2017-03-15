using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SelinaNo.Scenes;

namespace SelinaNo.Entities
{
    class ProjectileManager
    {
        GameScene parent;
        List<Projectile> projectileList;

        public ProjectileManager(GameScene game)
        {
            parent = game;
            projectileList = new List<Projectile>();

        }

        public void Update(GameTime gameTime)
        {
            foreach (Projectile projectile in projectileList)
            {
                projectile.Update(gameTime);
            }

            //Removing Dead Projectiles
            if (projectileList.Count > 0)
            {
                for (int k = projectileList.Count - 1; k >= 0; k--)
                {
                    if (!projectileList[k].Active)
                    {
                        projectileList.RemoveAt(k);
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Drawing the Projectiles
            foreach (Projectile projectile in projectileList)
            {
                projectile.Draw(spriteBatch);

            }
        }

        public void addProjectile(Projectile projectile)
        {
            projectileList.Add(projectile);
        }

        public List<Projectile> getProjectiles()
        {
            return projectileList;
        }
    }
}
