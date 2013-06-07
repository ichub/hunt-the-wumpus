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
    /// Class responsible for drawing text.
    /// </summary>
    public class TextManager
    {
        /// <summary>
        /// The game to which the manager belongs.
        /// </summary>
        public MainGame MainGame { get; private set; }

        /// <summary>
        /// The font which is used to display the text.
        /// </summary>
        public SpriteFont Font { get; private set; }

        /// <summary>
        /// Creates a new text manager.
        /// </summary>
        /// <param name="mainGame"> Game to which this manager belongs to. </param>
        public TextManager(MainGame mainGame)
        {
            this.MainGame = mainGame;
            this.Font = mainGame.Content.Load<SpriteFont>("Fonts\\Courier New");
        }

        /// <summary>
        /// Draws the given string at the given position.
        /// </summary>
        /// <param name="position"> Top left corner of the string to draw. </param>
        /// <param name="text"> String to draw. </param>
        /// <param name="openBatch"> Whether or not to open the spritebatch for drawing. </param>
        public void DrawText(Vector2 position, string text, bool openBatch)
        {
            this.DrawText(position, text, Color.White, openBatch);
        }

        /// <summary>
        /// Draws the text without opening the spritebatch.
        /// </summary>
        /// <param name="position"> Tope left corner of the string to draw. </param>
        /// <param name="text"> String to draw. </param>
        public void DrawText(Vector2 position, object text)
        {
            this.DrawText(position, text.ToString(), false);
        }

        /// <summary>
        /// Draws the given string at the given position.
        /// </summary>
        /// <param name="position"> Top left corner of the string to draw. </param>
        /// <param name="text">String to draw. </param>
        /// <param name="color"> Color of the string to draw. </param>
        /// <param name="openBatch"> Whether or not to open the spritebatch for drawing. </param>
        public void DrawText(Vector2 position, object text, Color color, bool openBatch = false)
        {
            // returns if there is no text to be drawn.
            if (text == null || text.ToString().Length < 1)
            {
                return;
            }

            // opens the batch if this text is drawn outside of the draw method.
            if (openBatch)
            {
                this.MainGame.SpriteBatch.Begin();
            }

            this.MainGame.SpriteBatch.DrawString(Font, text.ToString(), Helper.RoundVector(position), color, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            // closes the batch if it was opened here.
            if (openBatch)
            {
                this.MainGame.SpriteBatch.End();
            }
        }

        /// <summary>
        /// Draws the given text in a text block with word wrapping.
        /// </summary>
        /// <param name="position"> The top left corner of the word block. </param>
        /// <param name="text"> The text to draw. </param>
        /// <param name="maxWidth"> The width of the word block. </param>
        /// <param name="color"> The color of the text. </param>
        /// <returns> The bottom right corner of the word block. </returns>
        public Vector2 DrawTextBlock(Vector2 position, string text, int maxWidth, Color color)
        {
            Vector2 cursor = position;
            string[] words = text.Split(' ', '\n', '\t');
            float stringHeight = this.Font.MeasureString("a").Y;

            // loops through each word.
            for (int i = 0; i < words.Length; i++)
            {
                Vector2 wordSize = this.Font.MeasureString(words[i]);

                // if the word overlaps the max width, then move cursor down a line.
                if (wordSize.X + cursor.X > position.X + maxWidth)
                {
                    cursor.X = position.X;
                    cursor.Y += stringHeight + 2;
                }

                // draws the word.
                this.MainGame.SpriteBatch.DrawString(this.Font, words[i], cursor, color);

                // moves the cursor to the end of the word, and an extra space.
                cursor.X += wordSize.X + 20;
            }

            // the ending position of the cursor.
            return cursor;
        }
    }
}
