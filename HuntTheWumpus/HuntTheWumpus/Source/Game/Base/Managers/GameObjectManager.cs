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
        private List<object> masterObjectList;

        /// <summary>
        /// Initializes all instance variables.
        /// </summary>
        public GameObjectManager(MainGame parentGame)
        {
            this.parentGame = parentGame;
            this.masterObjectList = new List<object>();
        }

        /// <summary>
        /// Gets all game objects of type T.
        /// </summary>
        /// <typeparam name="T"> Type of game object to return. </typeparam>
        /// <returns> IEnumerable of type T of all game objects of type T. </returns>
        public IEnumerable<object> GetObjectsByType<T>() where T : class
        {
            return from obj 
                   in masterObjectList 
                   where obj is T 
                   select obj;
        }

        /// <summary>
        /// Adds the specified object to each corresponding list. Validity
        /// is determined by which interfaces the object implements.
        /// </summary>
        /// <param name="gameObject"> Object to add. </param>
        public void Add(object gameObject)
        {
            this.masterObjectList.Add(gameObject);
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
        /// Handles dragging for every single draggable game object.
        /// </summary>
        private void HandleDragging()
        {
                        
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
        /// Performs updates on every single gameobject. Call once per frame.
        /// </summary>
        public void FrameUpdate()
        {
            this.LoadContent();
            this.Initialize();
            this.HandleDragging();
            this.HandleClicking();
            this.Update();
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
