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
    class PlayerAvatar : IEntity
    {
        public MainGame MainGame { get; set; }
        public ILevel ParentLevel { get; set; }
        public AnimatedTexture Texture { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 LastPosition { get; set; }
        public Vector2 Velocity { get; set; }
        public BoundingBox BoundingBox { get; set; }
        public Team ObjectTeam { get; set; }

        public bool ContentLoaded { get; set; }
        public bool Initialized { get; set; }
        public float SpeedDampening { get; set; }

        private Room parentRoom;

        private int moveSpeed = 2;
        private bool collidedThisFrame = false;
        private bool collidedWithEnemyLastFrame = false;

        public PlayerAvatar(MainGame mainGame, ILevel parentLevel)
        {
            this.MainGame = mainGame;
            this.ParentLevel = parentLevel;
            this.parentRoom = parentLevel as Room;
            this.ObjectTeam = Team.Player;
            this.Position = new Vector2(400, 400);
            this.BoundingBox = new BoundingBox();
        }

        public void Move()
        {
            if (MainGame.InputManager.KeyboardState.IsKeyDown(Keys.A))
            {
                this.Velocity += new Vector2(-moveSpeed, 0);
            }
            if (MainGame.InputManager.KeyboardState.IsKeyDown(Keys.W))
            {
                this.Velocity += new Vector2(0, -moveSpeed);
            }
            if (MainGame.InputManager.KeyboardState.IsKeyDown(Keys.D))
            {
                this.Velocity += new Vector2(moveSpeed, 0);
            }
            if (MainGame.InputManager.KeyboardState.IsKeyDown(Keys.S))
            {
                this.Velocity += new Vector2(0, moveSpeed);
            }

            this.Position += this.Velocity;
            this.Velocity /= 1.2f;

            if (this.Velocity.LengthSquared() > this.MainGame.Player.MaxSpeed * this.MainGame.Player.MaxSpeed)
            {
                this.Velocity /= this.Velocity.Length();
                this.Velocity *= this.MainGame.Player.MaxSpeed;
            }
        }

        public void CollideWithLevelBounds()
        {
            this.Velocity = -this.Velocity * 2;
            this.Velocity = Vector2.Zero;
            this.Position = this.LastPosition;
        }

        public void FireProjectile()
        {
            Projectile projectile = new Projectile(this.MainGame, this.ParentLevel, Team.Player, "fireball_spritesheet");
            projectile.Position = this.Position + this.Texture.Size / 2;

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
                        this.MainGame.Player.HP = 3;
                        this.MainGame.Player.Score = 0;
                    }
                }
            }

        }

        public void Initialize()
        {
            this.BoundingBox = Extensions.Box2D(this.Position, this.Position + this.Texture.Size);
        }

        public void LoadContent(ContentManager content)
        {
            this.Texture = new AnimatedTexture(content.Load<Texture2D>("Textures\\player"));
        }

        public void Update(GameTime gameTime)
        {
            this.FireProjectile();
            this.Move();
            this.BoundingBox = Extensions.Box2D(this.Position, this.Position + this.Texture.Size);

            collidedWithEnemyLastFrame = collidedThisFrame;
            collidedThisFrame = false;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            this.Texture.Draw(spriteBatch, this.Position, gameTime);
        }

        public void MoveRoom(int newRoom)
        {
            if (newRoom >= Cave.NumberOfRooms)
                return;
            this.parentRoom = this.MainGame.LevelManager.GameCave.Rooms[newRoom];
        }
    }
}
