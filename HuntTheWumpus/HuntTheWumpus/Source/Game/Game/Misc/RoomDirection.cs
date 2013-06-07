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
    /// Direction of the arrow: more complex than normal "Direction"
    /// </summary>
    public enum RoomDirection
    {
        North,
        NorthEast,
        SouthEast,
        South,
        SouthWest,
        NorthWest,
    }
}
