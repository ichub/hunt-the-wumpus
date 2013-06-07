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
    /// <summary>
    /// Interface for all the damageable objects in the game. Used exclusively for damage
    /// cooldowns, as well as for tinting the object a color when it is damaged.
    /// </summary>
    public interface IDamagable
    {
        /// <summary>
        /// Timer which counts down a set interval after being damaged, and resets
        /// the state of the damageable object when it reaches the end.
        /// </summary>
        Timer DamageTimer { get; set; }

        /// <summary>
        /// The color to tint the object when it is damaged.
        /// </summary>
        Color DamageTint { get; set; }

        /// <summary>
        /// The color to tint the object when it is not damaged.
        /// </summary>
        Color CurrentTint { get; set; }

        /// <summary>
        /// Amount of miliseconds that the object remains damaged after being damaged.
        /// </summary>
        int DamageLength { get; set; }

        /// <summary>
        /// Used for determining whether or not the object is damaged.
        /// </summary>
        bool IsDamaged { get; set; }

        /// <summary>
        /// Method that is called when the object is damaged.
        /// </summary>
        void OnDamage();
    }
}
