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
        EndScene endScene;
        OptionsScene optionsScene;

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
            endScene = new EndScene(this);
            optionsScene = new OptionsScene(this);
            

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

            SoundEffect.MasterVolume = 0.1f;

            titleScene.LoadContent(this.Content);
            gameScene.LoadContent(this.Content);
            endScene.LoadContent(this.Content);



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

            if (currentState == GameState.LostNoHigh)
            {
                endScene.Update();
            }

            if (currentState == GameState.Options)
            {
                optionsScene.Update();
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
                endScene.Draw(spriteBatch, scoreboardFont);

            }

            if (currentState == GameState.Options)
            {
                GraphicsDevice.Clear(Color.Black);
                optionsScene.Draw(spriteBatch, scoreboardFont);

            }

            spriteBatch.End();


            base.Draw(gameTime);
        }

        public void setScore(int score)
        {
            gameScore = score;
        }

        public int getScore()
        {
            return gameScore;
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

        public void resetGame()
        {
            gameScene.resetGame();
        }

        public void setVolume(float newVolume)
        {
            SoundEffect.MasterVolume = newVolume;
        }

    }
}
