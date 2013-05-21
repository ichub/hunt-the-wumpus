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
    class Teleporter : IDrawable, ICollideable, IInitializable, IUpdateable
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

        public Room RoomToTeleportTo { get; set; }

        private bool collidedThisFrame = false;
        private bool collidedLastFrame = false;

        public Teleporter(MainGame mainGame, ILevel parentLevel, Room toTeleportTo)
        {
            this.MainGame = mainGame;
            this.ParentLevel = parentLevel;

            this.ObjectTeam = Team.Player;
            this.Position = new Vector2(100, 100);
            this.BoundingBoxes = new List<BoundingBox>();
            this.ObjectTeam = Team.None;

            this.RoomToTeleportTo = toTeleportTo;
        }

        public void CollideWith(ICollideable gameObject, bool isColliding)
        {
            if (isColliding)
                if (gameObject is PlayerAvatar)
                {
                    collidedThisFrame = isColliding | collidedThisFrame;
                    if (!collidedLastFrame)
                        MainGame.LevelManager.CurrentLevel = RoomToTeleportTo;
                }

        }

        public void Update(GameTime gameTime)
        {
            this.collidedLastFrame = this.collidedThisFrame;
            this.collidedThisFrame = false;
        }

        public void Initialize()
        {
            this.BoundingBoxes.Add(Extensions.Box2D(this.Position, this.Position + this.TextureSize));
            this.BoundingBoxes[0] = Extensions.Box2D(this.Position, this.Position + this.TextureSize);
        }

        public void LoadContent(ContentManager content)
        {
            this.Texture = content.Load<Texture2D>("Textures\\teleporter");
            this.TextureSize = new Vector2(this.Texture.Width, this.Texture.Height);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(this.Texture, this.Position, Color.White);
        }
    }
}
