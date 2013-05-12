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
    /// Interface for all game objects that are clickable.
    /// </summary>
    public interface IClickable : IGameObject
    {
        /// <summary>
        /// Bounding box where a click registers for the object.
        /// </summary>
        BoundingBox ClickBox { get; set; }

        /// <summary>
        /// True if mouse was clicked on the object, but not yet released.
        /// </summary>
        bool IsClicked { get; set; }

        /// <summary>
        /// Method for handling beginning clicking.
        /// </summary>
        /// <param name="clickPosition"> Position of the mouse when the click happens. </param>
        void OnClickBegin(Vector2 clickPosition);

        /// <summary>
        /// Method for handling ending clicking.
        /// </summary>
        void OnClickRelease();
    }
}
