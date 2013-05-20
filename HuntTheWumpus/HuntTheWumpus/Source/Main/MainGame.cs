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
        public TextManager TextManager { get; set; }
        public SoundManager SoundManager { get; set; }
        public SpriteBatch SpriteBatch { get; set; }
        public GameTime GameTime { get; set; }
        public PlayerData PlayerData { get; set; }
        public Player Player;

        public MainGame()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Game Options:
            this.Graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;  // sets the width of the window to the screen width
            this.Graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height; // sets the height of the window to the screen height
            this.Graphics.PreferMultiSampling = true;      // enables anti-aliasing
            this.IsMouseVisible = true;                    // lets mouse to be drawn on the window
            //this.Graphics.IsFullScreen = true;
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
            this.LevelManager.CurrentLevel = new StartLevel(this);
            this.InputManager = new InputManager();
            this.SoundManager = new SoundManager();
            this.PlayerData = new PlayerData();
            this.Player = new Player(2);

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
            this.TextManager = new TextManager(this);
            this.SoundManager.LoadSounds(this.Content);
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
            this.TextManager.DrawText(new Vector2(0, 0), "fps: " + (1000.0 / gameTime.ElapsedGameTime.Milliseconds).ToString(), true);
            this.TextManager.DrawText(new Vector2(0, 20), "hp : " + this.PlayerData.HP, true);
            //this.TextManager.DrawText(new Vector2(0, 40), "score : " + this.PlayerData.Score, true);
            this.TextManager.DrawText(new Vector2(0, 40), "score: " + this.Player.score, true); 
            this.TextManager.DrawText(new Vector2(0, 60), "room : " + (this.LevelManager.CurrentLevel is Room ? (this.LevelManager.CurrentLevel as Room).RoomIndex : 0), true);
            base.Draw(gameTime);
        }
    }
}
