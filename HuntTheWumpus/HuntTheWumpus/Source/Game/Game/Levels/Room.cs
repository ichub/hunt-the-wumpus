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
    public class Room : ILevel
    {
        public static bool FirstSpawn = true;

        public List<Vector2> RoomBounds { get; set; }
        public RoomType RoomType { get; set; }

        public MainGame MainGame { get; set; }
        public Cave MainCave { get; set; }
        public GameObjectManager GameObjects { get; set; }
        public Room[] AdjacentRooms { get; set; }

        public int RoomIndex { get; set; }
        public bool Initialized { get; set; }

        private Texture2D background;

        public Room(MainGame mainGame, Cave gameCave, int index, Texture2D background, List<Vector2> bounds, RoomType type)
        {
            this.MainGame = mainGame;
            this.MainCave = gameCave;
            this.GameObjects = new GameObjectManager(this.MainGame);
            this.RoomIndex = index;
            this.background = background;
            this.RoomBounds = bounds;
            this.RoomType = type;
            this.AdjacentRooms = new Room[6];
        }

        public void Initialize()
        {
            this.PlaceTeleporters();
        }

        public void PlaceTeleporters()
        {
            if (AdjacentRooms != null)
            {
                if (this.AdjacentRooms[0] != null)
                    this.GameObjects.Add(new Teleporter(this.MainGame, this, this.AdjacentRooms[0]) { Position = new Vector2(420, -90) });
                if (this.AdjacentRooms[1] != null)
                    this.GameObjects.Add(new Teleporter(this.MainGame, this, this.AdjacentRooms[1]) { Position = new Vector2(833, 170) });
                if (this.AdjacentRooms[2] != null)
                    this.GameObjects.Add(new Teleporter(this.MainGame, this, this.AdjacentRooms[2]) { Position = new Vector2(815, 687) });
                if (this.AdjacentRooms[3] != null)
                    this.GameObjects.Add(new Teleporter(this.MainGame, this, this.AdjacentRooms[3]) { Position = new Vector2(450, 763) });
                if (this.AdjacentRooms[4] != null)
                    this.GameObjects.Add(new Teleporter(this.MainGame, this, this.AdjacentRooms[4]) { Position = new Vector2(134, 627) });
                if (this.AdjacentRooms[5] != null)
                    this.GameObjects.Add(new Teleporter(this.MainGame, this, this.AdjacentRooms[5]) { Position = new Vector2(151, 81) });
            }
        }

        public void OnLoad()
        {
            if (FirstSpawn && this.RoomType == RoomType.Pit)
            {
                this.MainGame.LevelManager.CurrentLevel = this.MainGame.LevelManager.GameCave.Rooms[Math.Min(Math.Abs(30 - this.RoomIndex + 5), Math.Abs(30 - this.RoomIndex - 5))];
                FirstSpawn = false;
                return;
            }
            this.GameObjects.Add(new PlayerAvatar(this.MainGame, this) { Position = new Vector2(500, 200) });
            this.SpawnEnemies();
            this.PlaceTeleporters();
        }

        private void SpawnEnemies()
        {
            int amount = 2;
            for (int i = 0; i < amount; i++)
            {
                this.GameObjects.Add(new Bat(this.MainGame, this) { Position = new Vector2(400, 400) + Extensions.RandomVector(100) });
            }
        }

        public void OnUnLoad()
        {
            this.Reset();
        }

        public void Reset()
        {
            this.GameObjects = new GameObjectManager(this.MainGame);
        }

        public void FrameUpdate(GameTime gameTime, ContentManager content)
        {
            //Checks if Wumpus inhabits this room
            if (this.MainCave.Wumpus.RoomIndex == this.RoomIndex)
            {
                Enemy newEnemy = new Wumpus(this.MainGame, this);
                newEnemy.Position = this.MainGame.InputManager.MousePosition - new Vector2(50, 50);
                this.GameObjects.Add(newEnemy);
            }
            this.GameObjects.FrameUpdate();
        }

        public void FrameDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.background, new Vector2(0), Color.White);
            this.GameObjects.FrameDraw();
        }
    }
}
