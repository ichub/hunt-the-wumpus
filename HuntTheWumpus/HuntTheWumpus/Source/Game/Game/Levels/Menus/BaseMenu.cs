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
    /// Main menu.
    /// </summary>
    class BaseMenu : ILevel
    {
        public MainGame MainGame { get; set; }
        public GameObjectManager GameObjects { get; set; }
        public bool Initialized { get; set; }

        private Texture2D background;
        private ILevel cameFrom;

        public BaseMenu(MainGame mainGame, ILevel cameFrom)
        {
            this.MainGame = mainGame;
            this.GameObjects = new GameObjectManager(mainGame);
            this.cameFrom = cameFrom;
        }

        public BaseMenu(MainGame mainGame)
            : this(mainGame, mainGame.LevelManager.GameCave.Rooms[0]) { }

        public virtual void Initialize()
        {
            this.background = MainGame.Content.Load<Texture2D>("Textures\\menu");
        }

        public virtual void OnLoad()
        {
            this.GameObjects.Add(new Button(this.MainGame, 
                this, 
                () => { this.MainGame.LevelManager.CurrentLevel = cameFrom; },
                "quit") 
                { Position = new Vector2(512 - 100, 570) });
        }

        public virtual void OnUnLoad()
        {
        }

        public virtual void Reset()
        {
        }

        public virtual void FrameUpdate(GameTime gameTime, ContentManager content)
        {
            this.GameObjects.FrameUpdate();
        }

        public virtual void FrameDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.background, new Vector2(0), Color.White);
            this.GameObjects.FrameDraw();
        }
    }
}