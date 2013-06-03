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
    class Wumpus : Enemy
    {
        private AnimatedTexture facingLeft;
        private AnimatedTexture facingRight;
        private AnimatedTexture facingUp;
        private AnimatedTexture facingDown;

        public Wumpus(MainGame mainGame, ILevel parentLevel)
            : base(mainGame, parentLevel)
        {
            this.ObjectTeam = Team.Enemy;
        }

        public override void LoadContent(ContentManager content)
        {
            this.facingLeft = new AnimatedTexture(content.Load<Texture2D>("Textures\\Enemies\\Wumpus\\wumpus_leftspritesheet"), 4, 1000, 1000, 1);
            this.facingRight = new AnimatedTexture(content.Load<Texture2D>("Textures\\Enemies\\Wumpus\\wumpus_rightspritesheet"), 4, 1000, 1000, 1);
            this.facingUp = new AnimatedTexture(content.Load<Texture2D>("Textures\\Enemies\\Wumpus\\wumpus_backspritesheet"), 4, 1000, 1000, 1);
            this.facingDown = new AnimatedTexture(content.Load<Texture2D>("Textures\\Enemies\\Wumpus\\wumpus_frontspritesheet"), 4, 1000, 1000, 1);
            this.Texture = this.facingDown;
        }

        private void ChoseTexture()
        {
            switch (Extensions.GetDirection(this.Velocity))
            {
                case Direction.Up:
                    this.Texture = this.facingUp;
                    break;
                case Direction.Down:
                    this.Texture = this.facingDown;
                    break;
                case Direction.Left:
                    this.Texture = this.facingLeft;
                    break;
                case Direction.Right:
                    this.Texture = this.facingRight;
                    break;
            }
        }

        public override void Update(GameTime gameTime)
        {
            this.ChoseTexture();
            base.Update(gameTime);
        }
    }
}
