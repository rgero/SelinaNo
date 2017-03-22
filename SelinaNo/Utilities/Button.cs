using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace SelinaNo.Utilities
{
    class Button
    {
        Texture2D backgroundSprite;
        Rectangle buttonRect;
        SpriteFont font;
        string buttonText;
        ButtonState clickStatus;

        public Button(Texture2D sprite, SpriteFont font, string text, Point origin)
        {
            this.backgroundSprite = sprite;
            this.font = font;
            this.buttonText = text;
            this.buttonRect = new Rectangle(origin.X, origin.Y, sprite.Width, sprite.Height);
            clickStatus = ButtonState.Released;
        }

        public Button(Texture2D sprite, SpriteFont font, string text, Rectangle rect)
        {
            this.backgroundSprite = sprite;
            this.font = font;
            this.buttonText = text;
            this.buttonRect = rect;
        }

        public void Update()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed
                    && clickStatus == ButtonState.Released
                    && buttonRect.Contains(Mouse.GetState().Position))
            {
                clickStatus = ButtonState.Pressed;
            }
            if (Mouse.GetState().LeftButton == ButtonState.Released && clickStatus == ButtonState.Pressed)
            {
                if (buttonRect.Contains(Mouse.GetState().Position))
                {
                    Console.WriteLine(buttonText + " - Button Clicked");
                    clickStatus = ButtonState.Released;
                }
                clickStatus = ButtonState.Released;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 stringSize = font.MeasureString(buttonText);
            buttonRect.Width = (int)stringSize.X + 20;
            buttonRect.Height = (int)stringSize.Y;
            spriteBatch.Draw(backgroundSprite, buttonRect, Color.White);
            spriteBatch.DrawString(font, buttonText, buttonRect.Location.ToVector2(), Color.Black);
        }
    }
}
