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
    class Projectile : IUpdateable, IDrawable, IInitializable, ICollideable
    {
        public MainGame MainGame { get; set; }
        public ILevel ParentLevel { get; set; }
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 TextureSize { get; set; }
        public List<BoundingBox> BoundingBoxes { get; set; }
        public Team ObjectTeam { get; set; }

        public bool ContentLoaded { get; set; }
        public bool Initialized { get; set; }
        public bool HasCollided { get; private set; }
        public Vector2 Velocity;

        private AnimatedTexture animatedTexture;
        private string imageName;

        public Projectile(MainGame mainGame, ILevel parentLevel, Team team, string picture)
        {
            this.ObjectTeam = team;
            this.MainGame = mainGame;
            this.ParentLevel = parentLevel;

            this.Position = new Vector2(100, 100);
            this.BoundingBoxes = new List<BoundingBox>();
            this.Velocity = new Vector2(0, 1);
            this.imageName = picture;           
        }

        public void CollideWith(ICollideable gameObject, bool isCollided)
        {
            if (isCollided)
                if (gameObject is Enemy)
                    this.HasCollided = true;
        }

        private void Remove()
        {
            if (this.Position.X < 0)
            {
                this.ParentLevel.GameObjects.Remove(this);
            }
            else if (this.Position.Y < 0)
            {
                this.ParentLevel.GameObjects.Remove(this);
            }
            else if (this.Position.X > this.MainGame.LevelManager.GameCave.CaveBounds.Width - this.TextureSize.X)
            {
                this.ParentLevel.GameObjects.Remove(this);
            }
            else if (this.Position.Y > this.MainGame.LevelManager.GameCave.CaveBounds.Height - this.TextureSize.Y)
            {
                this.ParentLevel.GameObjects.Remove(this);
            }
        }

        public void Initialize()
        {
            this.BoundingBoxes.Add(Extensions.Box2D(this.Position, this.Position + this.TextureSize));
        }

        public void LoadContent(ContentManager content)
        {
            this.Texture = content.Load<Texture2D>("Textures\\" + this.imageName);
            this.animatedTexture = new AnimatedTexture(this.Texture, 5, 20, 20, 10);
            this.TextureSize = new Vector2(this.animatedTexture.Size.X, this.animatedTexture.Size.Y);
        }

        public void Update(GameTime gameTime)
        {
            this.Remove();
            if (this.BoundingBoxes.Any())
                this.BoundingBoxes[0] = Extensions.Box2D(this.Position, this.Position + this.TextureSize);
            this.Position += Velocity * 3;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(this.Texture, this.Position, null, Color.White, (float)this.rotation, this.TextureSize / 2, new Vector2(1, 1), SpriteEffects.None, 0);
            this.animatedTexture.Draw(spriteBatch, this.Position, this.MainGame.GameTime);
        }
    }
}
