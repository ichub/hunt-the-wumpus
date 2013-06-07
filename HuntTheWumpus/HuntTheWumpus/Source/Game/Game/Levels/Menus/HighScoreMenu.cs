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

        public const int XPositionName = 290;
        public const int XPositionScore = 690;

        public override void FrameDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.FrameDraw(gameTime, spriteBatch);
            var highScores = this.MainGame.HighScore.GetList();
            highScores.Reverse();

            this.MainGame.TextManager.DrawText(new Vector2(HighScoreMenu.XPositionName, 140 - 20), "Names");
            this.MainGame.TextManager.DrawText(new Vector2(HighScoreMenu.XPositionScore, 140 - 20), "Scores");

            for (int i = 0; i < 10; i++)
            {
                if (highScores.Count > i)
                {
                    this.MainGame.TextManager.DrawText(new Vector2(HighScoreMenu.XPositionName - 20, 160 + i * 20), i + 1);
                    this.MainGame.TextManager.DrawText(new Vector2(HighScoreMenu.XPositionName, 160 + i * 20), highScores[i].Name);
                    this.MainGame.TextManager.DrawText(new Vector2(HighScoreMenu.XPositionScore, 160 + i * 20), highScores[i].Score.ToString());
                }
            }

            highScores.Reverse();

        }
    }
}
