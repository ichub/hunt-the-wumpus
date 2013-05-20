using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpus.Source
{
    class Player
    {
        public int pos = 0; //Player position
        public int hp = 0; //Player health 
        public int arm = 0; //Player armor value
        public int money = 0; //Player money ammount
        private int score = 0;

        /// <summary>
        /// Sets up a new player
        /// </summary>
        /// <param name="difficulty">1-3 difficulty value</param>
        public Player(int difficulty)
        {
            pos = 0;
            hp = 20;
            money = 100;
            Inventory inv = new Inventory(difficulty);
        }

        /// <summary>
        /// Moves a player if able to 
        /// </summary>
        /// <param name="direction">direction in which the player should move</param>
        /// <returns>Players new position</returns>
        public int Move(int direction)
        {
            if (/*MAP VERIFICATION*/ true)
            {
                pos = 0;//move up on map;
            }
            return pos;
        }

        public void pay(int sum)
        {
            money -= sum;
        }
    }
}


    
