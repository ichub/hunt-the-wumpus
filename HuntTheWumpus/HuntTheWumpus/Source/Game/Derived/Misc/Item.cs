using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpus.Source
{
    class Item : Items
    {
        protected List<Items> thing = new List<Items>(); //list of items
        
        /// <summary>
        /// Constructs default ingame items
        /// </summary>
        public Item()
        {
            this.AddItem("Bow", "Holder", true, 0); //hopefully the purpose is clear enough
            this.AddItem("Arrow", "Holder", true, 10); //charges for bow
            this.AddItem("Map", "Holder", false, 100); //Map to show location of various items
            this.AddItem("Standard Helmet", "Holder", true, 100); //armor
            this.AddItem("Standard Chest", "Holder", true, 330); //armor
            this.AddItem("Standard legs", "Holder", true, 250); //armor
            this.AddItem("Refined Helmet", "Holder", true, 1000); //armor
            this.AddItem("Refined Chest", "Holder", true, 2000); //armor
            this.AddItem("Refined Legs", "Holder", true, 1500); //amor
            this.AddItem("Speed Ring", "Holder", false, 1000); //lets player move faster
            this.AddItem("Haste Ring", "Holder", false, 1500); //allows to deal more damage
            this.AddItem("Instilled Ring", "Holder", false, 2000); //when mobs hurt you, have a chance to not take damage
            this.AddItem("Upgraded Speed Ring", "Holder", false, 1000); //upgraded
            this.AddItem("Upgraded Haste Ring", "Holder", false, 1500); //upgraded
            this.AddItem("UpgradedInstilled Ring", "Holder", false, 2000); //upgraded
            this.AddItem("Ultimate Speed Ring", "Holder", false, 1000); //upgraded
            this.AddItem("Ultimate Haste Ring", "Holder", false, 1500); //upgraded
            this.AddItem("Ultimate Instilled Ring", "Holder", false, 2000); //upgraded
        }

        /// <summary>
        /// Allows to add items outside vanilla items
        /// </summary>
        /// <param name="decName">Name</param>
        /// <param name="decImage">Image Path</param>
        /// <param name="decDamageAllowed">Damage allowed?</param>
        /// <param name="itemPrice">Price of an item in the shop</param>
        public void AddItem(String decName, String decImage, bool decDamageAllowed, int itemPrice)
        {
            Items it = new Items(); 
            //it.id= decID;
            it.name= decName;
            it.image= decImage;
            it.damageAllowed = decDamageAllowed;
            thing.Add(it);  
        }
    }
}
