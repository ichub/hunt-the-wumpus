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
    /// Class for handling input.
    /// </summary>
    public class InputManager
    {
        /// <summary>
        /// The state of the mouse at this frame.
        /// </summary>
        public MouseState MouseState { get; private set; }

        /// <summary>
        /// The state of the mouse during the last frame.
        /// </summary>
        public MouseState LastMouseState { get; private set; }

        /// <summary>
        /// The state of the keyboard during the frame.
        /// </summary>
        public KeyboardState KeyboardState { get; private set; }

        /// <summary>
        /// The state of the keyboard during the last frame.
        /// </summary>
        public KeyboardState LastKeyboardState { get; private set; }

        /// <summary>
        /// The mouse position at this frame.
        /// </summary>
        public Vector2 MousePosition { get; private set; }

        /// <summary>
        /// Event which is fired every time a key is pressed.
        /// </summary>
        public event KeyPressed KeyPressed;

        /// <summary>
        /// Updates all the instance variables to reflect the state of input
        /// devices during the current frame.
        /// </summary>
        public void Update()
        {
            this.LastMouseState = this.MouseState;
            this.MouseState = Mouse.GetState();

            this.LastKeyboardState = this.KeyboardState;
            this.KeyboardState = Keyboard.GetState();

            this.MousePosition = new Vector2(this.MouseState.X, this.MouseState.Y);

            // invokes a clicked key event if a button is clicked
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                if (this.IsClicked(key))
                {
                    if (this.KeyPressed != null)
                    {
                        this.KeyPressed.Invoke(key);
                    }
                }
            }
        }

        /// <summary>
        /// Checks whether or not the state of the given key changed
        /// from not pressed to pressed during this frame.
        /// </summary>
        /// <param name="key"> Key to check. </param>
        /// <returns> True if it was clicked, false otherwise. </returns>
        public bool IsClicked(Keys key)
        {
            return KeyboardState.IsKeyDown(key) && !LastKeyboardState.IsKeyDown(key);
        }

        /// <summary>
        /// Checks whether the state of the given mouse button changed
        /// from not pressed to pressed during this frame.
        /// </summary>
        /// <param name="button"> The mouse button to check. </param>
        /// <returns> True if it was clicked, false otherwise. </returns>
        public bool IsClicked(MouseButton button)
        {
            if (button == MouseButton.Left)
                return MouseState.LeftButton == ButtonState.Pressed && LastMouseState.LeftButton != ButtonState.Pressed;
            else if (button == MouseButton.Middle)
                return MouseState.MiddleButton == ButtonState.Pressed && LastMouseState.MiddleButton != ButtonState.Pressed;
            else if (button == MouseButton.Right)
                return MouseState.RightButton == ButtonState.Pressed && LastMouseState.RightButton != ButtonState.Pressed;
            else return false;
            
        }
    }

    /// <summary>
    /// Enum for chosing mouse buttons.
    /// </summary>
    public enum MouseButton
    {
        Left,
        Middle,
        Right,
    }

    /// <summary>
    /// Method signature for event handlers for the key pressed event.
    /// </summary>
    /// <param name="pressedKey"> The key which is pressed. </param>
    public delegate void KeyPressed(Keys pressedKey);
}
