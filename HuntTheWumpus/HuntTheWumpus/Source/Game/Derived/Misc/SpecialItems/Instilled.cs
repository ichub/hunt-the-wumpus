using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpus.Source
{
    public class InstilledRing 
    {
        public int Chance { get; set; }

        public InstilledRing(RingType type)
        {
            if (type == RingType.Standard) 
                this.Chance = 20;
            else if (type == RingType.Upgraded) 
                this.Chance = 30;
            else if (type == RingType.Ultimate) 
                this.Chance = 50;
        }
    }
}
