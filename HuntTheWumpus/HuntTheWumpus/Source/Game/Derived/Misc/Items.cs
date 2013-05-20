using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpus.Source
{
    public class Items
    {
        public String name { set; get; }
        public String image { set; get; } //replace with appropriate object later
        public bool damageAllowed { set; get; }
        public int price { set; get; }

        /// <summary>
        /// Sets up a new item MUST ONLY BE CALLED BY ITEM.CS
        /// </summary>
        /// <param name="decName">Name</param>
        /// <param name="decImage">Image Path</param>
        /// <param name="decDamageAllowed">Damage allowed?</param>
        /// <param name="itemPrice">Price of an item in the shop</param>
        protected void newItem(String decName, String decImage, bool decDamageAllowed, int itemPrice)
        {
            name = decName;
            image = decImage;
            damageAllowed = decDamageAllowed;
            price = itemPrice;
        }
    }
}
