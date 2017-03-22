﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using SelinaNo.Scenes;


namespace SelinaNo.Entities
{
    class BeckyManager
    {
        GameScene parent;
        Texture2D bexSprite;
        SoundEffect merhSound;
        List<Becky> bexList = new List<Becky>();

        public BeckyManager(GameScene game)
        {
            parent = game;
            bexList = new List<Becky>();
        }

        public void LoadContent(ContentManager Content)
        {
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
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Drawing the Becky's
            foreach (Becky bex in bexList)
            {
                bex.Draw(spriteBatch);
            }
        }

        //Helper Functions

        public List<Becky> getBexList()
        {
            return bexList;
        }



        private void spawnBex()
        {
            int BexX = parent.getRandom().Next(0, GameConstants.SCREEN_WIDTH - bexSprite.Width);
            int BexY = parent.getRandom().Next(0, GameConstants.SCREEN_HEIGHT - bexSprite.Height);
            Becky Bex = new Becky(parent, bexSprite, BexX, BexY, parent.getRandom(), merhSound);
            bexList.Add(Bex);
        }

        public void clearList()
        {
            bexList.Clear();
        }

    }
}
