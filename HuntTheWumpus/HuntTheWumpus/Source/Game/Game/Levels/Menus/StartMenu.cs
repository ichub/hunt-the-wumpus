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
    class StartMenu : ILevel
    {
        public MainGame MainGame { get; set; }
        public GameObjectManager GameObjects { get; set; }
        public bool Initialized { get; set; }

        private Texture2D background;

        public StartMenu(MainGame mainGame)
        {
            this.MainGame = mainGame;
            this.GameObjects = new GameObjectManager(mainGame);
        }

        public void Initialize()
        {
            this.GameObjects.Add(new Button(this.MainGame, this, () => this.MainGame.LevelManager.CurrentLevel = this.MainGame.LevelManager.GameCave.Rooms[0], ButtonName.Start) { Position = new Vector2(1024 - 200, 768 - 100) / 2 });
            this.GameObjects.Add(new Button(this.MainGame, this, () => this.MainGame.LevelManager.CurrentLevel = new HighScoreMenu(this.MainGame, this), ButtonName.HighScore) { Position = new Vector2(1024 - 200, 768 - 100) / 2 + new Vector2(0, 150) });

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
            if (this.Initialized)
            {
                spriteBatch.Draw(this.background, new Vector2(0), Color.White);
                this.GameObjects.FrameDraw();
            }
        }
    }
}
