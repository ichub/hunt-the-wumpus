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
    /// Interface for all game objects that can be initialized.
    /// </summary>
    public interface IInitializable : IGameObject
    {
        /// <summary>
        /// Value for determining whether or not the object
        /// has been initialized.
        /// </summary>
        bool Initialized { get; set; }

        /// <summary>
        /// Called at object creation.
        /// </summary>
        void Initialize();
    }
}
