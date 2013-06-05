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
    class HighScoreMenu : BaseMenu
    {
        public HighScoreMenu(MainGame parentGame, ILevel cameFrom)
            : base(parentGame, cameFrom) { }

        public override void FrameDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.FrameDraw(gameTime, spriteBatch);
            var highScores = this.MainGame.HighScore.GetList();
            this.MainGame.TextManager.DrawText(new Vector2(290, 140 - 20), "Names");
            this.MainGame.TextManager.DrawText(new Vector2(690, 140 - 20), "Scores");

            for (int i = highScores.Count - 1; i > highScores.Count - 10; i--)
            {
                if (highScores.Count > 10 - i)
                {
                    this.MainGame.TextManager.DrawText(new Vector2(290, 140 + (10 - i) * 20), highScores[i].Name);
                    this.MainGame.TextManager.DrawText(new Vector2(690, 140 + (10 - i) * 20), highScores[i].Score.ToString());
                }
            }
        }
    }
}
