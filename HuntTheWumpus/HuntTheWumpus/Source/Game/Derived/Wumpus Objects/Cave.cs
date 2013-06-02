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
using HuntTheWumpus.Source.Game.Derived.Wumpus_Objects;

namespace HuntTheWumpus.Source
{
    public class Cave
    {
        public Room[] Rooms { get; set; }
        public MainGame MainGame { get; set; }
        public Rectangle CaveBounds { get; set; }
        public Vector2 CaveOffset { get; set; }
        public List<SuperBat> SuperBats { get; set; }

        public const int NumberOfRooms = 30;

        public Cave(MainGame mainGame, Vector2 windowSize)
        {
            this.MainGame = mainGame;
            this.Rooms = new Room[NumberOfRooms];
            this.CaveBounds = new Rectangle(((int)windowSize.X - 1024) / 2, ((int)windowSize.Y - 768) / 2, 1024, 768);
            this.CaveOffset = new Vector2(this.CaveBounds.X, this.CaveBounds.Y);

            for (int i = 0; i < this.Rooms.Length; i++)
            {
                this.Rooms[i] = RoomFactory.Create(this.MainGame, this, i);
            }

            for (int i = 0; i < this.Rooms.Length; i++)
            {
                Room[] rooms = SetUpAdjacentRooms(i);
                this.Rooms[i].AdjacentRooms = rooms;
            }

            //Initalize Super Bats
            this.SuperBats = new List<SuperBat>(3);
            Random rand = new Random();
            for (int i = 0; i < 3; i++)
            {
                int randomRoom = rand.Next(Cave.NumberOfRooms);
                if (this.SuperBats.Where((x) => x.ParentRoomIndex == randomRoom).Count() == 0)
                {
                    this.SuperBats.Add(new SuperBat(this.MainGame, randomRoom));
                }
                else
                {
                    i--;
                    continue;
                }
            }
            //Finished------------------------------------------------------------>
            this.DumpState();
        }

        public void UpdateSuperBats()
        {
            foreach (SuperBat bat in this.SuperBats)
            {
                bat.Update();
            }
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

        /// <summary>
        /// Converts given indecies to rooms
        /// </summary>
        /// <param name="roomIndecies"></param>
        /// <returns></returns>
        public Room[] ConvertIndeciesToRooms(int[] roomIndecies)
        {
            Room[] rooms = new Room[roomIndecies.Length];
            for (int i = 0; i < roomIndecies.Length; i++)
            {
                rooms[i] = this.Rooms[roomIndecies[i]];
            }
            return rooms;
        }

        /// <summary>
        /// Prints out to Debug
        /// </summary>
        public void DumpState()
        {
            foreach (Room currentRoom in this.Rooms)
            {
                Console.Write(currentRoom.RoomIndex + " : ");

                foreach (Room adjRoom in currentRoom.AdjacentRooms)
                {
                    Console.Write(adjRoom.RoomIndex + " , ");
                }
                Console.Write("\n");
            }
        }
    }
}