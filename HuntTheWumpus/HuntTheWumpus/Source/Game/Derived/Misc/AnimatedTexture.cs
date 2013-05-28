﻿using System;
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
    /// Class for drawing animated sprites given a spritesheet.
    /// </summary>
    public class AnimatedTexture
    {
        /// <summary>
        /// Size of one frame of the texture.
        /// </summary>
        public Vector2 Size { get; private set; }

        /// <summary>
        /// The spritesheet for this texture.
        /// </summary>
        public Texture2D SpriteSheet { get; private set; }

        /// <summary>
        /// The amount of frames in this texture.
        /// </summary>
        public int AmountOfFrames { get; private set; }

        /// <summary>
        /// How many times per second the frame is updated.
        /// </summary>
        public int FPS { get; private set; }

        private int milisecondsSinceLastUpdate;
        private readonly int maxFrame;
        private int currentFrame;

        /// <summary>
        /// Creates a new animated texture.
        /// </summary>
        /// <param name="spriteSheet"> The spritesheet for this texture. </param>
        /// <param name="amountOfFrames"> The amount of frames this texture will have. </param>
        /// <param name="spriteWidth"> The width of each frame of this texture. </param>
        /// <param name="spriteHeight"> The height of each frame of this texture. </param>
        /// <param name="fps"> The amount of times per second the frame is updated.</param>
        public AnimatedTexture(Texture2D spriteSheet, int amountOfFrames, int spriteWidth, int spriteHeight, int fps)
        {
            this.Size = new Vector2(spriteWidth, spriteHeight);
            this.SpriteSheet = spriteSheet;
            this.FPS = fps;
            this.currentFrame = 0;
            this.maxFrame = amountOfFrames;
            this.milisecondsSinceLastUpdate = 0;
        }

        /// <summary>
        /// Creates a static texture.
        /// </summary>
        /// <param name="texture"> Texture to display. </param>
        public AnimatedTexture(Texture2D texture)
        {
            this.SpriteSheet = texture;
            this.Size = new Vector2(texture.Width, texture.Height);
            this.AmountOfFrames = 1;
            this.FPS = 0;
            this.currentFrame = 0;
            this.maxFrame = 0;
            this.milisecondsSinceLastUpdate = 0;
        }

        /// <summary>
        /// Draws the texture.
        /// </summary>
        /// <param name="spriteBatch"> Spritebatch to draw it with. </param>
        /// <param name="position"> The position to draw it at. </param>
        /// <param name="gameTime"> The game time. </param>
        public void Draw(SpriteBatch spriteBatch, Vector2 position, GameTime gameTime)
        {
            this.milisecondsSinceLastUpdate += gameTime.ElapsedGameTime.Milliseconds;
            if (this.FPS != 0)
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
