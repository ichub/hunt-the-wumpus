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
    class AnimatedTexture
    {
        public Vector2 Size { get; private set; }
        public Texture2D SpriteSheet { get; private set; }
        public int AmountOfFrames { get; private set; }
        public int FPS { get; private set; }

        private int milisecondsSinceLastUpdate;
        private readonly int maxFrame;
        private int currentFrame;

        public AnimatedTexture(Texture2D spriteSheet, int amountOfFrames, int spriteWidth, int spriteHeight, int fps)
        {
            this.Size = new Vector2(spriteWidth, spriteHeight);
            this.SpriteSheet = spriteSheet;
            this.FPS = fps;
            this.currentFrame = 0;
            this.maxFrame = amountOfFrames;
            this.milisecondsSinceLastUpdate = 0;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, GameTime gameTime)
        {
            this.milisecondsSinceLastUpdate += gameTime.ElapsedGameTime.Milliseconds;
            if (this.milisecondsSinceLastUpdate > 1000.0 / this.FPS)
            {
                this.currentFrame++;
                this.currentFrame %= this.maxFrame;
                this.milisecondsSinceLastUpdate = 0;
            }
            spriteBatch.Draw(this.SpriteSheet, position, new Rectangle(currentFrame * (int)this.Size.X, 0, (int)this.Size.X, (int)this.Size.Y), Color.White);
        }
    }
}
