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
    public class Inventory
    {
        /// <summary>
        /// Indexer to access the slots
        /// </summary>
        /// <param name="index">the index</param>
        /// <returns>an Inventory Slot</returns>
        public InventorySlot this[int index]
        {

            get
            {
                return this.Slots[index];
            }

        }
        public int Length
        {
            get
            {
                return this.Slots.Count;
            }
        }
        private List<InventorySlot> Slots { get; set; }

        /// <summary>
        /// Creates a new inventory 
        /// </summary>
        /// <param name="difficulty">0-3 difficulty value for starter values</param>
        public Inventory(int size)
        {
            this.Slots = new List<InventorySlot>();
            this.Slots.Add(new InventorySlot() { Amount = 0, HeldItem = ItemList.GetItem("Gold") });
        }

        /// <summary>
        /// Adds the given item to the inventory
        /// </summary>
        /// <param name="item"> Item to pick up. </param>
        public void PickUp(Item item)
        {
            int index = this.ItemIndex(item.Name);

            if (index != -1)
            {
                this.Slots[index].Amount++;
            }
            else
            {
                this.Slots.Add(new InventorySlot() { HeldItem = item, Amount = 1 });
            }
        }

        /// <summary>
        /// Gets the index of the item corresponding to the given item in Slots.
        /// </summary>
        /// <param name="item"> Item to find. </param>
        /// <returns> Index if the item exists, -1 otherwise. </returns>
        private int ItemIndex(string name)
        {
            for (int i = 0; i < this.Slots.Count; i++)
            {
                if (this.Slots[i].HeldItem.Name == name)
                {
                    return i;
                }
            }
            return -1;
        }
        /// <summary>
        /// Gets the amount of gold the player has.
        /// </summary>
        /// <returns>return the amount of gold</returns>
        public int AmountOfGold()
        {
            int itemIndex = ItemIndex("Gold");
            if (itemIndex > 0)
            {
                return this.Slots[itemIndex].Amount;
            }
            return 0;
        }
        /// <summary>
        /// Gets the amount of gems the player has.
        /// </summary>
        /// <returns>return the amount of gold</returns>
        public int AmountOfGems()
        {
            int itemIndex = ItemIndex("Gem");
            if (itemIndex > 0)
            {
                return this.Slots[itemIndex].Amount;
            }
            return 0;
        }
        /// <summary>
        /// Checks if item is in slot
        /// </summary>
        /// <param name="st">name of item</param>
        /// <returns>true if yes,otherwise false</returns>
        public bool Contains(string st)
        {
            return this.Slots.Any((x) => x.HeldItem.Name == st);
        }
    }
}