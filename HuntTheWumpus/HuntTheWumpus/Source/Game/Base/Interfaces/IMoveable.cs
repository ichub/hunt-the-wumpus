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
    /// Objects that should be able to move in space.
    /// </summary>
    public interface IMoveable
    {
        /// <summary>
        /// The position of the object.
        /// </summary>
        Vector2 Position { get; set; }

        /// <summary>
        /// The velocit of the object. This is added to the position at the end 
        /// of every frame by default.
        /// </summary>
        Vector2 Velocity { get; set; }

        /// <summary>
        /// The factor by which the velocity is multiplied at the end of the frame. 
        /// Used for slowing objects down slowly.
        /// </summary>
        float SpeedDampening { get; set; }
    }
}
