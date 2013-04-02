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
    class WumpusPlayLevel : ILevel
    {
        public MainGame ParentGame { get; set; }
        public GameObjectManager GameObjects { get; set; }

        public bool Initialized { get; set; }

        public WumpusPlayLevel(MainGame parentGame)
        {
            this.GameObjects = new GameObjectManager(parentGame);
            this.ParentGame = parentGame;
        }

        public void Initialize()
        {
            this.GameObjects.Add(new Button());
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
