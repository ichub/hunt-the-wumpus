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
using System.Timers;

namespace HuntTheWumpus.Source
{
    public interface IDamagable
    {
        Timer DamageTimer { get; set; }
        Color DamageTint { get; set; }
        Color CurrentTint { get; set; }

        int DamageLength { get; set; }
        bool IsDamaged { get; set; }
    }
}
