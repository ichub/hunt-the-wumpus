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
        public Item RepresentedItem { get; set; }

        public bool ContentLoaded { get; set; }
        public bool Initialized { get; set; }

        public PhysicalItem(MainGame mainGame, ILevel parentLevel, string item)
        {
            this.MainGame = mainGame;
            this.ParentLevel = parentLevel;
            this.ObjectTeam = Team.Player;
            this.Position = new Vector2(40, 40);
            this.RepresentedItem = ItemList.GetItem(item);
        }

        public void CollideWith(ICollideable gameObject, bool isColliding)
        {
            if (isColliding)
            if (gameObject is PlayerAvatar)
            {
                this.MainGame.Player.Inventory.PickUp(this.RepresentedItem);
                this.ParentLevel.GameObjects.Remove(this);
            }
        }

        public void Initialize()
        {
            this.BoundingBox = Extensions.Box2D(this.Position, this.Position + this.TextureSize);
        }

        public void LoadContent(ContentManager content)
        {
            this.Texture = content.Load<Texture2D>("Textures\\Items\\" + this.RepresentedItem.Name);
            this.TextureSize = new Vector2(this.Texture.Width, this.Texture.Height);
        }

        public void Update(GameTime gameTime)
        {
            this.BoundingBox = Extensions.Box2D(this.Position, this.Position + this.TextureSize);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Position, Color.White);
        }
    }
}
