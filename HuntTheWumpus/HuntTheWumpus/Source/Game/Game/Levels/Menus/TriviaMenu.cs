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
    class TriviaMenu : BaseMenu
    {
        public const int AmountOfButtons = 4;

        private Question currentQuestion;
        private ILevel cameFrom;

        public TriviaMenu(MainGame mainGame, ILevel cameFrom)
            : base(mainGame, cameFrom)
        {
            this.currentQuestion = this.MainGame.TriviaManager.RandomQuestion();
            this.cameFrom = cameFrom;
        }

        public TriviaMenu(MainGame mainGame)
            : this(mainGame, mainGame.LevelManager.GameCave.Rooms[0]) { }

        private void CheckQuestion(int answer)
        {
            if (this.currentQuestion == null)
                return;

            if (this.currentQuestion.CorrectAnswer == answer)
            {
                this.MainGame.Player.Score += 100;
            }
            
            this.currentQuestion = this.MainGame.TriviaManager.RandomQuestion();
            if (this.currentQuestion == null)
                this.MainGame.LevelManager.CurrentLevel = this.cameFrom;
            
        }

        public override void OnLoad()
        {
            this.GameObjects.Add(new Button(this.MainGame, this, () => { CheckQuestion(0); }, "1button") { Position = new Vector2(290, 100 + 200) });
            this.GameObjects.Add(new Button(this.MainGame, this, () => { CheckQuestion(1); }, "2button") { Position = new Vector2(520, 100 + 200) });
            this.GameObjects.Add(new Button(this.MainGame, this, () => { CheckQuestion(2); }, "3button") { Position = new Vector2(290, 225 + 200) });
            this.GameObjects.Add(new Button(this.MainGame, this, () => { CheckQuestion(3); }, "4button") { Position = new Vector2(520, 225 + 200) });
            
            base.OnLoad();
        }

        public override void FrameDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.FrameDraw(gameTime, spriteBatch);
            this.MainGame.TextManager.DrawText(new Vector2(290, 100), 
                this.currentQuestion == null ? "" : this.currentQuestion.QuestionString, new Color(255, 212, 153));

            for (int i = 0; i < TriviaMenu.AmountOfButtons; i++)
            {
                this.MainGame.TextManager.DrawText(new Vector2(290, 140 + i * 20), this.currentQuestion == null ? "" : (i + 1) + " : " + this.currentQuestion.QuestionAnswers[i], new Color(255, 212, 153));
            }
        }
        public override void FrameUpdate(GameTime gameTime, ContentManager content)
        {
            base.FrameUpdate(gameTime, content);
        }
    }
}
