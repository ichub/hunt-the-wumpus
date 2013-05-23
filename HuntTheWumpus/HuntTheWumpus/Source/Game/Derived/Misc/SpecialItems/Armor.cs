using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpus
{
    class Armor
    {
        public int armorPiece;
        public Armor(int itemID)
        {
            armorPiece = itemID;
        }

        private int protectRatio()
        {
            if (armorPiece == 3)
            {
                return 2;
            }
            else if (armorPiece == 4)
            {
                return 4;
            }
            else if (armorPiece == 5)
            {
                return 3;
            }
            else if (armorPiece == 6)
            {
                return 3;
            }
            else if (armorPiece == 7)
            {
                return 5;
            }
            else if (armorPiece == 8)
            {
                return 4;
            }
            else
            {
                return 1;
            }
        }

        public double takeDamage(int damage)
        {
            return damage/this.protectRatio();
        }


    }
}
