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
        /// <summary>
        /// Timer that is called every 30 milliseconds, and adds a random velocity.
        /// Used to make movement look random.
        /// </summary>
        private Timer randomMovement;

        /// <summary>
        /// Whether or not the enemy is colliding.
        /// </summary>
        private bool isColliding = false;

        /// <summary>
        /// The health of the wumpus.
        /// </summary>
        protected int HP = 3;

        /// <summary>
        /// Creates a new enemy.
        /// </summary>
        /// <param name="mainGame"> The game to which the object belongs. </param>
        /// <param name="parentLevel"> The level to which the object belongs. </param>
        public Enemy(MainGame mainGame, ILevel parentLevel)
            : base(mainGame, parentLevel)
        {
            this.ObjectTeam = Team.Enemy;
            this.Position = new Vector2(300, 300);

            // creates the movement timer that randomly moves the enemy.
            this.randomMovement = new Timer(100);
            this.randomMovement.Elapsed += (a, b) =>
                {
                    this.Velocity += Helper.RandomVector(2);
                };

            // starts moving the enemy randomly.
            this.randomMovement.Start();
        }

        /// <summary>
        /// Moves towards the player
        /// </summary>
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

        /// <summary>
        /// Collides with the projectile, and if the health is less than 0,
        /// it kills itsself and romoves itsself from the game.
        /// </summary>
        /// <param name="gameObject"> The object to collide with. </param>
        /// <param name="isCollided"> Whether or not the object is colliding. </param>
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
                            this.HP--;
                            this.ParentLevel.GameObjects.Damage(this);

                            this.MainGame.SoundManager.PlaySound(Sound.Grunt);
                            if (this.HP < 0)
                            {
                                this.MainGame.Player.AddMiscScore();
                                this.Remove();
                                OnDeath();
                            }
                            this.Velocity += projectile.Velocity;
                        }
                    }
                this.isColliding = true;
            }
        }

        /// <summary>
        /// spawns gems
        /// </summary>
        /// <param name="amount"></param>
        private void SpawnGems(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                this.ParentLevel.GameObjects.Add(new Gem(this.MainGame, this.ParentLevel) { Position = this.Position + this.Texture.Size * this.Texture.Scale / 2 });
            }
        }

        /// <summary>
        /// spawns item drops
        /// </summary>
        private void SpawnSpecial()
        {
            if (Helper.RandomBool(1.0 / 2))
                this.ParentLevel.GameObjects.Add(new RingOfSpeed(this.MainGame, this.ParentLevel) { Position = this.Position + this.Texture.Size * this.Texture.Scale / 2 });
        }

        /// <summary>
        /// Called on death
        /// </summary>
        private void OnDeath()
        {
            this.SpawnSpecial();
            this.SpawnGems(5);
        }

        /// <summary>
        /// Updates the class
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            this.isColliding = false;
            this.GoToPlayer();
            this.Position += this.Velocity;
            this.Velocity /= this.SpeedDampening;
        }
    }
}
