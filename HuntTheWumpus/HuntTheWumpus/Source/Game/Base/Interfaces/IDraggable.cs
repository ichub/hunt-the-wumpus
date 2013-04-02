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
    /// Interface for all game objects that are draggable.
    /// </summary>
    public interface IDraggable
    {
        /// <summary>
        /// Box sorrounding the object where it is possible to click and drag from.
        /// </summary>
        BoundingBox GrabBox { get; set; }

        /// <summary>
        /// Position relative to the position of the object where the drag started. Null if not currently dragged.
        /// </summary>
        Nullable<Vector2> GrabPosition { get; set; }

        /// <summary>
        /// True if dragging; false if not.
        /// </summary>
        bool IsDragging { get; set; }

        /// <summary>
        /// Method for handling starting dragging.
        /// </summary>
        /// <param name="touchLocation"> Position where the mouse was clicked. </param>
        void OnDragStart(Vector2 touchLocation);

        /// <summary>
        /// Method for handling continued dragging.
        /// </summary>
        /// <param name="touchLocation"></param>
        void OnDrag(Vector2 touchLocation);

        /// <summary>
        /// Method for handling ending dragging.
        /// </summary>
        void OnDragEnd();
    }
}
