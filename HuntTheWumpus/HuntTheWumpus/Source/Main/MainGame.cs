using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace HuntTheWumpus.Source
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MainGame : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager Graphics { get; set; }
        public LevelManager LevelManager { get; set; }
        public InputManager InputManager { get; set; }
        public SpriteBatch SpriteBatch { get; set; }
        public GameTime GameTime { get; set; }

        public MainGame()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Game Options:
            this.Graphics.PreferredBackBufferWidth = 800;  // sets the width of the window to 800 pixels
            this.Graphics.PreferredBackBufferHeight = 600; // sets the height of the window to 600 pixels
            this.Graphics.PreferMultiSampling = true;      // enables anti-aliasing
            this.IsMouseVisible = true;                    // lets mouse to be drawn on the window
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
            this.LevelManager = new LevelManager(this);
            this.LevelManager.CurrentLevel = new WumpusPlayLevel(this);
            this.InputManager = new InputManager();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // TODO: Add your update logic here
            this.GameTime = gameTime;
            this.InputManager.Update();
            this.LevelManager.FrameUpdate();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);    // clears the screen
            this.SpriteBatch.Begin();                      // begins drawing
            this.LevelManager.FrameDraw();                 // draws the level
            this.SpriteBatch.End();                        // stops drawing

            base.Draw(gameTime);
        }
    }
}
