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
    /// Class for extension methods.
    /// </summary>
    public static class Extensions
    {
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

        public static BoundingBox Box2D(Vector2 topLeft, Vector2 bottomRight)
        {
            return new BoundingBox(new Vector3(topLeft, 0), new Vector3(bottomRight, 0));
        }

        public static void FillTexture(Texture2D texture, Color color)
        {
            Color[] colors = new Color[texture.Width * texture.Height];
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = color;
            }
            texture.SetData<Color>(colors);
        }
    }
}
