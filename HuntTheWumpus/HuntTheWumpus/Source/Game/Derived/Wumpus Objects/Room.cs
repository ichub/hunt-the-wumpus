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
        public MainGame MainGame { get; set; }
        public Cave MainCave { get; set; }
        public GameObjectManager GameObjects { get; set; }
        public Room[] AdjacentRooms { get; set; }

        public int RoomIndex { get; set; }
        public bool Initialized { get; set; }

        private Texture2D background;

        public Room(MainGame mainGame, Cave gameCave, int index)
        {
            this.MainGame = mainGame;
            this.MainCave = gameCave;
            this.GameObjects = new GameObjectManager(this.MainGame);
            this.RoomIndex = index;
            this.background = mainGame.Content.Load<Texture2D>("Textures\\Cave\\normal");
        }

        public void Initialize()
        {
            this.PlaceTeleporters();
        }

        public void PlaceTeleporters()
        {
            this.GameObjects.Add(new Teleporter(this.MainGame, this, this.AdjacentRooms[0]) { Position = new Vector2(300, 0)});
            this.GameObjects.Add(new Teleporter(this.MainGame, this, this.AdjacentRooms[1]) { Position = new Vector2(500, 100) });
            this.GameObjects.Add(new Teleporter(this.MainGame, this, this.AdjacentRooms[2]) { Position = new Vector2(500, 400) });
            this.GameObjects.Add(new Teleporter(this.MainGame, this, this.AdjacentRooms[3]) { Position = new Vector2(300, 500) });
            this.GameObjects.Add(new Teleporter(this.MainGame, this, this.AdjacentRooms[4]) { Position = new Vector2(0, 400) });
            this.GameObjects.Add(new Teleporter(this.MainGame, this, this.AdjacentRooms[5]) { Position = new Vector2(0, 200) });
        }

        public void OnLoad()
        {
            this.GameObjects.Add(new Enemy(this.MainGame, this) { Position = new Vector2(new Random().Next(700), new Random().Next(500)) });
            this.GameObjects.Add(new PlayerAvatar(this.MainGame, this) { Position = new Vector2(400, 300) });
            this.PlaceTeleporters();
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
                var newEnemy = new Enemy(this.MainGame, this);
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
