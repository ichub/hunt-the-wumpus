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
    public class MiniMap
    {
        public List<Vector2> TopLeftPoints { get; private set; }
        public MainGame MainGame { get; set; }
        public readonly float xSize = 44;
        public readonly float ySize = 36.5f;

        private List<Vector2> CenterPoints;

        private bool[] IndexesToShow;
        private bool Showing = false;


        public MiniMap(MainGame parentGame)
        {
            this.MainGame = parentGame;
            this.CenterPoints = new List<Vector2>(30);
            this.TopLeftPoints = new List<Vector2>(30);
            this.IndexesToShow = new bool[30];
            this.InitCenterPoints();
            this.InitTopLeftPoints();
        }

        public MiniMap(int width, int height)
        {
            this.xSize = width;
            this.ySize = height;


            this.CenterPoints = new List<Vector2>(30);
            this.TopLeftPoints = new List<Vector2>(30);
            this.InitCenterPoints();
            this.InitTopLeftPoints();
        }
        /// <summary>
        /// Show Room In Cave
        /// </summary>
        /// <param name="index"></param>
        public void ShowRoom(int index)
        {
            this.IndexesToShow[index] = true;
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
                foreach (var point in this.TopLeftPoints)
                {
                    if (this.IndexesToShow[index])
                    {
                        spriteBatch.Draw(content.Load<Texture2D>("Textures\\MiniMap\\minimapempty"), point + new Vector2(200, 200), Color.Red);
                    }
                    else
                    {
                        spriteBatch.Draw(content.Load<Texture2D>("Textures\\MiniMap\\minimapempty"), point + new Vector2(200, 200), Color.White);
                    }
                    index++;
                }
            }
        }

        public void Update()
        {
            if (this.MainGame.InputManager.IsClicked(Keys.M))
            {
                this.Showing = !this.Showing;
            }
        }
    }
}
