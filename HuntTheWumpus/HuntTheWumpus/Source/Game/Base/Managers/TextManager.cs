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
        public void DrawText(Vector2 position, string text)
        {
            this.DrawText(position, text, false);
        }

        /// <summary>
        /// Draws the given string at the given position.
        /// </summary>
        /// <param name="position"> Top left corner of the string to draw. </param>
        /// <param name="text">String to draw. </param>
        /// <param name="color"> Color of the string to draw. </param>
        /// <param name="openBatch"> Whether or not to open the spritebatch for drawing. </param>
        public void DrawText(Vector2 position, string text, Color color, bool openBatch = false)
        {
            // returns if there is no text to be drawn.
            if (text == null || text.Equals(String.Empty))
            {
                return;
            }

            // opens the batch if this text is drawn outside of the draw method.
            if (openBatch)
            {
                this.MainGame.SpriteBatch.Begin();
            }

            this.MainGame.SpriteBatch.DrawString(Font, text, position, color);

            // closes the batch if it was opened here.
            if (openBatch)
            {
                this.MainGame.SpriteBatch.End();
            }
        }
    }
}
