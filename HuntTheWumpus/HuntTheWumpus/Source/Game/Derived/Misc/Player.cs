using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpus.Source
{
    public class Player
    {
        public int CurrentRoom { get; set; }
        public int HP { get; set; }
        public int Armor { get; set; }
        public int Money { get; set; }
        public int Score { get; set; }
        public Inventory Inventory { get; set; }

        public Player(int difficulty)
        {
            this.CurrentRoom = 0;
            this.HP = 20;
            this.Money = 100;
            this.Score = 0;
            this.Inventory = new Inventory(difficulty);
        }

        public void pay(int sum)
        {
            Money -= sum;
        }
    }
}


    
