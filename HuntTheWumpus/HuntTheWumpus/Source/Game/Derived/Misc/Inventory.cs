using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace HuntTheWumpus.Source
{
    class Inventory
    {
        public int[,] inv = new int[5, 2];
        /// <summary>
        /// Creates a new inventory 
        /// </summary>
        /// <param name="difficulty">0-3 difficulty value for starter values</param>
        public Inventory(int dif)
        {
            //generate items based on difficulty 
            inv[0, 0] = 1;
            inv[0, 1] = 0;
            inv[0, 2] = 1;

            inv[1, 0] = 2;
            inv[1, 1] = 0;
            inv[1, 2] = 10;
        }
        /// <summary>
        /// Pick up an item
        /// </summary>
        /// <param name="item">item array([0] = item id, [1] = item damage, [2] = item amount</param>
        /// <returns>whether the item was picked up or not</returns>
        public bool pickUp(int[] item)
        {
            for (int i = 0; i <= 5; i++)
            {
                if (inv[i, 0] == 0)
                {
                    inv[i, 0] = item[0];
                    inv[i, 1] = item[1];
                    inv[i, 2] = item[2];
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Removes an item from player's inventory
        /// </summary>
        /// <param name="invSpot">inventory space to remove the item from</param>
        public void removeItem(int invSpot)
        {
            inv[invSpot, 0] = 0;
            inv[invSpot, 1] = 0;
            inv[invSpot, 2] = 0;
        }
        public void damage(int invSpot, int damageValue)
        {
            if (inv[invSpot, 0] != 0)
            {
                inv[invSpot, 1] += damageValue;
            }
        }
    }
}
