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
    public class InputManager
    {
        public MouseState MouseState { get; private set; }
        public MouseState LastMouseState { get; private set; }
        public KeyboardState KeyboardState { get; private set; }
        public KeyboardState LastKeyboardState { get; private set; }
        public Vector2 MousePosition { get; private set; }

        public void Update()
        {
            this.LastMouseState = this.MouseState;
            this.MouseState = Mouse.GetState();

            this.LastKeyboardState = this.KeyboardState;
            this.KeyboardState = Keyboard.GetState();

            this.MousePosition = new Vector2(this.MouseState.X, this.MouseState.Y);
        }

        public bool IsClicked(Keys key)
        {
            return KeyboardState.IsKeyDown(key) && !LastKeyboardState.IsKeyDown(key);
        }

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

    public enum MouseButton
    {
        Left,
        Middle,
        Right,
    }
}
