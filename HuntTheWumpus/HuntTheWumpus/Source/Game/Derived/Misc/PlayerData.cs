using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpus.Source
{
    public class PlayerData
    {
        public int HP { get; set; }
        public int Stamina { get; set; }
        public int Score { get; set; }

        public PlayerData()
        {
            this.Reset();
        }

        public void Reset()
        {
            this.HP = 3;
            this.Stamina = 1;
            this.Score = 0;
        }
    }
}
