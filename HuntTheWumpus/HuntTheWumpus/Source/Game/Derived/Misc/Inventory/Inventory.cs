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
        public List<InventorySlot> Slots { get; set; }

        /// <summary>
        /// Creates a new inventory 
        /// </summary>
        /// <param name="difficulty">0-3 difficulty value for starter values</param>
        public Inventory(int size)
        {
            this.Slots = new List<InventorySlot>();
        }

        /// <summary>
        /// Adds the given item to the inventory
        /// </summary>
        /// <param name="item"> Item to pick up. </param>
        public void PickUp(Item item)
        {
            int index = this.ItemIndex(item);

            if (index != -1)
            {
                this.Slots[index].Amount++;
            }
            else
            {
                this.Slots.Add(new InventorySlot() { HeldItem = item, Amount = 1 });
            }
        }

        private int ItemIndex(Item item)
        {
            for (int i = 0; i < this.Slots.Count; i++)
            {
                if (this.Slots[i].HeldItem.Name == item.Name)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}