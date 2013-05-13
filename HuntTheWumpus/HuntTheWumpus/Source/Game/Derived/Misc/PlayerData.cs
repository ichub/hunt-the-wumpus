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

        public PlayerData()
        {
            this.HP = 3;
            this.Stamina = 1;
        }
    }
}
