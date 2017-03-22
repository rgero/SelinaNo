using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using SelinaNo.Utilities;

namespace SelinaNo
{
    class TitleScene
    {
        SelinaNoGame parent;

        //Title Sprite
        Texture2D titleSprite;
        Rectangle titleRect;

        // Mouse Sprite
        Texture2D mouseSprite;
        Rectangle mouseRectangle;

        //Pose Sprite
        Texture2D leftPoseSprite;
        Rectangle leftPoseRect;
        Texture2D rightPoseSprite;
        Rectangle rightPoseRect;

        ButtonManager buttonManager;


        static string base_control_msg = "Press 1 for mouse, 2 for keyboard:\n";
        static string complete_ctrl_msg = base_control_msg + "Currently Selected: Mouse";

        public TitleScene(SelinaNoGame g)
        {
            parent = g;
            buttonManager = new ButtonManager(g);
        }

        public void LoadContent(ContentManager Content)
        {
            //Loading Title Sprite
            titleSprite = Content.Load<Texture2D>(@"Sprites\title");
            titleRect = new Rectangle(GameConstants.SCREEN_WIDTH / 2 - titleSprite.Width / 2,
                                      100,
                                      titleSprite.Width,
                                      titleSprite.Height);

            //Mouse Sprite
            mouseSprite = Content.Load<Texture2D>(@"Sprites\laser");
            mouseRectangle = new Rectangle(mouseSprite.Width / 2, mouseSprite.Height / 2, mouseSprite.Width, mouseSprite.Height);

            leftPoseSprite = Content.Load<Texture2D>(@"Sprites\Pose");
            float resizingFactor = ((float)GameConstants.SCREEN_HEIGHT / leftPoseSprite.Height);
            leftPoseRect = new Rectangle(-GameConstants.SCREEN_WIDTH / 6,
                                        GameConstants.SCREEN_HEIGHT / 3,
                                       (int)(resizingFactor * leftPoseSprite.Width),
                                       (int)(resizingFactor * leftPoseSprite.Height)
                                    );
            rightPoseSprite = Content.Load<Texture2D>(@"Sprites\PoseRight");
            rightPoseRect = new Rectangle((int)(3.5 * GameConstants.SCREEN_WIDTH / 6),
                                            GameConstants.SCREEN_HEIGHT / 3,
                                            (int)(resizingFactor * leftPoseSprite.Width),
                                            (int)(resizingFactor * leftPoseSprite.Height)
                                    );

            buttonManager.Load(Content);
            buttonManager.addButton("Testing", 10, 10, GameState.LostNoHigh);
        }

        public void Update()
        {
            mouseRectangle.X = Mouse.GetState().X - mouseSprite.Width / 2;
            mouseRectangle.Y = Mouse.GetState().Y - mouseSprite.Height / 2;

            buttonManager.Update();

            if (Keyboard.GetState().IsKeyDown(Keys.Tab))
            {
                parent.setState(GameState.Playing);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D1))
            {
                complete_ctrl_msg = base_control_msg + "Currently Selected: Mouse";
                parent.setControls(ControlScheme.Mouse);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D2))
            {
                complete_ctrl_msg = base_control_msg + "Currently Selected: Keyboard";
                parent.setControls(ControlScheme.Keyboard);
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont scoreboardFont)
        {
            spriteBatch.Draw(leftPoseSprite, leftPoseRect, Color.White);
            spriteBatch.Draw(rightPoseSprite, rightPoseRect, Color.White);
            spriteBatch.Draw(titleSprite, titleRect, Color.White);

            
            string message = "Press Tab to Play!";
            Vector2 size = scoreboardFont.MeasureString(message);
            spriteBatch.DrawString(scoreboardFont, message,
                new Vector2(GameConstants.SCREEN_WIDTH / 2 - size.X / 2, 3 * GameConstants.SCREEN_HEIGHT / 5), Color.White);
            if (complete_ctrl_msg != null)
            {
                Vector2 stringSize = scoreboardFont.MeasureString(complete_ctrl_msg);
                Vector2 controlLocation = new Vector2(GameConstants.CONTROL_X - stringSize.X / 2, GameConstants.CONTROL_Y);
                spriteBatch.DrawString(scoreboardFont, complete_ctrl_msg, controlLocation, Color.DodgerBlue);
            }

            buttonManager.Draw(spriteBatch);
            spriteBatch.Draw(mouseSprite, mouseRectangle, Color.White);
        }
    }
}
