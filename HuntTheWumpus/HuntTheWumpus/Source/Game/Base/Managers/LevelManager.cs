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
    public class LevelManager
    {
        public MainGame ParentGame { get; private set; }
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
                    this.currentLevel.OnUnLoad();
                    this.currentLevel.GameObjects.AddAndRemoveObjects();
                }
                this.currentLevel = value;
                if (value != null)
                {
                    this.currentLevel.OnLoad();
                }
            } 
        }

        public Cave GameCave { get; set; }

        private ILevel currentLevel;

        public LevelManager(MainGame parentGame)
        {
            this.ParentGame = parentGame;
            this.CurrentLevel = null;
            this.GameCave = new Cave(this.ParentGame, new Vector2(this.ParentGame.Graphics.PreferredBackBufferWidth, this.ParentGame.Graphics.PreferredBackBufferHeight));
        }

        public void FrameUpdate()
        {
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

        public void FrameDraw()
        {
            if (this.CurrentLevel != null)
            {
                this.CurrentLevel.FrameDraw(this.ParentGame.GameTime, this.ParentGame.SpriteBatch);
            }
        }
    }
}
