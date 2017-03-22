using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace SelinaNo.Utilities
{
    class ButtonManager
    {
        List<Button> buttonList;
        Texture2D defaultSprite;
        SpriteFont defaultFont;

        public ButtonManager(Texture2D sprite, SpriteFont font)
        {
            buttonList = new List<Button>();
            this.defaultSprite = sprite;
            this.defaultFont = font;
        }

        public ButtonManager()
        {
            buttonList = new List<Button>();
        }

        public void Load(ContentManager Content)
        {
            defaultSprite = Content.Load<Texture2D>(@"Sprites\defaultButtonSprite");
            defaultFont = Content.Load<SpriteFont>(@"Fonts\scoreboardFont");
        }

        public void addButton(Button button)
        {
            buttonList.Add(button);
        }

        public void addButton(string text, int x, int y)
        {
            Button button = new Button(defaultSprite, defaultFont, text, new Point(x, y));
            buttonList.Add(button);
        }

        public void Update()
        {
            foreach(Button i in buttonList)
            {
                i.Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Button i in buttonList)
            {
                i.Draw(spriteBatch);
            }
        }


    }
}
