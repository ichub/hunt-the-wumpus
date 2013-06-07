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
    /// Interface for all game objects that can be drawn on screen.
    /// </summary>
    public interface IDrawable : IGameObject
    {
        /// <summary>
        /// Texture that is used to draw the object.
        /// </summary>
        AnimatedTexture Texture { get; set; }

        /// <summary>
        /// Position of the object.
        /// </summary>
        Vector2 Position { get; set; }

        /// <summary>
        /// Value that is used to determine whether or not the content for
        /// the object has been loaded.
        /// </summary>
        bool ContentLoaded { get; set; }

        /// <summary>
        /// Whether or not the object is hidden. If it's hidden, it's not drawn.
        /// </summary>
        bool IsHidden { get; set; }

        float Layer { get; set; }

        /// <summary>
        /// Called at object creation to load content.
        /// </summary>
        /// <param name="content"> ContentManager used to load the content. </param>
        void LoadContent(ContentManager content);

        /// <summary>
        /// Called once per frame to draw the object.
        /// </summary>
        /// <param name="gameTime"> Provides a game time snapshot. </param>
        /// <param name="spriteBatch"> Used for drawing a texture on screen. </param>
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
