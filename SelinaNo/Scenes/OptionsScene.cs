using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace SelinaNo.Scenes
{
    class OptionsScene
    {
        SelinaNoGame parent;

        public OptionsScene(SelinaNoGame parent)
        {
            this.parent = parent;
        }


        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                parent.setState(GameState.MainMenu);
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont scoreboardFont)
        {
            // Nothing Implemented Yet.
        }

    }
}
