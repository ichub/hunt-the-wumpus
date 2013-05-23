using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HuntTheWumpus.Source;

namespace HuntTheWumpus
{
    public class Map
    {
        public int[,] mapper = new int[5, 6]; //Declares a new map
        public int x; //X value on the map
        public int y; //Y value on the map

        /// <summary>
        /// Initializes a new map, centers on current room
        /// </summary>
        /// <param name="currentLevel">Current room</param>
        public Map(int currentLevel) 
        {
            mapper[3, 3] = currentLevel;
            x = 3;
            y = 3;
        }

        /// <summary>
        /// Updates the map to include newly explored room
        /// </summary>
        /// <param name="direction">Direction in which the room is</param>
        /// <param name="currentlevel">New room number</param>
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
