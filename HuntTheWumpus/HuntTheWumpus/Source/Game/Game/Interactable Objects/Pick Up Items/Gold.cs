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
    class Gold : ItemObject
    {
        public Gold(MainGame parentGame, ILevel parentLevel)
            : base(parentGame, parentLevel, "Gold") { }

        public override void CollideWith(ICollideable gameObject, bool isColliding)
        {
            base.CollideWith(gameObject, isColliding);

            if (gameObject is PlayerAvatar && isColliding)
            {
                this.MainGame.Player.Score += 50;
                this.MainGame.SoundManager.PlaySound("gold");
            }
        }

        public override void LoadContent(ContentManager content)
        {
            this.Texture = new AnimatedTexture(content.Load<Texture2D>("Textures\\Items\\Gold"));
        }
    }
}
