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
    class RingOfSpeed : ItemObject
    {
        public RingOfSpeed(MainGame mainGame, ILevel parentLevel)
            : base(mainGame, parentLevel, "SpeedRing") { }

        public override void CollideWith(ICollideable gameObject, bool isColliding)
        {
            base.CollideWith(gameObject, isColliding);
            if (gameObject is PlayerAvatar && isColliding)
            {
                this.MainGame.Player.MaxSpeed = 10;
                this.MainGame.SoundManager.PlaySound(Sound.ItemPickup);
            }
        }

        public override void LoadContent(ContentManager content)
        {
            this.Texture = new AnimatedTexture(content.Load<Texture2D>("Textures\\Items\\SpeedRing"), 7, 56, 20);
        }
    }
}
