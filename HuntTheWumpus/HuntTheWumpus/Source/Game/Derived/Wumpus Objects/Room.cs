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
    public class Room : ILevel
    {
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
        }

        public void Initialize()
        {
            this.PlaceTeleporters();
        }

        public void PlaceTeleporters()
        {
            this.GameObjects.Add(new Teleporter(this.MainGame, this, this.AdjacentRooms[0]) { Position = new Vector2(420, -90) });
            this.GameObjects.Add(new Teleporter(this.MainGame, this, this.AdjacentRooms[1]) { Position = new Vector2(833, 170) });
            this.GameObjects.Add(new Teleporter(this.MainGame, this, this.AdjacentRooms[2]) { Position = new Vector2(815, 687) });
            this.GameObjects.Add(new Teleporter(this.MainGame, this, this.AdjacentRooms[3]) { Position = new Vector2(450, 763) });
            this.GameObjects.Add(new Teleporter(this.MainGame, this, this.AdjacentRooms[4]) { Position = new Vector2(134, 627) });
            this.GameObjects.Add(new Teleporter(this.MainGame, this, this.AdjacentRooms[5]) { Position = new Vector2(151, 81) });
        }

        public void OnLoad()
        {
            this.GameObjects.Add(new PlayerAvatar(this.MainGame, this) { Position = new Vector2(500, 200) });
            this.SpawnEnemies();
            this.PlaceTeleporters();
        }

        private void SpawnEnemies()
        {
            int amount = 2;
            for (int i = 0; i < amount; i++)
            {
                this.GameObjects.Add(new Enemy(this.MainGame, this) { Position = new Vector2(400, 400) + Extensions.RandomVector(100) });
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
            // temporary debug feature begin
            if (this.MainGame.InputManager.IsClicked(MouseButton.Left))
            {
                Enemy newEnemy = new Enemy(this.MainGame, this);
                newEnemy.Position = this.MainGame.InputManager.MousePosition - new Vector2(50, 50);
                this.GameObjects.Add(newEnemy);
            }
            // temporary debug deature end

            this.GameObjects.FrameUpdate();
        }

        public void FrameDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.background, this.MainCave.CaveOffset, Color.White);
            this.GameObjects.FrameDraw();
        }
    }
}
