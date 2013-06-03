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
    /// A button.
    /// </summary>
    class Button : IClickable, IUpdateable, IDrawable, IGameObject, IHoverable
    {
        public MainGame MainGame { get; set; }
        public ILevel ParentLevel { get; set; }
        public BoundingBox BoundingBox { get; set; }
        public AnimatedTexture Texture { get; set; }
        public Vector2 Position { get; set; }
        public Team ObjectTeam { get; set; }

        public bool IsClicked { get; set; }
        public bool ContentLoaded { get; set; }
        public bool IsHidden { get; set; }
        public bool IsMouseOver { get; set; }

        public Action OnClick { get; set; }

        private AnimatedTexture clickedTexture;
        private AnimatedTexture notClickedTexture;
        private AnimatedTexture mousedOverTexture;

        /// <summary>
        /// Creates a new button.
        /// </summary>
        /// <param name="mainGame"> The game to which this button belongs. </param>
        /// <param name="parentLevel"> The level to which this button belongs. </param>
        /// <param name="onClick"> The action to take when this button is clicked on. </param>
        public Button(MainGame mainGame, ILevel parentLevel, Action onClick)
        {
            this.MainGame = mainGame;
            this.ParentLevel = parentLevel;
            this.ObjectTeam = Team.None;
            this.OnClick = onClick;
        }

        public void LoadContent(ContentManager content)
        {
            this.notClickedTexture = new AnimatedTexture(content.Load<Texture2D>("Textures\\StartButton\\button_default"));
            this.clickedTexture = new AnimatedTexture(content.Load<Texture2D>("Textures\\StartButton\\button_mouseclicked"));
            this.mousedOverTexture = new AnimatedTexture(content.Load<Texture2D>("Textures\\StartButton\\button_mouseover"));
            this.Texture = this.notClickedTexture;
        }

        public void CenterOnScreen()
        {
            this.Position = new Vector2(1024, 768) - this.Texture.Size;
            this.Position /= 2;
            this.Position = Extensions.RoundVector(this.Position);
        }

        public void Update(GameTime gameTime)
        {
            this.BoundingBox = this.BoundingBox.Set2D(this.Position, this.Position + this.Texture.Size);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            this.Texture.Draw(spriteBatch, this.Position, gameTime);
        }

        public void OnClickBegin(Vector2 clickPosition)
        {
            this.Texture = this.clickedTexture;
        }

        public void OnClickRelease()
        {
            this.OnClick.Invoke();
            this.Texture = this.notClickedTexture;
            this.MainGame.SoundManager.PlaySound("menuchange");
        }

        public void OnHoverBegin()
        {
            this.Texture = this.mousedOverTexture;
        }

        public void OnHoverEnd()
        {
            this.Texture = this.notClickedTexture;
        }
    }
}
