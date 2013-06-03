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
using System.Timers;

namespace HuntTheWumpus.Source
{
    /// <summary>
    /// Class responsible for updating, drawing, and changing levels.
    /// </summary>
    public class LevelManager
    {
        public MainGame ParentGame { get; private set; }
        public bool Paused { get; set; }

        private Texture2D levelFade;
        private float fadeCount;
        private int fadeSpeed;
        private bool isLevelChanging;

        private Timer timer;

        public ILevel CurrentLevel
        {
            get
            {
                return this.currentLevel;
            }
            set
            {
                if (!this.isLevelChanging)
                {
                    if (this.currentLevel != null)
                    {
                        this.StartFade(value);
                        this.isLevelChanging = true;
                    }
                    else
                    {
                        this.currentLevel = value;
                        this.currentLevel.OnLoad();
                    }
                }
            }
        }

        public Cave GameCave { get; set; }

        private ILevel currentLevel;
        private ILevel nextLevel;

        /// <summary>
        /// Creates a new level manager.
        /// </summary>
        /// <param name="parentGame"> Game to which this level manager belongs. </param>
        public LevelManager(MainGame parentGame)
        {
            this.Paused = false;
            this.ParentGame = parentGame;
            this.levelFade = new Texture2D(parentGame.GraphicsDevice, parentGame.WindowWidth, parentGame.WindowHeight);
            Extensions.FillTexture(levelFade, Color.Black);
            this.GameCave = new Cave(this.ParentGame, new Vector2(this.ParentGame.Graphics.PreferredBackBufferWidth, this.ParentGame.Graphics.PreferredBackBufferHeight));
            this.fadeSpeed = 20;
        }

        /// <summary>
        /// Updates the current level.
        /// </summary>
        public void FrameUpdate()
        {
            if (!this.Paused)
            {
                if (this.CurrentLevel != null)
                {
                    if (!this.CurrentLevel.Initialized)
                    {
                        this.CurrentLevel.Initialize();
                        this.CurrentLevel.Initialized = true;
                    }
                    this.CurrentLevel.FrameUpdate(this.ParentGame.GameTime, this.ParentGame.Content);
                    this.GameCave.UpdateSuperBats();
                }
            }
        }

        /// <summary>
        /// Starts fading from one level to the given level.
        /// </summary>
        /// <param name="toFadeInto"> Level to fade into. </param>
        public void StartFade(ILevel toFadeInto)
        {
            if (this.timer != null)
            {
                this.timer.Stop();
                this.fadeCount = 0;
            }

            this.timer = new Timer(30);
            this.timer.Elapsed += this.FadeOut;
            this.timer.Start();
            this.nextLevel = toFadeInto;
        }

        /// <summary>
        /// Called when the current level has been faded out of. Changes the current
        /// level and starts fading into it.
        /// </summary>
        public void OnFadeOutEnd()
        {
            this.currentLevel.OnUnLoad();
            this.currentLevel = this.nextLevel;
            this.currentLevel.OnLoad();

            this.timer.Stop();
            this.timer = new Timer(30);
            this.timer.Elapsed += this.FadeIn;
            this.timer.Start();
            this.nextLevel = null;
        }

        /// <summary>
        /// Called when the current level has been completely faded in.
        /// </summary>
        public void OnFadeInEnd()
        {
            this.timer.Stop();
            this.timer = null;
            this.isLevelChanging = false;
        }

        /// <summary>
        /// Method that fades into the current level.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FadeIn(object sender, ElapsedEventArgs e)
        {
            this.fadeCount -= this.fadeSpeed;
            if (this.fadeCount < 0)
            {
                this.fadeCount = 0;
                this.OnFadeInEnd();
            }
        }

        /// <summary>
        /// Method that fades out of the current level.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FadeOut(object sender, ElapsedEventArgs e)
        {
            this.fadeCount += this.fadeSpeed;
            if (this.fadeCount > 255)
            {
                this.fadeCount = 255;
                this.OnFadeOutEnd();
            }
        }

        /// <summary>
        /// Draws the current level.
        /// </summary>
        public void FrameDraw()
        {
            if (this.CurrentLevel != null)
            {
                this.CurrentLevel.FrameDraw(this.ParentGame.GameTime, this.ParentGame.SpriteBatch);
            }

            this.ParentGame.SpriteBatch.Draw(this.levelFade, Vector2.Zero, new Color(255, 255, 255, (int)this.fadeCount));
        }
    }
}
