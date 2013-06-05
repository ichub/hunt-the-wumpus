using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace HuntTheWumpus.Source
{
    public class HUD
    {
        public MainGame ParentGame { get; set; }

        public Texture2D HudImage { get; private set; }

        private bool ShouldDraw = false;

        public HUD(MainGame parent)
        {
            this.ParentGame = parent;
            this.HudImage = this.ParentGame.Content.Load<Texture2D>("Textures\\HUDFULL");
        }
        public void Draw()
        {
            if (this.ParentGame.InputManager.IsClicked(Keys.H))
                this.ShouldDraw = !this.ShouldDraw;

            if (this.ParentGame.LevelManager.CurrentLevel != null)
            {
                if (!(this.ParentGame.LevelManager.CurrentLevel is GameOverLevel || this.ParentGame.LevelManager.CurrentLevel is StartLevel) && this.ShouldDraw)
                    this.ParentGame.SpriteBatch.Draw(this.HudImage, new Vector2(0, this.ParentGame.WindowHeight - this.HudImage.Height), Color.White);
            }
        }
    }
}
