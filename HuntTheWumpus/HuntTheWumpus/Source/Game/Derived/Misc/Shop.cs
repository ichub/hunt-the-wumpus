using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpus.Source.Game.Derived.Misc
{
    class Shop : Item
    {

        /// <summary>
        /// Returns whether a person may buy this shop item
        /// </summary>
        /// <param name="item">Item to check</param>
        /// <param name="money">balance of the player</param>
        /// <returns></returns>
        public bool isValid(Items item, int money)
        {
            return item.price <= money;
        }

        public Player buyItem(Player play, Items item)
        {
            if (!this.isValid(item, play.money))
            {
                return play;
            }
            play.money -= item.price;
            return play;
        }
    }
}
