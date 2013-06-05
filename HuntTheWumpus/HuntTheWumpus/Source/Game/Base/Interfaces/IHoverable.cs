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
    /// Interface for all objects that need to react to being hovored
    /// over by the mouse.
    /// </summary>
    interface IHoverable
    {
        /// <summary>
        /// Bounding box to check collision with the mouse.
        /// </summary>
        BoundingBox BoundingBox { get; set; }

        /// <summary>
        /// Used to determine whether or not the mouse is currently over the object.
        /// </summary>
        bool IsMouseOver { get; set; }

        /// <summary>
        /// Method called when the mouse first enters the object.
        /// </summary>
        void OnHoverBegin();

        /// <summary>
        /// Method called when the mouse exits the object.
        /// </summary>
        void OnHoverEnd();
    }
}
