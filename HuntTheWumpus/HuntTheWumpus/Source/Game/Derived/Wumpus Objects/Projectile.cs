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
        public double rotation = 0;

        public Vector2 Velocity;

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
            
        }

        public void Initialize()
        {
            this.BoundingBoxes.Add(Extensions.Box2D(this.Position, this.Position + this.TextureSize));
        }

        public void LoadContent(ContentManager content)
        {
            this.Texture = content.Load<Texture2D>("Textures\\" + this.imageName);
        }

        public void Update(GameTime gameTime)
        {
            this.BoundingBoxes[0] = Extensions.Box2D(this.Position, this.Position + this.TextureSize);
            this.Position += Velocity;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Position, null, Color.White, (float)this.rotation, this.TextureSize / 2, new Vector2(1, 1), SpriteEffects.None, 0);
        }
    }
}
