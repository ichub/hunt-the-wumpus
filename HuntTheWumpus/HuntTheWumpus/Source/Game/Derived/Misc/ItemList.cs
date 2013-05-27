using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace HuntTheWumpus.Source
{
    class ItemList
    {
        public List<Item> Items { get; set; }
        
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
        /// Adds the item to the item list.
        /// </summary>
        /// <param name="name"> Name of the item. </param>
        /// <param name="price"> Price of the item. </param>
        public void AddItem(String name, int price)
        {
            this.Items.Add(new Item(name, price));  
        }
    }
}
