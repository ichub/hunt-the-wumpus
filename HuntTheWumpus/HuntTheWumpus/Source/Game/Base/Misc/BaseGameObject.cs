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
    /// <summary>
    /// Class from which all game objects inherit. Implements some basic functionality.
    /// </summary>
    public abstract class BaseGameObject : IEntity, IDamagable
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

        public float SpeedDampening { get; set; }
        public float SpeedModifier { get; set; }
        public float Layer { get; set; }
        public bool ContentLoaded { get; set; }
        public bool Initialized { get; set; }
        public bool IsHidden { get; set; }
        public bool IsDamaged { get; set; }
        public int DamageLength { get; set; }

        /// <summary>
        /// A reference to the parent level as a room.
        /// </summary>
        protected Room ParentRoom { get; set; }

        /// <summary>
        /// Creates a new base game object.
        /// </summary>
        /// <param name="mainGame"> The game to which this object belongs to. </param>
        /// <param name="parentLevel"> The level to which the object belongs to. </param>
        public BaseGameObject(MainGame mainGame, ILevel parentLevel)
        {
            this.MainGame = mainGame;
            this.ParentLevel = parentLevel;

            this.ParentRoom = parentLevel as Room;
            this.ObjectTeam = Team.Enemy;
            this.BoundingBox = new BoundingBox();

            this.CurrentTint = Color.White;
            this.DamageTint = Color.Red;
            this.DamageLength = 259;
            this.SpeedDampening = 1.1f;
            this.SpeedModifier = 1;
        }

        /// <summary>
        /// Checks if none of this object is being drawn right now.
        /// </summary>
        /// <returns></returns>
        protected bool IsOutOfViewport()
        {
            return
                this.Position.X < 0 ||
                this.Position.X > this.MainGame.WindowWidth ||
                this.Position.Y < 0 ||
                this.Position.Y > this.MainGame.WindowHeight;
        }

        /// <summary>
        /// Removes this object at the end of the frame.
        /// </summary>
        public void Remove()
        {
            this.ParentLevel.GameObjects.Remove(this);
        }

        /// <summary>
        /// Reacts to collision with the level.
        /// </summary>
        public virtual void CollideWithLevelBounds()
        {
            this.Velocity = Vector2.Zero;
            this.Position += (this.LastPosition - this.Position) * 2;
        }

        /// <summary>
        /// Default collision method for all game objects.
        /// </summary>
        /// <param name="gameObject"> The object with which to collide. </param>
        /// <param name="isCollided"> Whether or not the two objects are actually colliding. </param>
        public virtual void CollideWith(ICollideable gameObject, bool isCollided) { }

        /// <summary>
        /// Initializes the object.
        /// </summary>
        public virtual void Initialize() { }

        /// <summary>
        /// Method which is called when the object is damaged.
        /// </summary>
        public virtual void OnDamage() { }

        /// <summary>
        /// Loads the objects content.
        /// </summary>
        /// <param name="content"> The content manager used to load content. </param>
        public virtual void LoadContent(ContentManager content) { }

        /// <summary>
        /// Default update method for all game objects.
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            this.Position += this.Velocity * this.SpeedModifier;
            this.Velocity /= this.SpeedDampening;
        }

        /// <summary>
        /// Default draw method for all game objects, just draws the texture at the 
        /// object's position.
        /// </summary>
        /// <param name="gameTime"> A snapshot of the time. </param>
        /// <param name="spriteBatch"> The spritebatch with which to draw the texture. </param>
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            this.Texture.Draw(spriteBatch, this.Position, gameTime, this.CurrentTint);
        }
    }
}