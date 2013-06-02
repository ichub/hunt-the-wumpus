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
    class Enemy : IDrawable, IInitializable, IUpdateable, ICollideable
    {
        public MainGame MainGame { get; set; }
        public ILevel ParentLevel { get; set; }
        public AnimatedTexture Texture { get; set; }
        public Vector2 Position { get; set; }
        public BoundingBox BoundingBox { get; set; }
        public Team ObjectTeam { get; set; }

        public bool ContentLoaded { get; set; }
        public bool Initialized { get; set; }

        private Room parentRoom;
        private Vector2 velocity;
        private Vector2 lastPosition;
        private Timer hurtTimer;
        private Color tint;
        private int hp = 3;
        private bool isColliding = false;
        private Timer randomMovement;

        public Enemy(MainGame mainGame, ILevel parentLevel)
        {
            this.MainGame = mainGame;
            this.ParentLevel = parentLevel;
            this.parentRoom = parentLevel as Room;
            this.ObjectTeam = Team.Enemy;
            this.Position = new Vector2(300, 300);
            this.BoundingBox = new BoundingBox();
            this.velocity = Vector2.Zero;
            this.tint = Color.White;
            this.randomMovement = new Timer(100);
            this.randomMovement.Elapsed += (a, b) => 
                {
                    this.velocity += Extensions.RandomVector(2);
                };
            this.randomMovement.Start();
        }

        public void CollideWithWalls()
        {
            if (this.Position.X < 0)
            {
                this.Position = new Vector2(0, this.Position.Y);
            }
            if (this.Position.Y < 0)
            {
                this.Position = new Vector2(this.Position.X, 0);
            }
            if (this.Position.X > this.parentRoom.MainCave.CaveBounds.Width - this.Texture.Size.X)
            {
                this.Position = new Vector2(this.parentRoom.MainCave.CaveBounds.Width - this.Texture.Size.X, this.Position.Y);
            }
            if (this.Position.Y > this.parentRoom.MainCave.CaveBounds.Height - this.Texture.Size.Y)
            {
                this.Position = new Vector2(this.Position.X, this.parentRoom.MainCave.CaveBounds.Height - this.Texture.Size.Y);
            }

        }

        public void CollideWithOppositeVector()
        {
            this.velocity = Vector2.Zero;
            this.Position += (this.lastPosition - this.Position) * 2;
        }

        private void GoToPlayer()
        {
            List<PlayerAvatar> L = this.ParentLevel.GameObjects.GetObjectsByType<PlayerAvatar>();
            if (L.Any())
            {
                Vector2 direction = L[0].Position - this.Position;
                direction.Normalize();
                this.velocity += direction / 50;
            }
        }

        public void CollideWith(ICollideable gameObject, bool isCollided)
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
                            this.tint = new Color(1.0f, 0f, 0f);

                            this.hurtTimer = new Timer(125);
                            this.hurtTimer.Elapsed += this.ResetTint;
                            this.hurtTimer.Start();

                            this.MainGame.SoundManager.PlaySound("grunt");
                            if (this.hp < 0)
                            {
                                this.MainGame.Player.Score += 10;
                                this.ParentLevel.GameObjects.Remove(this);
                                OnDeath();
                            }
                            this.ParentLevel.GameObjects.Remove(gameObject);
                            this.velocity += projectile.Velocity;
                        }
                    }
                this.isColliding = true;
            }
        }

        private void ResetTint(object sender, EventArgs e)
        {
            this.tint = Color.White;
            this.hurtTimer = null;
        }

        private void SpawnGold(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                this.ParentLevel.GameObjects.Add(new Gold(this.MainGame, this.ParentLevel) { Position = this.Position + this.Texture.Size / 2 });
            }
        }

        private void SpawnSpecial()
        {
            if (Extensions.RandomBool(1.0 / 2))
                this.ParentLevel.GameObjects.Add(new RingOfSpeed(this.MainGame, this.ParentLevel) { Position = this.Position + this.Texture.Size / 2 });
        }

        private void OnDeath()
        {
            this.SpawnSpecial();
            this.SpawnGold(5);
        }

        public void Initialize()
        {
            this.BoundingBox = Extensions.Box2D(this.Position, this.Position + this.Texture.Size);
        }

        public void LoadContent(ContentManager content)
        {
            this.Texture = new AnimatedTexture(content.Load<Texture2D>("Textures\\Enemies\\bat_spritesheet"), 3, 199, 100, 10);
        }

        public void Update(GameTime gameTime)
        {
            this.lastPosition = this.Position;
            this.BoundingBox = Extensions.Box2D(this.Position, this.Position + this.Texture.Size);
            this.CollideWithWalls();
            this.isColliding = false;
            this.GoToPlayer();
            this.Position += this.velocity;
            this.velocity /= 1.1f;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            this.Texture.Draw(spriteBatch, this.Position, gameTime, this.tint);
        }
    }
}
