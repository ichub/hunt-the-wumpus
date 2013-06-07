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

        private AnimatedTexture playerBaseNotMoving;
        private AnimatedTexture playerBaseLeftSpriteSheet;
        private AnimatedTexture playerBaseRightSpriteSheet;
        private AnimatedTexture playerBaseDownSpriteSheet;
        private AnimatedTexture playerBaseBackSpriteSheet;

        public PlayerAvatar(MainGame mainGame, ILevel parentLevel)
            : base(mainGame, parentLevel)
        {
            this.ObjectTeam = Team.Player;
            this.Position = new Vector2(400, 400);
            this.DamageLength = 250;
            this.DamageTint = Color.Red;
            this.CurrentTint = Color.White;
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
            Vector2 position = this.Position + this.Texture.Size * this.Texture.Scale / 2;

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
            collidedThisFrame = isColliding || collidedThisFrame;

            if (gameObject is Enemy && isColliding)
            {
                if (!collidedWithEnemyLastFrame)
                {
                    this.MainGame.Player.HP--;
                    this.MainGame.Player.Score -= 50;
                    this.ParentLevel.GameObjects.Damage(this);
                }
            }

        }

        public override void Initialize()
        {
            this.BoundingBox = Helper.Box2D(this.Position, this.Position + this.Texture.Size);
        }

        public override void LoadContent(ContentManager content)
        {
            this.playerBaseNotMoving = new AnimatedTexture(content.Load<Texture2D>("Textures\\Player\\player base"), 0, 100, 0, 0.5f);
            this.playerBaseLeftSpriteSheet = new AnimatedTexture(content.Load<Texture2D>("Textures\\Player\\player baseleftspritesheet"), 7, 140, 15, 0.5f);
            this.playerBaseRightSpriteSheet = new AnimatedTexture(content.Load<Texture2D>("Textures\\Player\\player baserightspritesheet"), 7, 140, 15, 0.5f);
            this.playerBaseDownSpriteSheet = new AnimatedTexture(content.Load<Texture2D>("Textures\\Player\\playerbasespritesheet"), 7, 100, 15, 0.5f);
            this.playerBaseBackSpriteSheet = new AnimatedTexture(content.Load<Texture2D>("Textures\\Player\\player basebackwardspritesheet"), 7, 100, 15, 0.5f);

            this.Texture = this.playerBaseDownSpriteSheet;
        }

        public override void OnDamage()
        {
            if (this.MainGame.Player.HP <= 0)
            {
                this.MainGame.LevelManager.CurrentLevel = new GameOverMenu(this.MainGame);
            }
            this.MainGame.SoundManager.PlaySound(Sound.Grunt);
        }

        private void ChoseTexture()
        {
            switch (Helper.GetDirection(this.Velocity))
            {
                case Direction.Up:
                    this.Texture = this.playerBaseBackSpriteSheet;
                    break;
                case Direction.Left:
                    this.Texture = this.playerBaseLeftSpriteSheet;
                    break;
                case Direction.Right:
                    this.Texture = this.playerBaseRightSpriteSheet;
                    break;
                case Direction.Down:
                    this.Texture = this.playerBaseDownSpriteSheet;
                    break;
            }

            if (this.Velocity.LengthSquared() < 0.5 * 0.5)
            {
                this.Texture = this.playerBaseNotMoving;
            }
        }

        public override void Update(GameTime gameTime)
        {
            this.ChoseTexture();
            this.FireProjectile();
            this.Move();
            this.BoundingBox = Helper.Box2D(this.Position, this.Position + this.Texture.Size);
            collidedWithEnemyLastFrame = collidedThisFrame;
            collidedThisFrame = false;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            this.Texture.Draw(spriteBatch, this.Position, gameTime, this.CurrentTint);
        }

    }
}
