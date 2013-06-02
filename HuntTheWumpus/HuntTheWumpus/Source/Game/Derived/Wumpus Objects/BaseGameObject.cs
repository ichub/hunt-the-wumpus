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
    class BaseGameObject : IEntity, IDamagable
    {
        public MainGame MainGame { get; set; }
        public ILevel ParentLevel { get; set; }
        public AnimatedTexture Texture { get; set; }
        public Vector2 LastPosition { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public BoundingBox BoundingBox { get; set; }
        public Team ObjectTeam { get; set; }
        public Timer DamageTimer { get; set; }
        public Color DamageTint { get; set; }
        public Color CurrentTint { get; set; }

        public bool ContentLoaded { get; set; }
        public bool Initialized { get; set; }
        public bool IsDamaged { get; set; }
        public float SpeedDampening { get; set; }
        public int DamageLength { get; set; }

        protected Room ParentRoom { get; set; }

        public BaseGameObject(MainGame mainGame, ILevel parentLevel)
        {
            this.MainGame = mainGame;
            this.ParentLevel = parentLevel;
            this.ParentRoom = parentLevel as Room;
            this.ObjectTeam = Team.Enemy;
            this.Position = new Vector2(300, 300);
            this.BoundingBox = new BoundingBox();
            this.CurrentTint = Color.White;
            this.DamageLength = 259;
            this.DamageTint = Color.Red;
            this.SpeedDampening = 1.1f;
        }

        public virtual void CollideWithLevelBounds()
        {
            this.Velocity = Vector2.Zero;
            this.Position += (this.LastPosition - this.Position) * 2;
        }

        public virtual void CollideWith(ICollideable gameObject, bool isCollided)
        {
            
        }

        public virtual void Initialize()
        {

        }

        public virtual void LoadContent(ContentManager content)
        {
        }

        public virtual void Update(GameTime gameTime)
        {
            this.Position += this.Velocity;
            this.Velocity /= this.SpeedDampening;
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            this.Texture.Draw(spriteBatch, this.Position, gameTime, this.CurrentTint);
        }
    }
}