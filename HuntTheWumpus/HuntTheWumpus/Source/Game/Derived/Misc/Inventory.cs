using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace HuntTheWumpus.Source
{
    public enum RingType
    {
        Standard,
        Upgraded,
        Ultimate,
    }

    public class Inventory
    {
        public int[,] inv = new int[5, 3];
        public int[,] armor = new int[3, 3];
        public List<Map> maps = new List<Map>();
        public int numRoom;
        public int playerSpeed = 5;
        public List<Instilled> instilledRing = new List<Instilled>();
        public bool InstilledActive = false;
        /// <summary>
        /// Creates a new inventory 
        /// </summary>
        /// <param name="difficulty">0-3 difficulty value for starter values</param>
        public Inventory(int dif)
        {
            //generate items based on difficulty 
            inv[0, 0] = 1;
            inv[0, 1] = 0;
            inv[0, 2] = 1;

            inv[1, 0] = 2;
            inv[1, 1] = 0;
            inv[1, 2] = 0;
        }
        /// <summary>
        /// Pick up an item
        /// </summary>
        /// <param name="item">item array([0] = item id, [1] = item damage, [2] = item amount</param>
        /// <returns>whether the item was picked up or not</returns>
        public bool pickUp(int[] item)
        {
            for (int i = 0; i <= 5; i++)
            {
                if (inv[i, 0] == 0)
                {
                    inv[i, 0] = item[0];
                    inv[i, 1] = item[1];
                    inv[i, 2] = item[2];
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Removes an item from player's inventory
        /// </summary>
        /// <param name="invSpot">inventory space to remove the item from</param>
        public void removeItem(int invSpot)
        {
            inv[invSpot, 0] = 0;
            inv[invSpot, 1] = 0;
            inv[invSpot, 2] = 0;
        }

        /// <summary>
        /// Damage and item
        /// </summary>
        /// <param name="invSpot">spot in the invetory</param>
        /// <param name="damageValue">Applied damage value</param>
        public void damage(int invSpot, int damageValue)
        {
            if (inv[invSpot, 0] != 0)
            {
                inv[invSpot, 1] += damageValue;
            }
        }

        /// <summary>
        /// Check the inventory for special items needed to be initialized
        /// </summary>
        /// <param name="roomNum">Current room number (used to initialize a map)</param>
        public void checkSpecial(int roomNum)
        {
            for (int i = 0; i < 5; i++)
            {
                if (inv[i, 0] == 2 && inv[i, 2] == 0)
                {
                    maps.Add(new Map(roomNum));
                    numRoom = roomNum;
                    inv[i, 2] = maps.Count - 1;
                }

                if (inv[i, 0] == 9) playerSpeed = 7;
                else playerSpeed = 5;

                if (inv[i, 0] == 11 && inv[i, 2] == 0)
                {
                    InstilledActive = true;
                    instilledRing.Add(new Instilled(RingType.Standard));
                    inv[i, 2] = instilledRing.Count - 1;
                }
                if (inv[i, 0] == 11) InstilledActive = true;
                else InstilledActive = false;
                    
            }

        }

        /// <summary>
        /// Updates a map when moved to an unexplored room
        /// </summary>
        /// <param name="newRoom">new room number</param>
        /// <param name="direction">direction in which it moved </param> //TODO: fix the map not recording in the right place
        public void updateMap(int newRoom, int direction)
        {
            if (newRoom != numRoom)
            {
                foreach (Map activeMap in maps)
                {
                    activeMap.UpdateMap(direction, newRoom);
                }
                numRoom = newRoom;
            }
        }

        public double takeDamage(int damage)
        {
            double protectPoints = 0;
            for (int i = 0; i <= 3; i++)
            {
                protectPoints += new Armor(armor[i, 0]).takeDamage(damage);
            }
            protectPoints /= 3;
            if (InstilledActive && new Random().Next(100) > instilledRing[0].chance) return 0;
            return damage / protectPoints;
        }
        }
    }

