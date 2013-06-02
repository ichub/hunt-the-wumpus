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
    class Projectile : BaseGameObject
    {
        public bool HasCollided { get; private set; }

        public Projectile(MainGame mainGame, ILevel parentLevel, Direction direction)
            : base(mainGame, parentLevel)
        {
            this.SpeedModifier = 7;
            this.ObjectTeam = Team.Player;
            this.SpeedDampening = 1;
            this.Position = new Vector2(100, 100);
            SetVelocity(direction);
        }

        private void SetVelocity(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    this.Velocity = new Vector2(0, -1);
                    break;
                case Direction.Right:
                    this.Velocity = new Vector2(1, 0);
                    break;
                case Direction.Down:
                    this.Velocity = new Vector2(0, 1);
                    break;
                case Direction.Left:
                    this.Velocity = new Vector2(-1, 0);
                    break;
            }
        }

        public override void CollideWith(ICollideable gameObject, bool isCollided)
        {
            if (isCollided)
            {
                if (gameObject is Enemy)
                {
                    this.HasCollided = true;
                }
            }
        }

        public override void CollideWithLevelBounds()
        {
            this.ParentLevel.GameObjects.Remove(this);
        }

        public override void LoadContent(ContentManager content)
        {
            this.Texture = new AnimatedTexture(content.Load<Texture2D>("Textures\\fireball_spritesheet"), 5, 20, 20, 60);
        }
    }
}
