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
    class PlayerAvatar : IDrawable, IUpdateable, IInitializable, IGameObject
    {
        public MainGame MainGame { get; set; }
        public ILevel ParentLevel { get; set; }
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 TextureSize { get; set; }

        public bool ContentLoaded { get; set; }
        public bool Initialized { get; set; }

        private Vector2 velocity;
        private int moveSpeed = 2;

        public PlayerAvatar(MainGame mainGame, ILevel parentLevel)
        {
            this.MainGame = mainGame;
            this.ParentLevel = parentLevel;

            this.Position = new Vector2(0, 0);
        }

        public void Initialize()
        {
        }

        public void Move()
        {
            if (MainGame.InputManager.KeyboardState.IsKeyDown(Keys.A))
                this.velocity += new Vector2(-moveSpeed, 0);
            if (MainGame.InputManager.KeyboardState.IsKeyDown(Keys.W))
                this.velocity += new Vector2(0, -moveSpeed);
            if (MainGame.InputManager.KeyboardState.IsKeyDown(Keys.D))
                this.velocity += new Vector2(moveSpeed, 0);
            if (MainGame.InputManager.KeyboardState.IsKeyDown(Keys.S))
                this.velocity += new Vector2(0, moveSpeed);

            this.Position += velocity;
            this.velocity /= 1.2f;

            // limits speed vector to a length of 5 pixels per frame.
            if (this.velocity.LengthSquared() > 5 * 5)
            {
                this.velocity /= this.velocity.Length();
                this.velocity *= 5;
            }
        }

        public void LoadContent(ContentManager content)
        {
            this.Texture = content.Load<Texture2D>("player");
        }

        public void Update(GameTime gameTime)
        {
            this.Move();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Position, Color.White);
        }
    }
}
