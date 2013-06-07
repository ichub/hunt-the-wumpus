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
    public class PlayerStats
    {
        public string Name { get; set; }

        public int CurrentRoom { get; set; }
        public int HP { get; set; }
        public int Armor { get; set; }
        public int Money { get; set; }
        public int Score { get; set; }
        public int CorrectAmountAnswered { get; set; }
        public int AmountOfArrows { get; set; }

        public float MaxSpeed { get; set; }
        public float SpeedDelta { get; set; }

        public Inventory Inventory { get; set; }

        public PlayerStats(string name)
        {
            this.Name = name;
            this.CurrentRoom = 0;
            this.HP = 3;
            this.Money = 100;
            this.Score = 0;
            this.CorrectAmountAnswered = 0;
            this.MaxSpeed = 5;
            this.SpeedDelta = 2;
            this.Inventory = new Inventory(20);
        }

        public bool Pay(int sum)
        {
            if (sum <= this.Money)
            {
                this.Money -= sum;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            this.CurrentRoom = 0;
            this.HP = 3;
            this.Money = 100;
            this.Score = 0;
            this.CorrectAmountAnswered = 0;
            this.MaxSpeed = 5;
            this.SpeedDelta = 2;
            this.Inventory = new Inventory(20);
        }
    }
}



