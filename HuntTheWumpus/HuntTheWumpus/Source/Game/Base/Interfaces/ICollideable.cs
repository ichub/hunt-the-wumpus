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
    /// Interface for handling collisions between objects.
    /// </summary>
    interface ICollideable : IGameObject
    {
        /// <summary>
        /// Current position of the object.
        /// </summary>
        Vector2 Position { get; set; }

        /// <summary>
        /// Position of the object during the last frame.
        /// </summary>
        Vector2 LastPosition { get; set; }

        /// <summary>
        /// A list of bounding boxes with which other objects can collide.
        /// </summary>
        BoundingBox BoundingBox { get; set; }

        /// <summary>
        /// Method for handling collisions between objects.
        /// </summary>
        /// <param name="gameObject"> Another collideable object. </param>
        /// <param name="isColliding"> Whether or not this object is colliding witht the other one. </param>
        void CollideWith(ICollideable gameObject, bool isColliding);

        /// <summary>
        /// Method for handling collisions between the object and the level.
        /// </summary>
        void CollideWithLevelBounds();
    }
}
