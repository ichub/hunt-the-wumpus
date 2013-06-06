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
    /// Game over screen.
    /// </summary>
    class GameOverMenu : ILevel
    {
        public MainGame MainGame { get; set; }
        public GameObjectManager GameObjects { get; set; }
        public bool Initialized { get; set; }

        private Texture2D background;

        public GameOverMenu(MainGame mainGame)
        {
            this.MainGame = mainGame;
            this.GameObjects = new GameObjectManager(mainGame);
        }

        public void Initialize()
        {
            this.GameObjects.Add(new Button(this.MainGame,
                this,
                () => this.MainGame.LevelManager.CurrentLevel = new StartMenu(this.MainGame), ButtonName.Menu)
                {
                    Position = new Vector2(1024, 768) / 2 - new Vector2(100, 0)
                });

            this.background = this.MainGame.Content.Load<Texture2D>("Textures\\gameover");
           
            //Player Lost Game
            SingleScore score = new SingleScore(this.MainGame.Player.Name, this.MainGame.Player.Score);
            this.MainGame.HighScore.Add(score);
            this.MainGame.Player.Reset();
            this.MainGame.MiniMap.Reset();
            this.MainGame.LevelManager.GameCave.Reset();
        }

        public void OnLoad() { }
        public void OnUnLoad() { }
        public void Reset() { }

        public void FrameUpdate(GameTime gameTime, ContentManager content)
        {
            this.GameObjects.FrameUpdate();
        }

        public void FrameDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.background, new Vector2(0), Color.White);
            this.GameObjects.FrameDraw();
        }
    }
}
