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
    class NameMenu : BaseMenu
    {
        private string name;
        private const int maxNameLength = 10;

        public NameMenu(MainGame mainGame)
            : base(mainGame)
        {
            this.name = String.Empty;
        }

        public override void OnLoad()
        {
            this.GameObjects.Add(new Button(this.MainGame,
                this,
                () => OnButtonClick(),
                ButtonName.Start) { Position = new Vector2(512 - 100, 570) });

            this.MainGame.InputManager.KeyPressed += this.OnKeyPress;
        }

        public override void OnUnLoad()
        {
            this.MainGame.InputManager.KeyPressed -= this.OnKeyPress;
            this.MainGame.Player.Name = this.name;
            base.OnUnLoad();
        }

        private void OnButtonClick()
        {
            if (name.Length > 0)
            {
                this.MainGame.LevelManager.CurrentLevel = new InstructionsMenu(this.MainGame);
            }
        }

        private void OnKeyPress(Keys key)
        {
            if (this.IsCharacter(key))
            {
                if (this.name.Length < NameMenu.maxNameLength)
                {
                    this.name += key.ToString();
                }
            }
            if (key == Keys.Back)
            {
                if (this.name.Length > 0)
                {
                    this.name = this.name.Substring(0, this.name.Length - 1);
                }
            }
        }

        private bool IsCharacter(Keys key)
        {
            for (int i = 65; i < 91; i++)
            {
                if (Convert.ToChar(i).ToString().ToUpper() == key.ToString())
                {
                    return true;
                }
            }

            return false;
        }

        public override void FrameDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.FrameDraw(gameTime, spriteBatch);

            Vector2 blockEnd = this.MainGame.TextManager.DrawTextBlock(new Vector2(290, 140),
                String.Format("Enter your name! It can between 1 and {0} characters long, and contain only letters.", NameMenu.maxNameLength),
                400,
                Color.Black);

            this.MainGame.TextManager.DrawText(new Vector2(290, blockEnd.Y + 40), "Name: " + this.name, Color.Black);
        }
    }
}
