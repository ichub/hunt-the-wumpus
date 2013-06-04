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
    public class DoGooder
    {
        private MainGame mainGame;

        public DoGooder(MainGame mainGame)
        {
            this.mainGame = mainGame;
        }

        public void Update()
        {
            if (this.mainGame.InputManager.IsClicked(Keys.Q))
            {
                this.mainGame.LevelManager.CurrentLevel = new TriviaMenu(this.mainGame, this.mainGame.LevelManager.CurrentLevel);
            }

            if (this.mainGame.InputManager.IsClicked(Keys.E))
            {
                this.mainGame.LevelManager.CurrentLevel = new ArrowMenu(this.mainGame, this.mainGame.LevelManager.CurrentLevel);
            }
        }
    }
}
