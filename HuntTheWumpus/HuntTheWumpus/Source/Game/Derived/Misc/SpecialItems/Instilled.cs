using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpus.Source
{
    class Instilled 
    {
        public int chance;
        public Instilled(RingType type)
        {
            if (type == RingType.Standard) chance = 20;
            else if (type == RingType.Upgraded) chance = 30;
            else if (type == RingType.Ultimate) chance = 50;
        }
    }
}
