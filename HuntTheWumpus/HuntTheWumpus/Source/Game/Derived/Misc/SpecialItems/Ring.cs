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
    public enum RingType
    {
        Standard,
        Upgraded,
        Ultimate,
    }

    public class Ring 
    {
        public int Chance { get; set; }

        public Ring(RingType type)
        {
            if (type == RingType.Standard) 
                this.Chance = 20;
            else if (type == RingType.Upgraded) 
                this.Chance = 30;
            else if (type == RingType.Ultimate) 
                this.Chance = 50;
        }
    }
}
