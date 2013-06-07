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
    class ItemObject : IEntity
    {
        public MainGame MainGame { get; set; }
        public ILevel ParentLevel { get; set; }
        public AnimatedTexture Texture { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 LastPosition { get; set; }
        public Vector2 Velocity { get; set; }
        public BoundingBox BoundingBox { get; set; }
        public Team ObjectTeam { get; set; }
        public Item RepresentedItem { get; set; }

        public bool ContentLoaded { get; set; }
        public bool Initialized { get; set; }
        public bool IsHidden { get; set; }
        public float SpeedDampening { get; set; }

        private Vector2 velocity;

        public ItemObject(MainGame mainGame, ILevel parentLevel, string item)
        {
            this.MainGame = mainGame;
            this.ParentLevel = parentLevel;
            this.ObjectTeam = Team.Player;
            this.Position = new Vector2(40, 40);
            this.RepresentedItem = ItemList.GetItem(item);
            this.velocity = Helper.RandomVector(1, 4);
        }

        public virtual void CollideWith(ICollideable gameObject, bool isColliding)
        {
            if (isColliding)
            if (gameObject is PlayerAvatar)
            {
                this.CollideWithPlayer(gameObject as PlayerAvatar);
            }
        }

        public void CollideWithLevelBounds()
        {
            this.Velocity = Vector2.Zero;
            this.Position += (this.LastPosition - this.Position) * 2;
        }

        public virtual void CollideWithPlayer(PlayerAvatar player)
        {
            this.MainGame.Player.Inventory.PickUp(this.RepresentedItem);
            this.ParentLevel.GameObjects.Remove(this);
        }

        public virtual void Initialize()
        {
            this.BoundingBox = Helper.Box2D(this.Position, this.Position + this.Texture.Size);
        }

        public virtual void LoadContent(ContentManager content)
        {
            this.Texture = new AnimatedTexture(content.Load<Texture2D>("Textures\\Items\\" + this.RepresentedItem.Name));
        }

        public virtual void Update(GameTime gameTime)
        {
            this.Position += this.velocity;
            this.velocity /= 1.1f;
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            this.Texture.Draw(spriteBatch, this.Position, gameTime);
        }
    }
}
