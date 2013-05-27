﻿using System;
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
    class Enemy : IDrawable, IInitializable, IUpdateable, ICollideable
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

        private Room parentRoom;

        public Enemy(MainGame mainGame, ILevel parentLevel)
        {
            this.MainGame = mainGame;
            this.ParentLevel = parentLevel;
            this.parentRoom = parentLevel as Room;
            this.ObjectTeam = Team.Enemy;
            this.Position = new Vector2(300, 300);
            this.BoundingBox = new BoundingBox();
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
            if (this.Position.X > this.parentRoom.MainCave.CaveBounds.Width - this.TextureSize.X)
            {
                this.Position = new Vector2(this.parentRoom.MainCave.CaveBounds.Width - this.TextureSize.X, this.Position.Y);
            }
            if (this.Position.Y > this.parentRoom.MainCave.CaveBounds.Height - this.TextureSize.Y)
            {
                this.Position = new Vector2(this.Position.X, this.parentRoom.MainCave.CaveBounds.Height - this.TextureSize.Y);
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
                    //this.MainGame.PlayerData.Score += 10;
                    this.MainGame.Player.Score += 10;
                    this.ParentLevel.GameObjects.Remove(this);
                    this.ParentLevel.GameObjects.Remove(gameObject);
                }
            }
        }

        public void Initialize()
        {
            this.BoundingBox = Extensions.Box2D(this.Position, this.Position + this.TextureSize);
        }

        public void LoadContent(ContentManager content)
        {
            this.Texture = content.Load<Texture2D>("Textures\\enemy");
            this.TextureSize = new Vector2(this.Texture.Width, this.Texture.Height);
        }

        public void Update(GameTime gameTime)
        {
            this.BoundingBox = Extensions.Box2D(this.Position, this.Position + this.TextureSize);
            this.CollideWithWalls();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Position, Color.White);
        }
    }
}
