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
    /// <summary>
    /// Class for extension methods and helper methods.
    /// </summary>
    public static class Helper
    {
        public const int FontSize = 14;

        /// <summary>
        /// The game to which this class belongs.
        /// </summary>
        private static MainGame mainGame;

        /// <summary>
        /// Initializes the extensions.
        /// </summary>
        /// <param name="mainGame"> The game to which this class belongs to. </param>
        public static void Init(MainGame game)
        {
            mainGame = game;
        }

        /// <summary>
        /// Tells whether or not the bounding box contains the 2D point alligned on the x-y plane.
        /// </summary>
        /// <param name="box"> Bounding box to check. </param>
        /// <param name="point"> Point to check. </param>
        /// <returns> True if it contains, false if it doesn't contain the point. </returns>
        public static bool Contains2D(this BoundingBox box, Vector2 point)
        {
            return box.Contains(new Vector3(point.X, point.Y, 0)) == ContainmentType.Contains;
        }

        /// <summary>
        /// Creates a new bounding box on the z-axis, basically a flat rectangle.
        /// </summary>
        /// <param name="box"> Any bounding box. </param>
        /// <param name="topLeft"> The top left corner of the box. </param>
        /// <param name="bottomRight"> The bottom right corner of the box. </param>
        /// <returns> A new bounding box with the specified dimensions. </returns>
        public static BoundingBox Set2D(this BoundingBox box, Vector2 topLeft, Vector2 bottomRight)
        {
            return new BoundingBox(new Vector3(topLeft, 0), new Vector3(bottomRight, 0));
        }

        /// <summary>
        /// Creates a new bounding box attached to the z-plane.
        /// </summary>
        /// <param name="topLeft"> The top left of the box. </param>
        /// <param name="bottomRight"> The bottom right of the box. </param>
        /// <returns> A new bounding box. </returns>
        public static BoundingBox Box2D(Vector2 topLeft, Vector2 bottomRight)
        {
            return new BoundingBox(new Vector3(topLeft, 0), new Vector3(bottomRight, 0));
        }

        /// <summary>
        /// Fills the given texture completely with the given color.
        /// </summary>
        /// <param name="texture"> Texture to fill. </param>
        /// <param name="color"> Color to fill it with. </param>
        public static void FillTexture(Texture2D texture, Color color)
        {
            Color[] colors = new Color[texture.Width * texture.Height];
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = color;
            }
            texture.SetData<Color>(colors);
        }

        /// <summary>
        /// Creates a random vector with the given length
        /// </summary>
        /// <param name="length"> The length of the vector. </param>
        /// <returns> A random vector. </returns>
        public static Vector2 RandomVector(float length)
        {
            double x = mainGame.Random.NextDouble() * 100 - 50;
            double y = mainGame.Random.NextDouble() * 100 - 50;

            Vector2 randomVector = new Vector2((float)x, (float)y);
            return randomVector / randomVector.Length() * length;
        }

        /// <summary>
        /// Creates a random vector with a randome length in the given range.
        /// </summary>
        /// <param name="minLength"> Minimum vector length. </param>
        /// <param name="maxLength"> Maximum vector length. </param>
        /// <returns></returns>
        public static Vector2 RandomVector(float minLength, float maxLength)
        {
            double randomLength = mainGame.Random.NextDouble() * (maxLength - minLength) + minLength;
            return RandomVector((float)randomLength);
        }

        /// <summary>
        /// Empty Vector
        /// </summary>
        /// <returns>Vector(0,0)</returns>
        public static Vector2 EmptyVector()
        {
            return new Vector2(0, 0);
        }

        /// <summary>
        /// Returns a boolean that is true a given percent of the time.
        /// </summary>
        /// <param name="probability"> The percentage amount that this returns true. </param>
        /// <returns> A random boolean. </returns>
        public static bool RandomBool(double probability)
        {
            return mainGame.Random.NextDouble() < probability;
        }

        /// <summary>
        /// Rounds the vector to integer coordinates.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Vector2 RoundVector(Vector2 vector)
        {
            return new Vector2((int)vector.X, (int)vector.Y);
        }

        /// <summary>
        /// Returns the direction that the given vector is most pointing to.
        /// </summary>
        /// <param name="vector"> The vector to check. </param>
        /// <returns> The direction. </returns>
        public static Direction GetDirection(Vector2 vector)
        {
            if (vector == Vector2.Zero)
                return Direction.Down;

            bool horizontalMove = Math.Abs(vector.X) > Math.Abs(vector.Y);

            if (horizontalMove)
            {
                if (vector.X < 0)
                    return Direction.Left;
                else
                    return Direction.Right;
            }
            else
            {
                if (vector.Y < 0)
                    return Direction.Up;
                else
                    return Direction.Down;
            }
        }

        /// <summary>
        /// Constructs a integer array with all the numbers set to some default number
        /// </summary>
        /// <param name="size">size of the array</param>
        /// <param name="number">the default number</param>
        /// <returns>an integer array with the specified default number</returns>
        public static int[] ChangeIndecesToNumber(int size, int number)
        {
            int[] array = new int[size];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = number;
            }
            return array;
        }

        /// <summary>
        /// Provides an Generic IndexOf for arrays
        /// </summary>
        /// <typeparam name="T">Any type</typeparam>
        /// <param name="array">The array of which to look into</param>
        /// <param name="toFind">What we are trying to find</param>
        /// <returns> -1 if not found or the index if found</returns>
        public static int IndexOf<T>(this T[] array, T toFind)
        {
            int index = 0;
            foreach (T item in array)
            {
                if (item.Equals(toFind))
                    return index;
                index++;
            }
            return -1;
        }

        /// <summary>
        /// Calculated the scale that needs to be applied to the main font
        /// </summary>
        /// <returns></returns>
        public static float CalculateScaleForDrawingText(int numberOfChar, int width)
        {
            return (float)width / (float)numberOfChar / Helper.FontSize;
        }
    }
}
