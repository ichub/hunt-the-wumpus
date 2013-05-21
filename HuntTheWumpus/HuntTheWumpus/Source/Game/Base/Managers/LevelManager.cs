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
    public class LevelManager
    {
        public MainGame ParentGame { get; private set; }
        public bool Paused { get; set; }

        private Texture2D levelFade;
        private float fadeCount;
        private int fadeSpeed;

        private Timer timer;

        public ILevel CurrentLevel 
        {
            get 
            { 
                return this.currentLevel; 
            } 
            set
            {
                if (this.currentLevel != null)
                {
                    
                    this.currentLevel.GameObjects.AddAndRemoveObjects();
                    this.StartFade(value);
                }
                else
                {
                    this.currentLevel = value;
                }                
            } 
        }

        public Cave GameCave { get; set; }

        private ILevel currentLevel;
        private ILevel nextLevel;

        public LevelManager(MainGame parentGame)
        {
            this.Paused = false;
            this.ParentGame = parentGame;
            this.CurrentLevel = null;
            this.levelFade = new Texture2D(parentGame.GraphicsDevice, parentGame.WindowWidth, parentGame.WindowHeight);
            Extensions.FillTexture(levelFade, Color.Black);
            this.GameCave = new Cave(this.ParentGame, new Vector2(this.ParentGame.Graphics.PreferredBackBufferWidth, this.ParentGame.Graphics.PreferredBackBufferHeight));
            this.fadeSpeed = 10;
        }

        public void FrameUpdate()
        {
            if (!this.Paused)
            if (this.CurrentLevel != null)
            {
                if (!this.CurrentLevel.Initialized)
                {
                    this.CurrentLevel.Initialize();
                    this.CurrentLevel.Initialized = true;
                }

                this.CurrentLevel.FrameUpdate(this.ParentGame.GameTime, this.ParentGame.Content);
            }
        }

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


        public void OnFadeInEnd()
        {
            this.timer.Stop();
            this.timer = null;
        }

        public void FadeIn(object sender, ElapsedEventArgs e)
        {
            this.fadeCount -= this.fadeSpeed;
            if (this.fadeCount < 0)
            {
                this.fadeCount = 0;
                this.OnFadeInEnd();
            }
        }

        public void FadeOut(object sender, ElapsedEventArgs e)
        {
            this.fadeCount += this.fadeSpeed;
            if (this.fadeCount > 255)
            {
                this.fadeCount = 255;
                this.OnFadeOutEnd();
            }
        }

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
