using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HuntTheWumpus.Source;

namespace HuntTheWumpus
{
    public class Map
    {
        public int[,] mapper = new int[5, 6];
        public int x;
        public int y;
        public Map(int currentLevel) 
        {
            mapper[3, 3] = currentLevel;
            x = 3;
            y = 3;
        }

        public void UpdateMap(int direction, int currentlevel)
        {
            if (direction == 0)
            {
                x++;
            }
            else if (direction == 1)
            {
                y++;
            }
            else if (direction == 2)
            {
                x--;
            }
            else if (direction == 3)
            {
                y--;
            }
            mapper[x, y] = currentlevel;
        }
    }
}
