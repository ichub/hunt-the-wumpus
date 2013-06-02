using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace HuntTheWumpus.Source.Game.Derived.Wumpus_Objects
{
    public class SuperBat
    {
        public MainGame MainGame { get; set; }
        public int ParentRoomIndex { get; set; }
        public Random RandomGenerator { get; set; }


        public SuperBat(MainGame game, int room)
        {
            this.MainGame = game;
            this.ParentRoomIndex = room;
            this.RandomGenerator = new Random();
        }

        public void Update()
        {
            Room currentRoom = this.MainGame.LevelManager.CurrentLevel as Room;
            if (null != currentRoom)
            {
                if (this.ParentRoomIndex == currentRoom.RoomIndex)
                {
                    //Later on, run animation
                    //of huge bat swooping in and grabing the player
                    Debug.WriteLine("Met Super Bat");

                    int newRoom = this.RandomGenerator.Next(0, Cave.NumberOfRooms);

                    List<PlayerAvatar> avatars = this.MainGame.LevelManager.CurrentLevel.GameObjects.GetObjectsByType<PlayerAvatar>();
                    if (avatars.Count == 0)
                        return;

                    PlayerAvatar player = avatars[0];
                    player.MoveRoom(newRoom);

                    this.MainGame.MiniMap.ShowRoom(this.MainGame.LevelManager.GameCave.Rooms[newRoom]);
                    this.ParentRoomIndex = newRoom;
                }
            }
        }
    }
}
