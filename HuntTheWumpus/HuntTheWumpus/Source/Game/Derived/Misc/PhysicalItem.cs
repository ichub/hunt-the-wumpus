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
    class PhysicalItem : IDrawable, IUpdateable, IInitializable, ICollideable
    {
        public MainGame MainGame { get; set; }
        public ILevel ParentLevel { get; set; }
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 TextureSize { get; set; }
        public BoundingBox BoundingBox { get; set; }
        public Team ObjectTeam { get; set; }

        public bool ContentLoaded { get; set; }
        public bool Initialized { get; set; }

        private Vector2 velocity;
        private Vector2 lastPosition;
        private Room parentRoom;

        private int moveSpeed = 2;
        private bool collidedThisFrame = false;
        private bool collidedWithEnemyLastFrame = false;

        public PhysicalItem(MainGame mainGame, ILevel parentLevel)
        {
            this.MainGame = mainGame;
            this.ParentLevel = parentLevel;
            this.parentRoom = parentLevel as Room;
            this.ObjectTeam = Team.Player;
            this.Position = new Vector2(400, 400);
            this.BoundingBox = new BoundingBox();
            this.lastPosition = this.Position;
        }

        public void Move()
        {
            if (MainGame.InputManager.KeyboardState.IsKeyDown(Keys.A))
            {
                this.velocity += new Vector2(-moveSpeed, 0);
            }
            if (MainGame.InputManager.KeyboardState.IsKeyDown(Keys.W))
            {
                this.velocity += new Vector2(0, -moveSpeed);
            }
            if (MainGame.InputManager.KeyboardState.IsKeyDown(Keys.D))
            {
                this.velocity += new Vector2(moveSpeed, 0);
            }
            if (MainGame.InputManager.KeyboardState.IsKeyDown(Keys.S))
            {
                this.velocity += new Vector2(0, moveSpeed);
            }

            this.Position += velocity;
            this.velocity /= 1.2f;

            if (this.velocity.LengthSquared() > 5 * 5)
            {
                this.velocity /= this.velocity.Length();
                this.velocity *= 5;
            }
        }

        public void CollideWithWalls()
        {
            if (this.Position.X < 0)
            {
                this.Position = new Vector2(0, this.Position.Y);
                this.velocity = Vector2.Zero;
            }
            if (this.Position.Y < 0)
            {
                this.Position = new Vector2(this.Position.X, 0);
                this.velocity = Vector2.Zero;
            }
            if (this.Position.X > this.parentRoom.MainCave.CaveBounds.Width - this.TextureSize.X)
            {
                this.Position = new Vector2(this.parentRoom.MainCave.CaveBounds.Width - this.TextureSize.X, this.Position.Y);
                this.velocity = Vector2.Zero;
            }
            if (this.Position.Y > this.parentRoom.MainCave.CaveBounds.Height - this.TextureSize.Y)
            {
                this.Position = new Vector2(this.Position.X, this.parentRoom.MainCave.CaveBounds.Height - this.TextureSize.Y);
                this.velocity = Vector2.Zero;
            }
        }

        public void FireProjectile()
        {
            var projectile = new Projectile(this.MainGame, this.ParentLevel, Team.Player, "fireball_spritesheet");
            projectile.Position = this.Position + this.TextureSize / 2;

            if (MainGame.InputManager.IsClicked(Keys.Up))
            {
                projectile.Velocity = new Vector2(0, -4);
                this.ParentLevel.GameObjects.Add(projectile);
            }
            if (MainGame.InputManager.IsClicked(Keys.Down))
            {
                projectile.Velocity = new Vector2(0, 4);
                this.ParentLevel.GameObjects.Add(projectile);
            }
            if (MainGame.InputManager.IsClicked(Keys.Left))
            {
                projectile.Velocity = new Vector2(-4, 0);
                this.ParentLevel.GameObjects.Add(projectile);
            }
            if (MainGame.InputManager.IsClicked(Keys.Right))
            {
                projectile.Velocity = new Vector2(4, 0);
                this.ParentLevel.GameObjects.Add(projectile);
            }
             //*/
        }

        public void CollideWith(ICollideable gameObject, bool isColliding)
        {
            collidedThisFrame = isColliding | collidedThisFrame;

            if (gameObject is Enemy && isColliding)
            {
                if (!collidedWithEnemyLastFrame)
                {
                    this.MainGame.Player.HP--;
                    this.MainGame.Player.Score -= 50;
                    if (this.MainGame.Player.HP <= 0)
                    {
                        this.MainGame.LevelManager.CurrentLevel = new GameOverLevel(this.MainGame);
                        this.MainGame.Player.HP = 10;
                        this.MainGame.Player.Score = 0;
                    }
                }
            }

        }

        public void Initialize()
        {
            this.BoundingBox = Extensions.Box2D(this.Position, this.Position + this.TextureSize);
        }

        public void LoadContent(ContentManager content)
        {
            this.Texture = content.Load<Texture2D>("Textures\\player");
            this.TextureSize = new Vector2(this.Texture.Width, this.Texture.Height);
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Position, Color.White);
        }
    }
}
