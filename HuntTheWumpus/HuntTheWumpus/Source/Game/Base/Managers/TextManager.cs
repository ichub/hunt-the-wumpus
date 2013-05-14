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
    public class TextManager
    {
        public MainGame MainGame { get; set; }
        public SpriteFont CourierNew { get; set; }

        private SpriteBatch textBatch;

        public TextManager(MainGame mainGame)
        {
            this.MainGame = mainGame;
            this.CourierNew = mainGame.Content.Load<SpriteFont>("Fonts\\Courier New");
            this.textBatch = mainGame.SpriteBatch;
        }

        public void DrawText(Vector2 position, string text, bool openBatch)
        {
            if (openBatch)
                textBatch.Begin();
            textBatch.DrawString(CourierNew, text, position, Color.Black);
            if (openBatch)
                textBatch.End();
        }

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
