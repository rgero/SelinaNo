using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SelinaNo.Scenes;

namespace SelinaNo
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class SelinaNoGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont scoreboardFont;

        //Declaring Scenes
        TitleScene titleScene;
        GameScene gameScene;

        //Declaring the ONE random that will be used the entire game.
        Random rand;

        GameState currentState;
        ControlScheme currentControls;

        public static int gameScore;

        public SelinaNoGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = GameConstants.SCREEN_HEIGHT;
            graphics.PreferredBackBufferWidth = GameConstants.SCREEN_WIDTH;

            titleScene = new TitleScene(this);
            gameScene = new GameScene(this);

            rand = new Random();

            currentControls = ControlScheme.Mouse;
            currentState = GameState.MainMenu;

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Loading Font
            scoreboardFont = Content.Load<SpriteFont>(@"Fonts\scoreboardFont");



            titleScene.LoadContent(this.Content);
            gameScene.LoadContent(this.Content);



        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (currentState == GameState.MainMenu)
            {
                titleScene.Update();

            }

            if (currentState == GameState.Playing)
            {
                gameScene.Update(gameTime);
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin();

            if (currentState == GameState.Playing)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
                gameScene.Draw(spriteBatch, scoreboardFont);
            }

            if (currentState == GameState.MainMenu)
            {
                GraphicsDevice.Clear(Color.Black);
                titleScene.Draw(spriteBatch, scoreboardFont);
            }

            if (currentState == GameState.LostNoHigh)
            {
                GraphicsDevice.Clear(Color.Black);
                string message = "You Lost!\nYour score was " + gameScore.ToString() + "\nPress Escape to Quit";
                Vector2 size = scoreboardFont.MeasureString(message);

                spriteBatch.DrawString(scoreboardFont, message,
                    new Vector2(GameConstants.SCREEN_WIDTH / 2 - size.X / 2, GameConstants.SCREEN_HEIGHT / 2 - size.Y / 2),
                    Color.White);

            }

            spriteBatch.End();


            base.Draw(gameTime);
        }

        public static void setScore(int score)
        {
            gameScore = score;
        }

        public Random getRandom()
        {
            return rand;
        }

        public void setControls(ControlScheme e)
        {
            currentControls = e;
        }

        public ControlScheme getControls()
        {
            return currentControls;
        }

        public void setState(GameState state)
        {
            currentState = state;
        }

    }
}
