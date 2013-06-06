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
            //Opens up the TriviaMenu
            if (this.mainGame.InputManager.IsClicked(Keys.Q))
            {
                this.mainGame.LevelManager.CurrentLevel = new TriviaMenu(this.mainGame, this.mainGame.LevelManager.CurrentLevel);
            }
            //Opens up the ArrowMenu
            if (this.mainGame.InputManager.IsClicked(Keys.E))
            {
                this.mainGame.LevelManager.CurrentLevel = new ArrowMenu(this.mainGame, this.mainGame.LevelManager.CurrentLevel);
            }
            //Takes you to the pit
            if (this.mainGame.InputManager.IsClicked(Keys.P))
            {
                var pits = this.mainGame.LevelManager.GameCave.Rooms.Where((x) => x is Room && (x as Room).RoomType == RoomType.Pit);
                if (pits.Count() != 0)
                {
                    this.mainGame.LevelManager.CurrentLevel = pits.ToArray()[0];
                }
            }
            //Takes you to the Wumpus
            if (this.mainGame.InputManager.IsClicked(Keys.L))
            {
                this.mainGame.LevelManager.CurrentLevel = this.mainGame.LevelManager.GameCave.Rooms[this.mainGame.LevelManager.GameCave.Wumpus.RoomIndex];
            }
        }
    }
}
