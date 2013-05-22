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
        /// Gets all game objects of type T.
        /// </summary>
        /// <typeparam name="T"> Type of game object to return. </typeparam>
        /// <returns> IEnumerable of type T of all game objects of type T. </returns>
        public List<T> GetObjectsByType<T>() where T : class
        {
            List<T> resultList = new List<T>();
            foreach (object gameObject in this.masterObjectList)
                if (gameObject is T)
                    resultList.Add(gameObject as T);
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
            if (first.BoundingBoxes != null && second.BoundingBoxes != null)
            for (int i = 0; i < first.BoundingBoxes.Count; i++)
            {
                for (int j = 0; j < second.BoundingBoxes.Count; j++)
                {
                    if (first.BoundingBoxes[i].Intersects(second.BoundingBoxes[j]))
                    {
                        return true;
                    }
                }
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
        }
    }
}
