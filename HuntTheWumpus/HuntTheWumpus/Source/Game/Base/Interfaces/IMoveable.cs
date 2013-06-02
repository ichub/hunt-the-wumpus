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
    public interface IMoveable
    {
        Vector2 Position { get; set; }
        Vector2 Velocity { get; set; }

        float SpeedDampening { get; set; }
    }
}
