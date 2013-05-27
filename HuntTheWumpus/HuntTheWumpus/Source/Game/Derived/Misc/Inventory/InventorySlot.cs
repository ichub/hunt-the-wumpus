using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpus.Source
{
    public class InventorySlot
    {
        public Item HeldItem { get; set; }
        public int Amount { get; set; }
    }
}
