using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace HuntTheWumpus.Source
{
    public enum Warnings
    {
        Wumpus,
        Bat,
        Pit,
        None
    }
    public class HUD
    {
        public MainGame ParentGame { get; set; }
        public SpriteFont SpriteFont { get; set; }
        public Color DrawTextColor { get; set; }

        public Texture2D HudImage { get; private set; }

        public const string WumpusWarning = "I Smell a Wumpus!";
        public const string BatWarning = "Bats Nearby";
        public const string PitWarning = "I feel a draft";


        private readonly Vector2 WumpusTextPosition;
        private readonly Vector2 BatTextPosition;
        private readonly Vector2 PitTextPosition;

        private bool ShouldDraw = false;

        private Warnings Warning;

        public HUD(MainGame parent)
        {
            this.ParentGame = parent;
            this.HudImage = this.ParentGame.Content.Load<Texture2D>("Textures\\HUDFULL");
            this.DrawTextColor = Color.Blue;

            this.WumpusTextPosition = new Vector2(35, this.ParentGame.WindowHeight - 35);
            this.BatTextPosition = new Vector2(35, this.ParentGame.WindowHeight - 35);
            this.PitTextPosition = new Vector2(35, this.ParentGame.WindowHeight - 35);
        }
        public void DrawHud()
        {
            if (this.ParentGame.InputManager.IsClicked(Keys.H))
                this.ShouldDraw = !this.ShouldDraw;

            if (this.ParentGame.LevelManager.CurrentLevel != null)
            {
                if (!(this.ParentGame.LevelManager.CurrentLevel is GameOverLevel || this.ParentGame.LevelManager.CurrentLevel is StartLevel) && this.ShouldDraw)
                {
                    this.ParentGame.SpriteBatch.Draw(this.HudImage, new Vector2(0, this.ParentGame.WindowHeight - this.HudImage.Height), Color.White);
                    this.DrawWarning(this.Warning);
                }
            }

        }
        public void SwitchWarning(Warnings warning)
        {
            this.Warning = warning;
        }
        private void DrawWarning(Warnings warning)
        {
            this.SpriteFont = this.ParentGame.TextManager.CourierNew;
            switch (warning)
            {
                case Warnings.Wumpus:
                    this.ParentGame.SpriteBatch.DrawString(this.SpriteFont, HUD.WumpusWarning, this.WumpusTextPosition, this.DrawTextColor);
                    break;
                case Warnings.Bat:
                    this.ParentGame.SpriteBatch.DrawString(this.SpriteFont, HUD.BatWarning, this.BatTextPosition, this.DrawTextColor);
                    break;
                case Warnings.Pit:
                    this.ParentGame.SpriteBatch.DrawString(this.SpriteFont, HUD.PitWarning, this.PitTextPosition, this.DrawTextColor);
                    break;
            }
        }
    }
}
