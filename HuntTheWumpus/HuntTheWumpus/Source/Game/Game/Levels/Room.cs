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
        
        /// <summary>
        /// Rooms that this room can lead to
        /// </summary>
        public Room[] ConnectedRooms { get; set; }
        /// <summary>
        /// All rooms adjacent to this room
        /// </summary>
        public Room[] AdjacentRooms { get; set; }

        public int RoomIndex { get; set; }
        public bool Initialized { get; set; }

        private Texture2D background;
        private AnimatedTexture[] walls;
        private Vector2[] wallPositions;
        private bool wumpusDrawn = false;
        private bool hasBeenVisited = false;

        public Room(MainGame mainGame, Cave gameCave, int index, Texture2D background, List<Vector2> bounds, RoomType type, AnimatedTexture[] walls)
        {
            this.MainGame = mainGame;
            this.MainCave = gameCave;
            this.GameObjects = new GameObjectManager(this.MainGame);
            this.RoomIndex = index;
            this.background = background;
            this.RoomBounds = bounds;
            this.RoomType = type;
            this.ConnectedRooms = new Room[6];
            this.walls = walls;

            this.wallPositions = new Vector2[] 
                {
                    new Vector2(445, 0),
                    new Vector2(782, 170),
                    new Vector2(760, 570),
                    new Vector2(450, 720),
                    new Vector2(154, 544),
                    new Vector2(157, 704),
                };
        }

        public void Initialize()
        {
        }

        public void PlaceTeleporters()
        {
            if (ConnectedRooms != null)
            {
                if (this.ConnectedRooms[0] != null)
                    this.GameObjects.Add(new Teleporter(this.MainGame, this, this.ConnectedRooms[0]) { Position = new Vector2(420, -90) });
                if (this.ConnectedRooms[1] != null)
                    this.GameObjects.Add(new Teleporter(this.MainGame, this, this.ConnectedRooms[1]) { Position = new Vector2(833, 170) });
                if (this.ConnectedRooms[2] != null)
                    this.GameObjects.Add(new Teleporter(this.MainGame, this, this.ConnectedRooms[2]) { Position = new Vector2(833, 644) });
                if (this.ConnectedRooms[3] != null)
                    this.GameObjects.Add(new Teleporter(this.MainGame, this, this.ConnectedRooms[3]) { Position = new Vector2(450, 763) });
                if (this.ConnectedRooms[4] != null)
                    this.GameObjects.Add(new Teleporter(this.MainGame, this, this.ConnectedRooms[4]) { Position = new Vector2(134, 627) });
                if (this.ConnectedRooms[5] != null)
                    this.GameObjects.Add(new Teleporter(this.MainGame, this, this.ConnectedRooms[5]) { Position = new Vector2(151, 81) });
            }
        }

        //TODO: Place correct walls
        private void DrawWalls(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (this.ConnectedRooms[0] == null)
                this.walls[0].Draw(spriteBatch, this.wallPositions[0], gameTime);
            if (this.ConnectedRooms[1] == null)
                this.walls[1].Draw(spriteBatch, this.wallPositions[1], gameTime);
            if (this.ConnectedRooms[2] == null)
                this.walls[2].Draw(spriteBatch, this.wallPositions[2], gameTime);
            if (this.ConnectedRooms[3] == null)
                this.walls[3].Draw(spriteBatch, this.wallPositions[3], gameTime);
            if (this.ConnectedRooms[4] == null)
                this.walls[4].Draw(spriteBatch, this.wallPositions[4], gameTime);
            if (this.ConnectedRooms[5] == null)
                this.walls[5].Draw(spriteBatch, this.wallPositions[5], gameTime);
        }

        public void OnLoad()
        {
            this.PlaceTeleporters();
            if (Room.FirstSpawn && this.RoomType == RoomType.Pit)
            {
                this.MainGame.LevelManager.CurrentLevel = this.MainGame.LevelManager.GameCave.Rooms[Math.Min(Math.Abs(30 - this.RoomIndex + 5), Math.Abs(30 - this.RoomIndex - 5))];
                Room.FirstSpawn = false;
                return;
            }

            this.GameObjects.Add(new PlayerAvatar(this.MainGame, this)
            {
                Position = new Vector2(500f, 200f)
            });

            this.SpawnEnemies();
            this.PlaceTeleporters();

            if (!this.hasBeenVisited)
            {
                this.GameObjects.Add(new Gold(this.MainGame, this) { Position = new Vector2(1024, 768) / 2 });
            }
        }

        private void SpawnEnemies()
        {
            if (this.MainCave.RoomContainsWumpus(this.RoomIndex))
            {
            }
            else if (this.MainCave.RoomContainsSuperBat(this.RoomIndex))
            {
                this.GameObjects.Add(new SuperBatAvatar(this.MainGame, this));
            }
            else
            {
                int amount = 2;
                for (int i = 0; i < amount; i++)
                {
                    this.GameObjects.Add(new Bat(this.MainGame, this) { Position = new Vector2(400, 400) + Helper.RandomVector(100) });
                }
            }
        }

        public void OnUnLoad()
        {
            this.wumpusDrawn = false;
            this.Reset();
        }

        public void Reset()
        {
            this.GameObjects = new GameObjectManager(this.MainGame);
        }

        public void FrameUpdate(GameTime gameTime, ContentManager content)
        {
            //Checks if Wumpus inhabits this room
            if (this.MainCave.Wumpus.RoomIndex == this.RoomIndex && !this.wumpusDrawn)
            {
                this.wumpusDrawn = true;
                Enemy newEnemy = new Wumpus(this.MainGame, this);
                newEnemy.Position = this.MainGame.InputManager.MousePosition - new Vector2(50, 50);
                this.GameObjects.Add(newEnemy);
            }
            this.GameObjects.FrameUpdate();
        }

        public void FrameDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.background, Vector2.Zero, Color.White);
            this.DrawWalls(spriteBatch, gameTime);
            this.GameObjects.FrameDraw();
        }
    }
}
