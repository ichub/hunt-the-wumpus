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
    /// <summary>
    /// Class which represents the player in each individual room.
    /// </summary>
    class PlayerAvatar : BaseGameObject
    {
        private bool collidedThisFrame = false;
        private bool collidedWithEnemyLastFrame = false;

        public PlayerAvatar(MainGame mainGame, ILevel parentLevel)
            : base(mainGame, parentLevel)
        {
            this.ObjectTeam = Team.Player;
            this.Position = new Vector2(400, 400);
        }

        public void Move()
        {
            if (MainGame.InputManager.KeyboardState.IsKeyDown(Keys.A))
            {
                this.Velocity += new Vector2(-this.MainGame.Player.SpeedDelta, 0);
            }
            if (MainGame.InputManager.KeyboardState.IsKeyDown(Keys.W))
            {
                this.Velocity += new Vector2(0, -this.MainGame.Player.SpeedDelta);
            }
            if (MainGame.InputManager.KeyboardState.IsKeyDown(Keys.D))
            {
                this.Velocity += new Vector2(this.MainGame.Player.SpeedDelta, 0);
            }
            if (MainGame.InputManager.KeyboardState.IsKeyDown(Keys.S))
            {
                this.Velocity += new Vector2(0, this.MainGame.Player.SpeedDelta);
            }

            this.Position += this.Velocity;
            this.Velocity /= 1.2f;

            if (this.Velocity.LengthSquared() > this.MainGame.Player.MaxSpeed * this.MainGame.Player.MaxSpeed)
            {
                this.Velocity /= this.Velocity.Length();
                this.Velocity *= this.MainGame.Player.MaxSpeed;
            }
        }

        public override void CollideWithLevelBounds()
        {
            this.Velocity = -this.Velocity * 2;
            this.Velocity = Vector2.Zero;
            this.Position = this.LastPosition;
        }

        public void FireProjectile()
        {
            Vector2 position = this.Position + this.Texture.Size / 2;

            if (MainGame.InputManager.IsClicked(Keys.Up))
                this.ParentLevel.GameObjects.Add(new Projectile(this.MainGame, this.ParentLevel, Direction.Up) { Position = position });

            else if (MainGame.InputManager.IsClicked(Keys.Down))
                this.ParentLevel.GameObjects.Add(new Projectile(this.MainGame, this.ParentLevel, Direction.Down) { Position = position });

            else if (MainGame.InputManager.IsClicked(Keys.Left))
                this.ParentLevel.GameObjects.Add(new Projectile(this.MainGame, this.ParentLevel, Direction.Left) { Position = position });

            else if (MainGame.InputManager.IsClicked(Keys.Right))
                this.ParentLevel.GameObjects.Add(new Projectile(this.MainGame, this.ParentLevel, Direction.Right) { Position = position });
        }

        public override void CollideWith(ICollideable gameObject, bool isColliding)
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
                        this.MainGame.Player.HP = 3;
                        this.MainGame.Player.Score = 0;
                    }
                }
            }

        }

        public override void Initialize()
        {
            this.BoundingBox = Extensions.Box2D(this.Position, this.Position + this.Texture.Size);
        }

        public override void LoadContent(ContentManager content)
        {
            this.Texture = new AnimatedTexture(content.Load<Texture2D>("Textures\\player"));
        }

        public override void Update(GameTime gameTime)
        {
            this.FireProjectile();
            this.Move();
            this.BoundingBox = Extensions.Box2D(this.Position, this.Position + this.Texture.Size);
            collidedWithEnemyLastFrame = collidedThisFrame;
            collidedThisFrame = false;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            this.Texture.Draw(spriteBatch, this.Position, gameTime);
        }
        private void UpdatePitRoom()
        {
            if (this.MainGame.LevelManager.CurrentLevel is Room && (this.MainGame.LevelManager.CurrentLevel as Room).RoomType == RoomType.Pit && this.MainGame.LevelManager.CurrentLevel.Initialized)
            {
                this.MainGame.LevelManager.CurrentLevel = new GameOverLevel(this.MainGame);
            }
        }
    }
}
