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
        public List<SuperBat> SuperBats { get; set; }

        public const int NumberOfRooms = 30;
        public const int NumberOfConnections = 3;

        private Random Random;
        private bool[] TakenRooms;

        public Cave(MainGame mainGame)
        {
            this.MainGame = mainGame;
            this.Rooms = new Room[NumberOfRooms];
            this.Random = new Random(DateTime.Now.Millisecond);
            this.TakenRooms = new bool[Cave.NumberOfRooms];
            for (int i = 0; i < this.Rooms.Length; i++)
            {
                this.Rooms[i] = RoomFactory.Create(this.MainGame, this, i);
            }

            while (!Cave.AreAllRoomsAccessible(this.Rooms))
            {
                this.TakenRooms = new bool[Cave.NumberOfRooms];

                for (int i = 0; i < this.Rooms.Length; i++)
                {
                    Room[] rooms = SetUpAdjacentRooms(i);
                    this.Rooms[i].AdjacentRooms = rooms;
                }
            }

            #region Super Bat Stuff
            //Initalize Super Bats
            this.SuperBats = new List<SuperBat>(3);
            Random rand = new Random();
            //Spec says 2 SuperBats
            for (int i = 0; i < 2; i++)
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
            #endregion
            #region Pit Room Stuff
            //Pick Random Pit Room
            int pitRoom = this.Random.Next(Cave.NumberOfRooms);
            this.Rooms[pitRoom] = RoomFactory.Create(this.MainGame, this, RoomType.Pit, pitRoom);

            foreach (var item in this.Rooms)
            {
                if (item.AdjacentRooms == null)
                    continue;
                //Make sure all the connection to the pit are there
                for (int j = 0; j < item.AdjacentRooms.Length; j++)
                {
                    if (item.AdjacentRooms[j] != null && item.AdjacentRooms[j].RoomIndex == pitRoom)
                    {
                        item.AdjacentRooms[j] = RoomFactory.Create(this.MainGame, this, RoomType.Pit, pitRoom);
                    }
                }
            }
            #endregion
        }

        public void UpdateSuperBats()
        {
            foreach (SuperBat bat in this.SuperBats)
            {
                bat.Update();
            }
        }
        /// <summary>
        /// Sets up the adjacnt rooms for one room:
        /// </summary>
        /// <param name="roomIndex">the index of the room in this.Rooms</param>
        /// <returns>Array of rooms that represent its connections</returns>
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

            int[] randomlyChosenConnections = Cave.GetRelevantIndexes(this.TakenRooms, adjacentRoomIndecies);
            foreach (int item in randomlyChosenConnections)
            {
                if (item == -1)
                    continue;
                this.TakenRooms[item] = true;
            }
            return ConvertIndeciesToRooms(randomlyChosenConnections);
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
                if (roomIndecies[i] == -1)
                    continue;
                rooms[i] = this.Rooms[roomIndecies[i]];
            }
            return rooms;
        }
        /// <summary>
        /// Checks if all rooms are accessible.
        /// </summary>
        /// <param name="rooms">The array of all rooms</param>
        /// <returns>True if all rooms are accessible and false if otherwise</returns>
        public static bool AreAllRoomsAccessible(Room[] rooms)
        {
            if (rooms.Length < Cave.NumberOfRooms)
                return false;
            bool[] accessRooms = new bool[Cave.NumberOfRooms];
            foreach (Room item in rooms)
            {
                if (item.AdjacentRooms != null)
                {
                    foreach (Room adjRoom in item.AdjacentRooms)
                    {
                        if (adjRoom == null)
                            continue;
                        if (adjRoom.RoomIndex == -1)
                            continue;
                        accessRooms[adjRoom.RoomIndex] = true;
                    }
                }
            }
            return (!accessRooms.Any((x) => x == false));
        }
        /// <summary>
        /// Get Rooms that havent yet had a connection
        /// </summary>
        /// <param name="takenRooms"></param>
        /// <param name="possibleRooms"></param>
        /// <returns></returns>
        public static int[] GetRelevantIndexes(bool[] takenRooms, int[] possibleRooms)
        {
            int[] rooms = Extensions.ChangeIndecesToNumber(possibleRooms.Length, -1);
            Random rand = new Random(DateTime.Now.Millisecond);

            int index = 0;
            foreach (int item in possibleRooms)
            {
                if (index >= Cave.NumberOfConnections)
                    continue;

                if (!takenRooms[item])
                {
                    rooms[possibleRooms.IndexOf(item)] = item;
                    index++;
                }
            }
            while (index < Cave.NumberOfConnections)
            {
                int toAdd = possibleRooms[rand.Next(6)];
                while (rooms.Any((x) => x == toAdd))
                {
                    toAdd = possibleRooms[rand.Next(6)];
                }
                rooms[possibleRooms.IndexOf(toAdd)] = toAdd;
                index++;
            }
            return rooms;
        }
    }
}