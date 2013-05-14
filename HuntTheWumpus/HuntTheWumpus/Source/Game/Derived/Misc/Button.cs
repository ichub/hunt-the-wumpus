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
        public Team ObjectTeam { get; set; }

        public bool IsClicked { get; set; }
        public bool ContentLoaded { get; set; }

        public Action OnClick { get; set; }
        public string Text { get; set; }

        public Button(MainGame mainGame, ILevel parentLevel, Action onClick, string text)
        {
            this.MainGame = mainGame;
            this.ParentLevel = parentLevel;
            this.ObjectTeam = Team.None;
            this.Position = new Vector2(100, 100);
            this.Text = text;
            this.OnClick = onClick;
        }

        public void LoadContent(ContentManager content)
        {
            this.Texture = content.Load<Texture2D>("Textures\\box");
            this.TextureSize = new Vector2(this.Texture.Width, this.Texture.Height);
        }

        public void Update(GameTime gameTime)
        {
            this.ClickBox = this.ClickBox.Set2D(this.Position, this.Position + this.TextureSize);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Position, Color.White);
            this.MainGame.TextManager.DrawText(this.Position + new Vector2(15, 15), this.Text, Color.White, false);
        }

        public void OnClickBegin(Vector2 clickPosition)
        {
        }

        public void OnClickRelease()
        {
            OnClick();
            this.MainGame.SoundManager.PlaySound("buttonclick");
        }
    }
}
