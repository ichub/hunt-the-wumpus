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
using System.Diagnostics;

namespace HuntTheWumpus.Source
{
    public class MiniMap
    {
        public const int DefaultRoomNumber = 30;
        public List<Vector2> TopLeftPoints { get; private set; }
        public MainGame MainGame { get; set; }

        public readonly float xSize = 44;
        public readonly float ySize = 36.5f;

        public Vector2 Shift { get; set; }

        private List<Vector2> CenterPoints;

        private bool[] IndexesToShow;
        private bool Showing = false;
        private Room CurrentRoom;

        /// <summary>
        /// Initialize.
        /// </summary>
        private MiniMap(Vector2 initialShift)
        {
            this.CenterPoints = new List<Vector2>(MiniMap.DefaultRoomNumber);
            this.TopLeftPoints = new List<Vector2>(MiniMap.DefaultRoomNumber);
            this.IndexesToShow = new bool[MiniMap.DefaultRoomNumber];
            this.Shift = initialShift;
            this.InitCenterPoints();
            this.InitTopLeftPoints();
        }

        public MiniMap(MainGame parentGame, Vector2 initialShift)
            : this(initialShift)
        {
            if (null == parentGame)
            {
                throw new ArgumentNullException("parentGame");
            }

            this.MainGame = parentGame;
        }

        public MiniMap(int width, int height, Vector2 initialShift)
            : this(initialShift)
        {
            if (width <= 0)
            {
                throw new ArgumentException("width");
            }

            if (height <= 0)
            {
                throw new ArgumentException("height");
            }

            this.xSize = width;
            this.ySize = height;
        }

        /// <summary>
        /// Show Room In Cave
        /// </summary>
        /// <param name="index"></param>
        public void ShowRoom(Room room)
        {
            if (null != room)
            {
                this.IndexesToShow[room.RoomIndex] = true;
                this.CurrentRoom = room;
            }
        }
        /// <summary>
        /// Hide Room In Cave
        /// </summary>
        /// <param name="index"></param>
        public void HideRoom(int index)
        {
            this.IndexesToShow[index] = false;
        }
        /// <summary>
        /// Initialize points
        /// </summary>
        private void InitCenterPoints()
        {
            float halfX = xSize / 2;
            float halfY = ySize / 2;

            for (int i = 1; i < 31; i++)
            {
                if (i % 6 == 1)
                {
                    this.CenterPoints.Add(
                        new Vector2(halfX,
                            ((i - 1) / 3 + 1) * halfY));
                }
                else if (i % 6 == 2)
                {
                    Vector2 current = this.CenterPoints[i - 2];
                    this.CenterPoints.Add(
                        new Vector2(current.X + (1.4f * halfX), current.Y - halfY));
                }
                else if (i % 6 == 3 || i % 6 == 4 || i % 6 == 5 || i % 6 == 0)
                {
                    Vector2 current = this.CenterPoints[i - 3];
                    this.CenterPoints.Add(
                        new Vector2(current.X + (1.4f * xSize), current.Y));
                }
            }
        }
        private void InitTopLeftPoints()
        {
            float halfX = xSize / 2;
            float halfY = ySize / 2;

            foreach (Vector2 item in this.CenterPoints)
            {
                this.TopLeftPoints.Add(
                    new Vector2(item.X - halfX,
                        item.Y + 2));
            }
        }


        public void Draw(SpriteBatch spriteBatch, ContentManager content)
        {
            if (this.Showing)
            {
                int index = 0;
                foreach (Vector2 point in this.TopLeftPoints)
                {
                    if (this.IndexesToShow[index])
                    {
                        spriteBatch.Draw(content.Load<Texture2D>("Textures\\MiniMap\\minimapempty"), point + this.Shift, Color.Red);
                    }
                    else
                    {
                        spriteBatch.Draw(content.Load<Texture2D>("Textures\\MiniMap\\minimapempty"), point + this.Shift, Color.White);
                    }
                    index++;
                }
                //Draw Current room
                spriteBatch.Draw(content.Load<Texture2D>("Textures\\MiniMap\\minimapempty"), this.TopLeftPoints[this.CurrentRoom.RoomIndex] + this.Shift, Color.Blue);
                //Draw connections of the current room
                foreach (Room adjRoom in this.CurrentRoom.AdjacentRooms)
                {
                    spriteBatch.Draw(content.Load<Texture2D>("Textures\\MiniMap\\minimapempty"), this.TopLeftPoints[adjRoom.RoomIndex] + this.Shift, Color.Green);
                }
            }
        }

        public void Update()
        {
            if (this.MainGame.InputManager.IsClicked(Keys.M))
            {
                this.Showing = !this.Showing;
            }
            if (this.MainGame.LevelManager.CurrentLevel is GameOverLevel)
            {
                this.Showing = false;
            }
        }
    }
}
