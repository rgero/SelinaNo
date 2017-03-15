using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace SelinaNo
{
    class TitleScene
    {
        SelinaNoGame parent;

        //Title Sprite
        Texture2D titleSprite;
        Rectangle titleRect;

        //Pose Sprite
        Texture2D leftPoseSprite;
        Rectangle leftPoseRect;
        Texture2D rightPoseSprite;
        Rectangle rightPoseRect;

        static string base_control_msg = "Press 1 for mouse, 2 for keyboard:\n";
        static string complete_ctrl_msg = base_control_msg + "Currently Selected: Mouse";

        public TitleScene(SelinaNoGame g)
        {
            parent = g;
        }

        public void LoadContent(ContentManager Content)
        {
            //Loading Title Sprite
            titleSprite = Content.Load<Texture2D>(@"Sprites\title");
            titleRect = new Rectangle(GameConstants.SCREEN_WIDTH / 2 - titleSprite.Width / 2,
                                      100,
                                      titleSprite.Width,
                                      titleSprite.Height);

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
        }

        public void Update()
        {
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
        }


    }
}
