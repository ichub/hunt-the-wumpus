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
        public GraphicsDeviceManager Graphics { get; private set; }
        public LevelManager LevelManager { get; private set; }
        public InputManager InputManager { get; private set; }
        public TextManager TextManager { get; private set; }
        public SoundManager SoundManager { get; private set; }
        public SpriteBatch SpriteBatch { get; private set; }
        public DoGooder DoGooder { get; private set; }
        public GameTime GameTime { get; private set; }
        public PlayerStats Player { get; private set; }
        public MiniMap MiniMap { get; private set; }
        public Trivia TriviaManager { get; private set; }
        public Random Random { get; private set; }
        public HighScore HighScore { get; private set; }

        public int ScreenWidth { get; private set; }
        public int ScreenHeight { get; private set; }
        public int WindowWidth { get; private set; }
        public int WindowHeight { get; private set; }

        public MainGame()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.ScreenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            this.ScreenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;


            // Game Options:
            this.Graphics.PreferredBackBufferWidth = 1024;//GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;  // sets the width of the window to the screen width
            this.Graphics.PreferredBackBufferHeight = 768;//GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height; // sets the height of the window to the screen height
            this.Graphics.PreferMultiSampling = true;      // enables anti-aliasing
            this.IsMouseVisible = true;                    // allows to be drawn on the window
            //this.Graphics.IsFullScreen = true;

            this.WindowWidth = this.Graphics.PreferredBackBufferWidth;
            this.WindowHeight = this.Graphics.PreferredBackBufferHeight;
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
            RoomFactory.InitFactory(this.Content);
            this.TriviaManager = new Trivia(this);
            this.DoGooder = new DoGooder(this);
            this.Random = new Random();
            this.MiniMap = new MiniMap(this, new Vector2(200f, 200f));
            this.LevelManager = new LevelManager(this);
            Extensions.Init(this);
            this.LevelManager.CurrentLevel = new HighScoreMenu(this, new StartLevel(this));
            this.InputManager = new InputManager();
            this.SoundManager = new SoundManager();
            this.Player = new PlayerStats("Sexy Beast");
            this.HighScore = new HighScore(this);
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
            this.DoGooder.Update();
            this.MiniMap.Update();
            this.MiniMap.ShowRoom((this.LevelManager.CurrentLevel is Room ? (this.LevelManager.CurrentLevel as Room) : null));
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);    // clears the screen
            this.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);                  // begins drawing
            this.LevelManager.FrameDraw();                 // draws the level
            this.MiniMap.Draw(this.SpriteBatch, this.Content);
            this.SpriteBatch.End();                        // stops drawing
            this.TextManager.DrawText(new Vector2(0, 0), "fps: " + (1000.0 / gameTime.ElapsedGameTime.Milliseconds).ToString(), true);
            this.TextManager.DrawText(new Vector2(0, 20), "hp : " + this.Player.HP, true);
            this.TextManager.DrawText(new Vector2(0, 40), "score: " + this.Player.Score, true);
            this.TextManager.DrawText(new Vector2(0, 60), "room : " + (this.LevelManager.CurrentLevel is Room ? (this.LevelManager.CurrentLevel as Room).RoomIndex : 0), true);
            this.TextManager.DrawText(new Vector2(0, 80), "gold : " + this.Player.Inventory.AmountOfGold().ToString(), true);
            this.TextManager.DrawText(new Vector2(0, 100), "highscore : " + this.HighScore.GetHighScore().ToString(), true);
            base.Draw(gameTime);
        }
    }
}
