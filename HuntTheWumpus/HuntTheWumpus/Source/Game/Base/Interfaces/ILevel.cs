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
    /// Interface for all level objects.
    /// </summary>
    public interface ILevel
    {
        /// <summary>
        /// Manager for handling game objects.
        /// </summary>
        GameObjectManager GameObjects { get; set; }

        /// <summary>
        /// Instance of a game to which the level belongs.
        /// </summary>
        MainGame MainGame { get; set; }

        /// <summary>
        /// True if initialized; false if not.
        /// </summary>
        bool Initialized { get; set; }

        /// <summary>
        /// Method for initializing the level.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Method for updating the level.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="content"></param>
        void FrameUpdate(GameTime gameTime, ContentManager content);

        /// <summary>
        /// Method for drawing the level.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        void FrameDraw(GameTime gameTime, SpriteBatch spriteBatch);

        void OnLoad();

        void OnUnLoad();

        void Reset();
    }
}
