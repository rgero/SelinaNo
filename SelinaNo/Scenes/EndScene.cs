using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


namespace SelinaNo.Scenes
{
    class EndScene
    {
        SelinaNoGame parent;


        static string base_control_msg = "Press 1 for mouse, 2 for keyboard:\n";
        static string complete_ctrl_msg = base_control_msg + "Currently Selected: Mouse";

        public EndScene(SelinaNoGame g)
        {
            parent = g;
        }

        public void LoadContent(ContentManager Content)
        {
            //Don't need to load anything in this scene yet.
        }

        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                parent.resetGame();
                parent.setState(GameState.MainMenu);
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont scoreboardFont)
        {
            List<String> lines = new List<String>();
            lines.Add("You Lost!");
            lines.Add("Your score was " + parent.getScore().ToString());
            lines.Add("Press Escape to quit or Enter to return to the main menu.");

            for (int i = 0; i < lines.Count; i++)
            {
                String message = lines[i];
                Vector2 size = scoreboardFont.MeasureString(message);

                spriteBatch.DrawString(scoreboardFont, message,
                    new Vector2(GameConstants.SCREEN_WIDTH / 2 - size.X / 2, GameConstants.SCREEN_HEIGHT / 2 - size.Y / 2 + i*GameConstants.LINE_OFFSET),
                    Color.White);
            }
        }


    }
}
