using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpus.Source.Main
{
    public class Items
    {
        public int id { set; get; }
        public String name { set; get; }
        public String image { set; get; } //replace with appropriate object later
        public bool damageAllowed { set; get; }

        /// <summary>
        /// Sets up a new item MUST ONLY BE CALLED BY ITEM.CS
        /// </summary>
        /// <param name="decID">Item ID</param>
        /// <param name="decName">Name</param>
        /// <param name="decImage">Image Path</param>
        /// <param name="decDamageAllowed">Damage allowed?</param>
        public void newItem(int decID, String decName, String decImage, bool decDamageAllowed)
        {
            id = decID;
            name = decName;
            image = decImage;
            damageAllowed = decDamageAllowed; 
        }
    }
}
