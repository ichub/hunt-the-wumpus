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
    public class Cave
    {
        public Room[] Rooms { get; set; }
        public MainGame MainGame { get; set; }
        public Rectangle CaveBounds { get; set; }
        public Vector2 CaveOffset { get; set; }

        public Cave(MainGame mainGame, Vector2 windowSize)
        {
            this.MainGame = mainGame;
            this.Rooms = new Room[5 * 6];
            this.CaveBounds = new Rectangle(((int)windowSize.X - 960) / 2, ((int)windowSize.Y - 960) / 2, 960, 960);
            this.CaveOffset = new Vector2(this.CaveBounds.X, this.CaveBounds.Y);

            for (int i = 0; i < this.Rooms.Length; i++)
            {
                this.Rooms[i] = new Room(this.MainGame, this, i);
            }

            for (int i = 0; i < this.Rooms.Length; i++)
            {
                var a = SetUpAdjacentRooms(i);
                this.Rooms[i].AdjacentRooms = a;
            }

            this.DumpState();
        }

        public Room[] SetUpAdjacentRooms(int roomIndex)
        {
            int[] adjacentRoomIndecies = new int[6];

            // casework to create an array of adjacent rooms

            adjacentRoomIndecies[0] = (roomIndex + 24) % 30;
            adjacentRoomIndecies[3] = (roomIndex + 6) % 30;

            if (roomIndex % 2 == 1)
            {
                adjacentRoomIndecies[2] = (roomIndex + 1) % 30;
                adjacentRoomIndecies[1] = (roomIndex + 25) % 30;
                if (roomIndex % 6 == 1)
                {
                    adjacentRoomIndecies[4] = (roomIndex + 5) % 30;
                    adjacentRoomIndecies[5] = (roomIndex + 29) % 30;
                }
                else
                {
                    adjacentRoomIndecies[4] = (roomIndex + 29) % 30;
                    adjacentRoomIndecies[5] = (roomIndex + 23) % 29;
                }
            }
            else
            {
                adjacentRoomIndecies[4] = (roomIndex + 5) % 30;
                adjacentRoomIndecies[5] = (roomIndex + 29) % 30;
                if (roomIndex % 6 == 2 || roomIndex % 6 == 4)
                {
                    adjacentRoomIndecies[1] = (roomIndex + 1) % 30;
                    adjacentRoomIndecies[2] = (roomIndex + 7) % 30;
                }
                else
                {
                    adjacentRoomIndecies[1] = (roomIndex + 25) % 30;
                    adjacentRoomIndecies[2] = (roomIndex + 1) % 30;
                }
            }

            return ConvertIndeciesToRooms(adjacentRoomIndecies);

        }

        public Room[] ConvertIndeciesToRooms(int[] roomIndecies)
        {
            Room[] rooms = new Room[roomIndecies.Length];
            for (int i = 0; i < roomIndecies.Length; i++)
            {
                rooms[i] = this.Rooms[roomIndecies[i]];
            }
            return rooms;
        }

        public void DumpState()
        {
            foreach (var item in this.Rooms)
            {
                Console.Write(item.RoomIndex + " : ");

                foreach (var adj in item.AdjacentRooms)
                {
                    Console.Write(adj.RoomIndex + " , ");
                }
                Console.Write("\n");
            }
        }
    }
}
