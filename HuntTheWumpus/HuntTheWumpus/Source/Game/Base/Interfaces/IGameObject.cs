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
    /// Interface from which all game objects inherit.
    /// </summary>
    public interface IGameObject
    {
        /// <summary>
        /// The game to which this object belongs to.
        /// </summary>
        MainGame MainGame { get; set; }

        /// <summary>
        /// The level to which this object belongs to.
        /// </summary>
        ILevel ParentLevel { get; set; }

        /// <summary>
        /// The team to which this object belongs to.
        /// </summary>
        Team ObjectTeam { get; set; }
    }
}
