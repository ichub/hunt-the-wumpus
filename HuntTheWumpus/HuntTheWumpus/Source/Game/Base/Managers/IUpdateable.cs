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
    /// Interface for all game objects that are updateable.
    /// </summary>
    public interface IUpdateable : IGameObject
    {
        /// <summary>
        /// Called once per frame, before draw.
        /// </summary>
        /// <param name="gameTime"> Provides a snapshot of game time. </param>
        void Update(GameTime gameTime);
    }
}
