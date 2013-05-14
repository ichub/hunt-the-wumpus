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
    public class SoundManager
    {
        public Dictionary<String, SoundEffect> Sounds { get; set; }

        public SoundManager()
        {
            this.Sounds = new Dictionary<String, SoundEffect>();
        }

        public void LoadSounds(ContentManager content)
        {
            this.Sounds.Add("buttonclick", content.Load<SoundEffect>("Sounds\\button"));
            this.Sounds.Add("menuchange", content.Load<SoundEffect>("Sounds\\menu change"));
        }

        public void PlaySound(string name)
        {
            if (this.Sounds.ContainsKey(name))
                this.Sounds[name].Play();
        }
    }
}
