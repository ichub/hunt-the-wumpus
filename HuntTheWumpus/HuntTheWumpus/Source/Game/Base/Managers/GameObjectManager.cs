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
    /// <summary>
    /// Class responsible for updating and drawing all game objects.
    /// </summary>
    public class GameObjectManager
    {
        private MainGame parentGame;
        private List<IGameObject> masterObjectList;
        private Stack<IGameObject> objectsToAdd;
        private Stack<IGameObject> objectsToRemove;

        /// <summary>
        /// Initializes all instance variables.
        /// </summary>
        public GameObjectManager(MainGame parentGame)
        {
            this.parentGame = parentGame;
            this.masterObjectList = new List<IGameObject>();
            this.objectsToAdd = new Stack<IGameObject>();
            this.objectsToRemove = new Stack<IGameObject>();
        }

        /// <summary>
        /// Checks and handles collisions with the border of the room
        /// </summary>
        public void HandleBounds()
        {
            foreach (ICollideable item in this.GetObjectsByType<ICollideable>())
            {
                if (!(item is Teleporter))
                {
                    List<Vector2> corners = this.CreateCorners(item.BoundingBox);
                    for (int j = 1; j < corners.Count; j++)
                    {
                        Vector2 first = corners[j - 1];
                        Vector2 second = corners[j];
                        for (int i = 1; i < Room.RoomBounds.Count; i++)
                        {
                            if (DoLinesIntersect(first, second, Room.RoomBounds[i - 1], Room.RoomBounds[i]))
                            {
                                if (item is PlayerAvatar)
                                {
                                    PlayerAvatar current = item as PlayerAvatar;
                                    current.CollideWithOppositeVector();
                                }
                            }

                        }
                    }
                }
            }
        }
        /// <summary>
        /// Checks if two lines interect
        /// </summary>
        /// <param name="first1">first point of first line</param>
        /// <param name="first2">second point of first line</param>
        /// <param name="second1">first point of second line</param>
        /// <param name="second2">second point of second line</param>
        /// <returns> true if intersect, other wise false</returns>
        private bool DoLinesIntersect(Vector2 first1, Vector2 first2, Vector2 second1, Vector2 second2)
        {
            float firstSlope = this.GetSlope(first1, first2);
            float secondSlope = this.GetSlope(second1, second2);

            float firstIntercept = this.GetIntercept(first1, firstSlope);
            float secondIntercept = this.GetIntercept(second1, secondSlope);

            float x = (secondIntercept - firstIntercept) / (firstSlope - secondSlope);
            float y = x * secondSlope + secondIntercept;

            return (x < Math.Max(first1.X, first2.X)
                && x > Math.Min(first1.X, first2.X)
                && x < Math.Max(second1.X, second2.X)
                && x > Math.Min(second1.X, second2.X));
        }
        /// <summary>
        /// Calculate the slope given 2 vectors.
        /// </summary>
        /// <param name="First Vector"></param>
        /// <param name="Second Vector"></param>
        /// <returns>Slope</returns>
        private float GetSlope(Vector2 first, Vector2 second)
        {
            return (second.Y - first.Y) / (second.X - first.X);
        }
        /// <summary>
        /// Get the y intercept of a line.
        /// </summary>
        /// <param name="The point of which the line passes through"></param>
        /// <param name="Slope of the line"></param>
        /// <returns>The y-intercept</returns>
        private float GetIntercept(Vector2 vect, float slope)
        {
            return (vect.Y) - (vect.X * slope);
        }
        /// <summary>
        /// Gets the corners of the bounding box.
        /// </summary>
        /// <param name="box"> Bounding Box of item</param>
        /// <returns>List of Vector2</returns>
        private List<Vector2> CreateCorners(BoundingBox box)
        {
            List<Vector2> vectors = new List<Vector2>(4);
            vectors.Add(new Vector2(box.Min.X, box.Min.Y));
            vectors.Add(new Vector2(box.Max.X, box.Min.Y));
            vectors.Add(new Vector2(box.Max.X, box.Max.Y));
            vectors.Add(new Vector2(box.Min.X, box.Max.Y));
            return vectors;
        }
        /// <summary>
        /// Gets all game objects of type T.
        /// </summary>
        /// <typeparam name="T"> Type of game object to return. </typeparam>
        /// <returns> IEnumerable of type T of all game objects of type T. </returns>
        public List<T> GetObjectsByType<T>() where T : class
        {
            List<T> resultList = new List<T>();
            foreach (object gameObject in this.masterObjectList)
            {
                if (gameObject is T)
                {
                    resultList.Add(gameObject as T);
                }
            }
            return resultList;
        }

        /// <summary>
        /// Adds the object to the game at the end of the frame.
        /// </summary>
        /// <param name="gameObject"> Object to add. </param>
        public void Add(IGameObject gameObject)
        {
            this.objectsToAdd.Push(gameObject);
        }

        /// <summary>
        /// Removes the object from the game at the end of the frame.
        /// </summary>
        /// <param name="gameObject"> Object to remove. </param>
        public void Remove(IGameObject gameObject)
        {
            this.objectsToRemove.Push(gameObject);
        }

        /// <summary>
        /// Method that adds and removes objects from the game at the end of the frame.
        /// </summary>
        public void AddAndRemoveObjects()
        {
            while (objectsToAdd.Any())
            {
                this.masterObjectList.Add(objectsToAdd.Pop());
            }

            while (objectsToRemove.Any())
            {
                this.masterObjectList.Remove(objectsToRemove.Pop());
            }
        }

        /// <summary>
        /// Loads content of every single game object, if it flags itsself for loading.
        /// </summary>
        private void LoadContent()
        {
            foreach (IDrawable obj in this.GetObjectsByType<IDrawable>())
            {
                if (!obj.ContentLoaded)
                {
                    obj.LoadContent(this.parentGame.Content);
                    obj.ContentLoaded = true;
                }
            }
        }

        /// <summary>
        /// Initializes every single object if it flags itsself for initializing.
        /// </summary>
        private void Initialize()
        {
            foreach (IInitializable obj in this.GetObjectsByType<IInitializable>())
            {
                if (!obj.Initialized)
                {
                    obj.Initialize();
                    obj.Initialized = true;
                }
            }
        }

        /// <summary>
        /// Updates every single object.
        /// </summary>
        private void Update()
        {
            foreach (IUpdateable obj in this.GetObjectsByType<IUpdateable>())
            {
                obj.Update(this.parentGame.GameTime);
            }
        }

        /// <summary>
        /// Handles clicking for every single clickable game object.
        /// </summary>
        private void HandleClicking()
        {
            foreach (IClickable obj in this.GetObjectsByType<IClickable>())
            {
                if (obj.ClickBox.Contains2D(this.parentGame.InputManager.MousePosition) &&
                    this.parentGame.InputManager.MouseState.LeftButton == ButtonState.Pressed)
                {
                    obj.OnClickBegin(this.parentGame.InputManager.MousePosition);
                    obj.IsClicked = true;
                }
                else if (obj.IsClicked == true)
                {
                    obj.OnClickRelease();
                    obj.IsClicked = false;
                }
            }
        }

        /// <summary>
        /// Checks whether two objects are collided.
        /// </summary>
        /// <param name="first"> The first object to check. </param>
        /// <param name="second"> The second object to check. </param>
        /// <returns> True if they are collided, false otherwise. </returns>
        private bool AreCollided(ICollideable first, ICollideable second)
        {
            if (first.BoundingBox != null && second.BoundingBox != null)
            {
                return first.BoundingBox.Intersects(second.BoundingBox);
            }
            return false;
        }

        /// <summary>
        /// Handles collisions between all collidable objects in the game.
        /// </summary>
        private void HandleCollisions()
        {
            var collidable = GetObjectsByType<ICollideable>();

            for (int i = 0; i < collidable.Count; i++)
            {
                for (int j = i + 1; j < collidable.Count; j++)
                {
                    collidable[i].CollideWith(collidable[j], AreCollided(collidable[i], collidable[j]));
                    collidable[j].CollideWith(collidable[i], AreCollided(collidable[i], collidable[j]));
                }
            }
        }

        /// <summary>
        /// Performs updates on every single gameobject. Call once per frame.
        /// </summary>
        public void FrameUpdate()
        {
            this.LoadContent();
            this.Initialize();
            this.HandleClicking();
            this.HandleCollisions();
            this.Update();
            this.AddAndRemoveObjects();
        }

        /// <summary>
        /// Draws every single object if its content is loaded.
        /// </summary>
        private void Draw()
        {
            foreach (IDrawable obj in this.GetObjectsByType<IDrawable>())
            {
                if (obj.ContentLoaded)
                {
                    obj.Draw(this.parentGame.GameTime, this.parentGame.SpriteBatch);
                }
            }
        }

        /// <summary>
        /// Draws every single object. Call once per frame.
        /// </summary>
        public void FrameDraw()
        {
            this.Draw();
            this.HandleBounds();
        }
    }
}
