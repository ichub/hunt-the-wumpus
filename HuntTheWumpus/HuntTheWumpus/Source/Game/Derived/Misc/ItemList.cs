using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpus.Source
{
    class ItemList
    {
        protected List<Item> Items { get; set; }
        
        /// <summary>
        /// Constructs default ingame items
        /// </summary>
        public ItemList()
        {
            this.Items = new List<Item>();
            this.AddItem("Bow", 0); //hopefully the purpose is clear enough
            this.AddItem("Arrow", 10); //charges for bow
            this.AddItem("Standard Helmet", 100); //armor
            this.AddItem("Standard Chest", 330); //armor
            this.AddItem("Standard legs", 250); //armor
            this.AddItem("Refined Helmet", 1000); //armor
            this.AddItem("Refined Chest", 2000); //armor
            this.AddItem("Refined Legs", 1500); //amor
            this.AddItem("Speed Ring", 1000); //lets player move faster
            this.AddItem("Haste Ring", 1500); //allows to deal more damage
            this.AddItem("Instilled Ring", 2000); //when mobs hurt you, have a chance to not take damage
            this.AddItem("Upgraded Speed Ring", 1000); //upgraded
            this.AddItem("Upgraded Haste Ring", 1500); //upgraded
            this.AddItem("UpgradedInstilled Ring", 2000); //upgraded
            this.AddItem("Ultimate Speed Ring", 1000); //upgraded
            this.AddItem("Ultimate Haste Ring", 1500); //upgraded
            this.AddItem("Ultimate Instilled Ring", 2000); //upgraded
        }

        /// <summary>
        /// Allows to add items outside vanilla items
        /// </summary>
        /// <param name="decName">Name</param>
        /// <param name="decImage">Image Path</param>
        /// <param name="decDamageAllowed">Damage allowed?</param>
        /// <param name="itemPrice">Price of an item in the shop</param>
        public void AddItem(String name, int price)
        {
            Item it = new Item(name, price); 
            //it.id= decID;
            it.Name= name;
            it.Price = price;
            this.Items.Add(it);  
        }
    }
}
