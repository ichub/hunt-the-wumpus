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
    public static class ItemList
    {
        public static List<Item> Items { get; set; }

        /// <summary>
        /// Constructs default ingame items
        /// </summary>
        static ItemList()
        {
            ItemList.Items = new List<Item>();

            AddItem("Gold", 0);
            AddItem("Gem", 0);
            AddItem("Bow", 0); //hopefully the purpose is clear enough
            AddItem("Arrow", 10); //charges for bow
            AddItem("StandardHelmet", 100); //armor
            AddItem("StandardChest", 330); //armor
            AddItem("StandardLegs", 250); //armor
            AddItem("RefinedHelmet", 1000); //armor
            AddItem("RefinedChest", 2000); //armor
            AddItem("RefinedLegs", 1500); //amor
            AddItem("SpeedRing", 1000); //lets player move faster
            AddItem("HasteRing", 1500); //allows to deal more damage
            AddItem("InstilledRing", 2000); //when mobs hurt you, have a chance to not take damage
            AddItem("UpgradedSpeed Ring", 1000); //upgraded
            AddItem("UpgradedHaste Ring", 1500); //upgraded
            AddItem("UpgradedInstilled Ring", 2000); //upgraded
            AddItem("UltimateSpeedRing", 1000); //upgraded
            AddItem("UltimateHasteRing", 1500); //upgraded
            AddItem("UltimateInstilledRing", 2000); //upgraded
        }

        /// <summary>
        /// Adds the item to the item list.
        /// </summary>
        /// <param name="name"> Name of the item. </param>
        /// <param name="price"> Price of the item. </param>
        public static void AddItem(String name, int price)
        {
            ItemList.Items.Add(new Item(name, price));
        }

        public static Item GetItem(string name)
        {
            foreach (Item item in ItemList.Items)
            {
                if (item.Name == name)
                    return item;
            }
            return null;
        }
    }
}
