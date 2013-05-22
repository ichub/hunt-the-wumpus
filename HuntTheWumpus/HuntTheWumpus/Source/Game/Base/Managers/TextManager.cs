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
        public MainGame MainGame { get; private set; }
        public SpriteFont CourierNew { get; private set; }

        private SpriteBatch textBatch;

        /// <summary>
        /// Creates a new text manager.
        /// </summary>
        /// <param name="mainGame"> Game to which this manager belongs to. </param>
        public TextManager(MainGame mainGame)
        {
            this.MainGame = mainGame;
            this.CourierNew = mainGame.Content.Load<SpriteFont>("Fonts\\Courier New");
            this.textBatch = mainGame.SpriteBatch;
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
        public void DrawText(Vector2 position, string text, Color color, bool openBatch)
        {
            if (openBatch)
                textBatch.Begin();
            textBatch.DrawString(CourierNew, text, position, color);
            if (openBatch)
                textBatch.End();
        }
    }
}
