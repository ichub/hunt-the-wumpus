using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpus.Source
{
    public class Item
    {
        public string Name { set; get; }
        public int Price { get; set; }

        public Item(string name, int price)
        {
            this.Name = name;
            this.Price = price;
        }
    }
}
