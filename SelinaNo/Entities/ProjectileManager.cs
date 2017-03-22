﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using SelinaNo.Scenes;

namespace SelinaNo.Entities
{
    class ProjectileManager
    {
        GameScene parent;
        List<Projectile> projectileList;
        Texture2D laserSprite;

        public ProjectileManager(GameScene game)
        {
            parent = game;
            projectileList = new List<Projectile>();

        }

        public void LoadContent(ContentManager Content)
        {
            //Loading Laser Sprite
            laserSprite = Content.Load<Texture2D>(@"Sprites\laser");
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

        public void clearList()
        {
            projectileList.Clear();
        }

        public Texture2D getProjectileSprite()
        {
            return laserSprite;
        }
    }
}
