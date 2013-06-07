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
            AddItem("SpeedRing", 1000); //lets player move faster
            AddItem("Gem", 0);
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
