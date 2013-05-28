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
    public class Player
    {
        public int CurrentRoom { get; set; }
        public int HP { get; set; }
        public int Armor { get; set; }
        public int Money { get; set; }
        public int Score { get; set; }
        public float MaxSpeed { get; set; }
        public Inventory Inventory { get; set; }

        public Player()
        {
            this.CurrentRoom = 0;
            this.HP = 3;
            this.Money = 100;
            this.Score = 0;
            this.MaxSpeed = 5;
            this.Inventory = new Inventory(20);
        }

        public void pay(int sum)
        {
            Money -= sum;
        }
    }
}


    
