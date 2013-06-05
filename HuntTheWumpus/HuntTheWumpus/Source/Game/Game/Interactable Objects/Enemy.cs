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
using System.Timers;

namespace HuntTheWumpus.Source
{
    public abstract class Enemy : BaseGameObject
    {
        private int hp = 3;
        private bool isColliding = false;
        private Timer randomMovement;

        public Enemy(MainGame mainGame, ILevel parentLevel)
            : base(mainGame, parentLevel)
        {
            this.ObjectTeam = Team.Enemy;
            this.Position = new Vector2(300, 300);

            this.randomMovement = new Timer(100);
            this.randomMovement.Elapsed += (a, b) =>
                {
                    this.Velocity += Extensions.RandomVector(2);
                };

            this.randomMovement.Start();
        }

        private void GoToPlayer()
        {
            List<PlayerAvatar> L = this.ParentLevel.GameObjects.GetObjectsByType<PlayerAvatar>();
            if (L.Any())
            {
                Vector2 direction = L[0].Position - this.Position;
                direction.Normalize();
                this.Velocity += direction / 50;
            }
        }

        public override void CollideWith(ICollideable gameObject, bool isCollided)
        {
            if (gameObject is Projectile && isCollided)
            {
                var projectile = gameObject as Projectile;
                if (!projectile.HasCollided)
                    if (projectile.ObjectTeam == Team.Player)
                    {
                        if (!this.isColliding)
                        {
                            this.hp--;
                            this.ParentLevel.GameObjects.Damage(this);

                            this.MainGame.SoundManager.PlaySound(Sound.Grunt);
                            if (this.hp < 0)
                            {
                                this.MainGame.Player.Score += 10;
                                this.Remove();
                                OnDeath();
                            }
                            this.Velocity += projectile.Velocity;
                        }
                    }
                this.isColliding = true;
            }
        }

        private void SpawnGems(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                this.ParentLevel.GameObjects.Add(new Gem(this.MainGame, this.ParentLevel) { Position = this.Position + this.Texture.Size * this.Texture.Scale/ 2 });
            }
        }

        private void SpawnSpecial()
        {
            if (Extensions.RandomBool(1.0 / 2))
                this.ParentLevel.GameObjects.Add(new RingOfSpeed(this.MainGame, this.ParentLevel) { Position = this.Position + this.Texture.Size * this.Texture.Scale / 2 });
        }

        private void OnDeath()
        {
            this.SpawnSpecial();
            this.SpawnGems(5);
        }

        public override void Update(GameTime gameTime)
        {
            this.isColliding = false;
            this.GoToPlayer();
            this.Position += this.Velocity;
            this.Velocity /= this.SpeedDampening;
        }
    }
}
