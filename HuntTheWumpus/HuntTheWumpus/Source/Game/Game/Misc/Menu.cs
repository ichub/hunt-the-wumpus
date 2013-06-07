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
    class Menu : IDrawable, IUpdateable
    {
        public MainGame MainGame { get; set; }
        public ILevel ParentLevel { get; set; }
        public AnimatedTexture Texture { get; set; }
        public Vector2 Position { get; set; }
        public Team ObjectTeam { get; set; }

        public bool IsHidden { get; set; }
        public bool ContentLoaded { get; set; }
        /// <summary>
        /// Draws the menu
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            this.Texture.Draw(spriteBatch, this.Position, gameTime);
        }
        /// <summary>
        /// Loads the needed content
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            this.Texture = new AnimatedTexture(content.Load<Texture2D>("Textures\\menu"));
        }
        /// <summary>
        /// Does nothing
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            return;
        }
    }
}
