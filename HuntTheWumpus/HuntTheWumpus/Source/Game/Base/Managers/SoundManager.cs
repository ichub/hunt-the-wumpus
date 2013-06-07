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
        /// <summary>
        /// All the avaliable sounds.
        /// </summary>
        public Dictionary<Sound, SoundEffect> Sounds { get; private set; }

        /// <summary>
        /// The ambience of the cave.
        /// </summary>
        private SoundEffectInstance ambience;
        
        /// <summary>
        /// Creates a new sound manager.
        /// </summary>
        public SoundManager()
        {
            this.Sounds = new Dictionary<Sound, SoundEffect>();
        }

        /// <summary>
        /// Loads all the sounds needed for the game.
        /// </summary>
        /// <param name="content"> Content manager with which to load in sounds. </param>
        public void LoadSounds(ContentManager content)
        {
            // initializes the sounds
            this.Sounds.Add(Sound.ButtonClick, content.Load<SoundEffect>("Sounds\\button"));
            this.Sounds.Add(Sound.MenuChange, content.Load<SoundEffect>("Sounds\\menu change"));
            this.Sounds.Add(Sound.Grunt, content.Load<SoundEffect>("Sounds\\grunt"));
            this.Sounds.Add(Sound.ItemPickup, content.Load<SoundEffect>("Sounds\\item pickup"));
            this.Sounds.Add(Sound.CaveAmbience, content.Load<SoundEffect>("Sounds\\caveambience"));
            this.Sounds.Add(Sound.DeathScream, content.Load<SoundEffect>("Sounds\\DeathScream"));

            // playes the cave ambience
            this.ambience = this.Sounds[Sound.CaveAmbience].CreateInstance();
            this.ambience.IsLooped = true;
            this.ambience.Play();
        }

        /// <summary>
        /// Plays the given sound.
        /// </summary>
        /// <param name="name"> Name of the sound to play. </param>
        public void PlaySound(Sound name)
        {
            if (this.Sounds.ContainsKey(name))
            {
                this.Sounds[name].Play();
            }
        }
    }

    /// <summary>
    /// Names of all the sounds that can be played by the game.
    /// </summary>
    public enum Sound
    {
        ButtonClick,
        MenuChange,
        Grunt,
        ItemPickup,
        CaveAmbience,
        DeathScream,
    }
}
