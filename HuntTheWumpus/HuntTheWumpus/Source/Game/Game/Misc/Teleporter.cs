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
    public class Teleporter : IDrawable, ICollideable, IInitializable, IUpdateable
    {
        public MainGame MainGame { get; set; }
        public ILevel ParentLevel { get; set; }
        public AnimatedTexture Texture { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 LastPosition { get; set; }
        public BoundingBox BoundingBox { get; set; }
        public Team ObjectTeam { get; set; }

        public bool ContentLoaded { get; set; }
        public bool Initialized { get; set; }
        public bool IsHidden { get; set; }

        public Room RoomToTeleportTo { get; set; }

        private bool collidedThisFrame = false;
        private bool collidedLastFrame = false;

        public Teleporter(MainGame mainGame, ILevel parentLevel, Room toTeleportTo)
        {
            this.MainGame = mainGame;
            this.ParentLevel = parentLevel;

            this.ObjectTeam = Team.Player;
            this.Position = new Vector2(100, 100);
            this.BoundingBox = new BoundingBox();
            this.ObjectTeam = Team.Neutral;

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
                    this.ParentLevel.GameObjects.Remove(this);
                }

        }

        public void CollideWithLevelBounds()
        {
        }

        public void Update(GameTime gameTime)
        {
            this.collidedLastFrame = this.collidedThisFrame;
            this.collidedThisFrame = false;
        }

        public void Initialize()
        {
            this.BoundingBox = Helper.Box2D(this.Position, this.Position + this.Texture.Size);
        }

        public void LoadContent(ContentManager content)
        {
            this.Texture = new AnimatedTexture(content.Load<Texture2D>("Textures\\teleporter"));
            this.BoundingBox = Helper.Box2D(this.Position, this.Position + this.Texture.Size);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //this.Texture.Draw(spriteBatch, this.Position, this.MainGame.GameTime);
        }
    }
}
