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
    class Bat : Enemy
    {
        public Bat(MainGame mainGame, ILevel parentLevel)
            : base(mainGame, parentLevel)
        {
        }

        public override void LoadContent(ContentManager content)
        {
            this.Texture = new AnimatedTexture(content.Load<Texture2D>("Textures\\Enemies\\Bat\\bat_spritesheet"), 3, 199, 10, 1, Helper.EnemyLayer);
        }
    }
}
