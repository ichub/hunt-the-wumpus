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
    class Room : ILevel
    {
        public MainGame MainGame { get; set; }
        public GameObjectManager GameObjects { get; set; }
        public bool Initialized { get; set; }

        public Room(MainGame mainGame)
        {
            this.MainGame = mainGame;
            this.GameObjects = new GameObjectManager(this.MainGame);
        }

        public void Initialize()
        {
            this.GameObjects.Add(new Enemy(this.MainGame, this));
            this.GameObjects.Add(new PlayerAvatar(this.MainGame, this));
        }

        public void FrameUpdate(GameTime gameTime, ContentManager content)
        {
            // temporary debug feature begin
            if (this.MainGame.InputManager.IsClicked(MouseButton.Left))
            {
                var newEnemy = new Enemy(this.MainGame, this);
                newEnemy.Position = this.MainGame.InputManager.MousePosition - new Vector2(50, 50);
                this.GameObjects.Add(newEnemy);
            }
            // temporary debug deature end

            this.GameObjects.FrameUpdate();
        }

        public void FrameDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            this.GameObjects.FrameDraw();
        }
    }
}
