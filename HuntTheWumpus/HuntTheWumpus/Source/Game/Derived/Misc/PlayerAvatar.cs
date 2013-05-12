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
    /// Class which represents the player in each individual room.
    /// </summary>
    class PlayerAvatar : IDrawable, IUpdateable, IInitializable, IGameObject
    {
        public MainGame MainGame { get; set; }
        public ILevel ParentLevel { get; set; }
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 TextureSize { get; set; }

        public bool ContentLoaded { get; set; }
        public bool Initialized { get; set; }

        public PlayerAvatar(MainGame mainGame, ILevel parentLevel)
        {
            this.MainGame = mainGame;
            this.ParentLevel = parentLevel;

            this.Position = new Vector2(0, 0);
        }

        public void Initialize()
        {
        }

        public void LoadContent(ContentManager content)
        {
            this.Texture = content.Load<Texture2D>("player");
            this.TextureSize = new Vector2(this.Texture.Width, this.Texture.Height);
        }

        public void Update(GameTime gameTime)
        {
            this.Position += new Vector2(1, 1);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Position, Color.White);
        }
    }
}
