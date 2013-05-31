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
    /// Class responsible for loading and playing sounds.
    /// </summary>
    public class SoundManager
    {
        public Dictionary<String, SoundEffect> Sounds { get; private set; }

        /// <summary>
        /// Creates a new sound manager.
        /// </summary>
        public SoundManager()
        {
            this.Sounds = new Dictionary<String, SoundEffect>();
        }

        /// <summary>
        /// Loads all the sounds needed for the game.
        /// </summary>
        /// <param name="content"> Content manager with which to load in sounds. </param>
        public void LoadSounds(ContentManager content)
        {
            this.Sounds.Add("buttonclick", content.Load<SoundEffect>("Sounds\\button"));
            this.Sounds.Add("menuchange", content.Load<SoundEffect>("Sounds\\menu change"));
            this.Sounds.Add("grunt", content.Load<SoundEffect>("Sounds\\grunt"));
            this.Sounds.Add("gold", content.Load<SoundEffect>("Sounds\\item pickup"));
        }

        /// <summary>
        /// Plays  the given sound.
        /// </summary>
        /// <param name="name"> Name of the sound to play. </param>
        public void PlaySound(string name)
        {
            if (this.Sounds.ContainsKey(name))
                this.Sounds[name].Play();
        }
    }
}
