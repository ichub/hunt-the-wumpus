using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpus.Source
{
    class Item
    {
        private List<Items> thing = new List<Items>();
        public Item()
        {
            this.AddItem(1, "Bow", "Holder", true);
            this.AddItem(2, "Arrow", "Holder", true);
            this.AddItem(3, "Map", "Holder", false);
            this.AddItem(4, "Standard Helmet", "Holder", true);
            this.AddItem(5, "Standard Chest", "Holder", true);
            this.AddItem(6, "Standard legs", "Holder", true);
            this.AddItem(7, "Refined Helmet", "Holder", true); 
            this.AddItem(8, "Refined Chest", "Holder", true);
            this.AddItem(9, "Refined Legs", "Holder", true); 
        }

        public void AddItem(int decID, String decName, String decImage, bool decDamageAllowed)
        {
            Items it = new Items(); 
            it.id= decID;
            it.name= decName;
            it.image= decImage;
            it.damageAllowed = decDamageAllowed;
            thing.Add(it);  
        }
    }
}
