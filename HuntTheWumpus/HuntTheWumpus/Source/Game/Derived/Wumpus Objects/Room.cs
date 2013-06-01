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
        public static List<Vector2> RoomBounds { get; set; }

        public MainGame MainGame { get; set; }
        public Cave MainCave { get; set; }
        public GameObjectManager GameObjects { get; set; }
        public Room[] AdjacentRooms { get; set; }

        public int RoomIndex { get; set; }
        public bool Initialized { get; set; }

        private Texture2D background;

        /// <summary>
        /// Actually initialize the correct bounds.
        /// </summary>
        static Room()
        {
            RoomBounds = new List<Vector2>()
            {
                new Vector2(20f,20f),
                new Vector2(30f,800f),
                new Vector2(13f,312f),
                new Vector2(440f,5f),
                new Vector2(440f,5f),
                new Vector2(543f,5f),
                new Vector2(944f,272f),
                new Vector2(1018f,398f),
                new Vector2(1009f,441f),
                new Vector2(909f,553f),
                new Vector2(865f,563f),
                new Vector2(846f,621f),
                new Vector2(801f,659f),
                new Vector2(648f,763f),
                new Vector2(391f,765f),
                new Vector2(202f,590f),
                new Vector2(156f,509f),
                new Vector2(9f,439f),
                new Vector2(13f,312f),
            };
        }

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
            this.GameObjects.Add(new Teleporter(this.MainGame, this, this.AdjacentRooms[0]) { Position = new Vector2(420, 0) });
            this.GameObjects.Add(new Teleporter(this.MainGame, this, this.AdjacentRooms[1]) { Position = new Vector2(833, 170) });
            this.GameObjects.Add(new Teleporter(this.MainGame, this, this.AdjacentRooms[2]) { Position = new Vector2(815, 687) });
            this.GameObjects.Add(new Teleporter(this.MainGame, this, this.AdjacentRooms[3]) { Position = new Vector2(450, 763) });
            this.GameObjects.Add(new Teleporter(this.MainGame, this, this.AdjacentRooms[4]) { Position = new Vector2(134, 627) });
            this.GameObjects.Add(new Teleporter(this.MainGame, this, this.AdjacentRooms[5]) { Position = new Vector2(151, 81) });
        }

        public void OnLoad()
        {
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
