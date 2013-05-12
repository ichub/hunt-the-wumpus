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
    class Button : IClickable, IUpdateable, IDrawable, IGameObject
    {
        public MainGame MainGame { get; set; }
        public ILevel ParentLevel { get; set; }
        public BoundingBox ClickBox { get; set; }
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 TextureSize { get; set; }

        public bool IsClicked { get; set; }
        public bool ContentLoaded { get; set; }

        public Button(MainGame mainGame, ILevel parentLevel)
        {
            this.MainGame = mainGame;
            this.ParentLevel = parentLevel;

            this.Position = new Vector2(0, 0);
        }

        public void LoadContent(ContentManager content)
        {
            this.Texture = content.Load<Texture2D>("box");
        }

        public void Update(GameTime gameTime)
        {
            this.Position += new Vector2(1, 1);
            this.ClickBox = this.ClickBox.Set2D(this.Position, this.Position + this.TextureSize);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Position, Color.White);
        }

        public void OnClickBegin(Vector2 clickPosition)
        {
            this.ParentLevel.GameObjects.Remove(this);
        }

        public void OnClickRelease()
        {
        }
    }
}
