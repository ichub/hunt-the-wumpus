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
    class StartLevel : ILevel
    {
        public MainGame MainGame { get; set; }
        public GameObjectManager GameObjects { get; set; }
        public bool Initialized { get; set; }

        private Texture2D background;

        public StartLevel(MainGame mainGame)
        {
            this.MainGame = mainGame;
            this.GameObjects = new GameObjectManager(mainGame);
        }

        public void Initialize()
        {
            Button startButton = new Button(this.MainGame, this, () => this.MainGame.LevelManager.CurrentLevel = this.MainGame.LevelManager.GameCave.Rooms[0]) { Position = new Vector2(1024 - 200, 768 - 100) / 2 };
            this.GameObjects.Add(startButton);
            this.background = MainGame.Content.Load<Texture2D>("Textures\\titlescreen");
        }

        public void OnLoad()
        {
            return;
        }

        public void OnUnLoad()
        {
            return;
        }

        public void Reset()
        {
            return;
        }

        public void FrameUpdate(GameTime gameTime, ContentManager content)
        {
            this.GameObjects.FrameUpdate();
        }

        public void FrameDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.background, this.MainGame.LevelManager.GameCave.CaveOffset, Color.White);
            this.GameObjects.FrameDraw();
        }
    }
}
