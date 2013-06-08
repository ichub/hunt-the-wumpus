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
    /// provides a class for hotkeys
    /// </summary>
    public class DoGooder
    {
        private MainGame mainGame;

        /// <summary>
        /// Needs the maingame
        /// </summary>
        /// <param name="mainGame"></param>
        public DoGooder(MainGame mainGame)
        {
            this.mainGame = mainGame;
        }

        /// <summary>
        /// provides a method to update and check the hotkeys
        /// </summary>
        public void Update()
        {
            if ((this.mainGame.LevelManager.CurrentLevel is Room))
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
                    IEnumerable<Room> pits = this.mainGame.LevelManager.GameCave.Rooms.Where((x) => x is Room && (x as Room).RoomType == RoomType.Pit);
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

                // takes you to a superbat
                if (this.mainGame.InputManager.IsClicked(Keys.B))
                {
                    this.mainGame.LevelManager.CurrentLevel = this.mainGame.LevelManager.GameCave.Rooms[this.mainGame.LevelManager.GameCave.SuperBats[0].ParentRoomIndex];
                }

                if (this.mainGame.InputManager.IsClicked(Keys.K))
                {
                    this.mainGame.LevelManager.CurrentLevel = new WinMenu(this.mainGame);
                    this.mainGame.LevelManager.Reset();
                }
            }
        }
    }
}
