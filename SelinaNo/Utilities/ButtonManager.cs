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
        Object parent;

        public ButtonManager(Texture2D sprite, SpriteFont font, Object parent)
        {
            buttonList = new List<Button>();
            this.defaultSprite = sprite;
            this.defaultFont = font;
            this.parent = parent;
        }

        public ButtonManager(Object parent)
        {
            buttonList = new List<Button>();
            this.parent = parent;
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

        public void addButton(string text, int x, int y, Object action)
        {
            Button button = new Button(defaultSprite, defaultFont, text, new Point(x, y), action);
            buttonList.Add(button);
        }

        public void Update()
        {
            foreach(Button i in buttonList)
            {
                Object returnedValue = i.Update();
                if (returnedValue != null)
                {
                    if (returnedValue.GetType() == typeof(GameState))
                    {
                        if (parent.GetType() == typeof(SelinaNoGame))
                        {
                            SelinaNoGame p = (SelinaNoGame)parent;
                            p.setState((GameState)returnedValue);
                        }
                    }
                }

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
