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
        Vector2 stringSize;
        Vector2 stringPos;
        Vector2 rectPos;
        string buttonText;
        ButtonState clickStatus;
        Object onClickAction;

        public Button(Texture2D sprite, SpriteFont font, string text, Point origin, Object action)
        {
            this.backgroundSprite = sprite;
            this.font = font;
            this.stringSize = font.MeasureString(text);
            this.buttonText = text;

            // Origin should be the CENTER.
            int xPos = (int)(origin.X - stringSize.X / 2.0);
            int yPos = (int)(origin.Y - stringSize.Y / 2.0);

            this.buttonRect = new Rectangle(xPos, yPos, (int)stringSize.X + 60, (int)stringSize.Y + 60);
            this.stringPos = new Vector2(xPos + 30, yPos + 30);
            this.onClickAction = action;
            clickStatus = ButtonState.Released;
        }


        public Object Update()
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
                    return onClickAction;
                }
                clickStatus = ButtonState.Released;
            }
            return null;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundSprite, buttonRect, Color.White);
            spriteBatch.DrawString(font, buttonText, stringPos, Color.Black);
        }
    }
}
