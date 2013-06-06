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
    /// <summary>
    /// A class the represents the Hud at the bottom of the screen
    /// </summary>
    public class HUD
    {

        public MainGame ParentGame { get; set; }
        public SpriteFont SpriteFont { get; set; }
        public Color DrawTextColor { get; set; }

        public byte Transparency { get; set; }

        public Texture2D HudImage { get; private set; }

        public const string WumpusWarning = "I Smell a Wumpus!";
        public const string BatWarning = "Bats Nearby";
        public const string PitWarning = "I feel a draft";

        private readonly Vector2 WumpusTextPosition;
        private readonly Vector2 BatTextPosition;
        private readonly Vector2 PitTextPosition;

        private bool ShouldDraw = false;

        private Warnings Warning;

        /// <summary>
        /// Constructor that demands the MainGame
        /// </summary>
        /// <param name="parent"></param>
        public HUD(MainGame parent)
        {
            this.ParentGame = parent;
            this.HudImage = this.ParentGame.Content.Load<Texture2D>("Textures\\HUDFULL");
            this.DrawTextColor = Color.Blue;

            this.WumpusTextPosition = new Vector2(35, this.ParentGame.WindowHeight - 35);
            this.BatTextPosition = new Vector2(35, this.ParentGame.WindowHeight - 35);
            this.PitTextPosition = new Vector2(35, this.ParentGame.WindowHeight - 35);

            this.Transparency = 255 / 2;
        }
        /// <summary>
        /// Draws the complete HUD
        /// Including the messages
        /// </summary>
        public void DrawHud()
        {
            if (this.ParentGame.InputManager.IsClicked(Keys.H))
                this.ShouldDraw = !this.ShouldDraw;

            if (this.ParentGame.LevelManager.CurrentLevel != null)
            {
                if (!(this.ParentGame.LevelManager.CurrentLevel is GameOverMenu || this.ParentGame.LevelManager.CurrentLevel is StartMenu) && this.ShouldDraw)
                {
                    Color tint = new Color(Color.White.R, Color.White.R, Color.White.R, this.Transparency);
                    this.ParentGame.SpriteBatch.Draw(this.HudImage, new Vector2(0, this.ParentGame.WindowHeight - this.HudImage.Height), tint);
                    this.DrawWarning(this.Warning);
                }
            }

        }
        /// <summary>
        /// Swith the current type of Warning
        /// </summary>
        /// <param name="warning">Type of warning</param>
        public void SwitchWarning(Warnings warning)
        {
            this.Warning = warning;
        }

        /// <summary>
        /// Draws the specified Warning
        /// </summary>
        /// <param name="warning">Type of warning</param>
        private void DrawWarning(Warnings warning)
        {
            //Updates the class variable for the font.
            this.SpriteFont = this.ParentGame.TextManager.Font;

            switch (warning)
            {
                case Warnings.Wumpus:
                    this.ParentGame.SpriteBatch.DrawString(this.SpriteFont, HUD.WumpusWarning, this.WumpusTextPosition, this.DrawTextColor);
                    return;
                case Warnings.Bat:
                    this.ParentGame.SpriteBatch.DrawString(this.SpriteFont, HUD.BatWarning, this.BatTextPosition, this.DrawTextColor);
                    return;
                case Warnings.Pit:
                    this.ParentGame.SpriteBatch.DrawString(this.SpriteFont, HUD.PitWarning, this.PitTextPosition, this.DrawTextColor);
                    return;
                case Warnings.None:
                    return;
            }
        }
    }
}
