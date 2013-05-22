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
    class GameOverLevel : ILevel
    {
        public MainGame MainGame { get; set; }
        public GameObjectManager GameObjects { get; set; }
        public bool Initialized { get; set; }

        public GameOverLevel(MainGame mainGame)
        {
            this.MainGame = mainGame;
            this.GameObjects = new GameObjectManager(mainGame);
        }

        public void Initialize()
        {
            this.GameObjects.Add(new Button(this.MainGame, this, () =>
                    this.MainGame.LevelManager.CurrentLevel = new StartLevel(this.MainGame),
                "main menu"));
        }

        public void OnLoad()
        {
        }

        public void OnUnLoad()
        {
        }

        public void Reset()
        {
        }

        public void FrameUpdate(GameTime gameTime, ContentManager content)
        {
            this.GameObjects.FrameUpdate();
        }

        public void FrameDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            this.MainGame.TextManager.DrawText(new Vector2(400, 400), "GAME OVER", false);
            this.GameObjects.FrameDraw();
        }
    }
}
