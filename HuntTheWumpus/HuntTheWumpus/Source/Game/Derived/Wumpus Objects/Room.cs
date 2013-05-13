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
            var a = new Enemy(this.MainGame, this);
            a.Position = new Vector2(200, 200);
            this.GameObjects.Add(a);
            this.GameObjects.Add(new PlayerAvatar(this.MainGame, this));
        }

        public void FrameUpdate(GameTime gameTime, ContentManager content)
        {
            this.GameObjects.FrameUpdate();
        }

        public void FrameDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            this.GameObjects.FrameDraw();
        }
    }
}
