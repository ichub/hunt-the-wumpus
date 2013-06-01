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
                new Vector2(432, 0),
                new Vector2(542, 0),
                new Vector2(583, 62),
                new Vector2(646, 104),
                new Vector2(721, 169),
                new Vector2(789, 182),
                new Vector2(834, 136),
                new Vector2(936, 133),
                new Vector2(940, 144),
                new Vector2(990, 211),
                new Vector2(900, 250),
                new Vector2(853, 296),
                new Vector2(924, 336),
                new Vector2(948, 410),
                new Vector2(915, 496),
                new Vector2(826, 533),
                new Vector2(904, 595),
                new Vector2(930, 678),
                new Vector2(873, 736),
                new Vector2(772, 703),
                new Vector2(760, 645),
                new Vector2(578, 764),
                new Vector2(456, 768),
                new Vector2(310, 639),
                new Vector2(223, 710),
                new Vector2(113, 689),
                new Vector2(123, 592),
                new Vector2(223, 519),
                new Vector2(100, 363),
                new Vector2(298, 199),
                new Vector2(157, 121),
                new Vector2(272, 22),
                new Vector2(365, 101),
                new Vector2(427, 5),
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
            this.GameObjects.Add(new Teleporter(this.MainGame, this, this.AdjacentRooms[0]) { Position = new Vector2(420, -90) });
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
