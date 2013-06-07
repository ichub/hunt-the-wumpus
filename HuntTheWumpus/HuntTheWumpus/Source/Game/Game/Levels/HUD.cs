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
        public float Scale { get; set; }

        public MainGame ParentGame { get; set; }
        public SpriteFont Font { get; set; }
        public Color DrawTextColor { get; set; }

        public byte Transparency { get; set; }

        public Texture2D HudImage { get; private set; }

        public const string WumpusWarning = "I Smell a Wumpus!";
        public const string BatWarning = "Super Bats Nearby";
        public const string PitWarning = "I Feel a Draft";

        private Vector2 WumpusTextPosition { get; set; }
        private Vector2 BatTextPosition { get; set; }
        private Vector2 PitTextPosition { get; set; }

        private Vector2 LifeTextPosition { get; set; }

        private bool ShouldDraw = false;

        private Warnings Warning;

        private Vector2 EmptyVector;
        /// <summary>
        /// Constructor that demands the MainGame
        /// </summary>
        /// <param name="parent"></param>
        public HUD(MainGame parent)
        {
            this.ParentGame = parent;
            this.HudImage = this.ParentGame.Content.Load<Texture2D>("Textures\\HUDFULL");
            this.DrawTextColor = Color.Blue;
            this.Font = this.ParentGame.TextManager.Font;
            this.EmptyVector = Extensions.EmptyVector();
            this.Scale = 2;

            this.WumpusTextPosition = new Vector2(35, this.ParentGame.WindowHeight - 35);
            this.BatTextPosition = new Vector2(35, this.ParentGame.WindowHeight - 35);
            this.PitTextPosition = new Vector2(35, this.ParentGame.WindowHeight - 35);

            this.LifeTextPosition = new Vector2(this.ParentGame.WindowWidth - 70, this.ParentGame.WindowHeight - 110);
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
                    Color tint = new Color(Color.White.R, Color.White.G, Color.White.B, this.Transparency);
                    this.ParentGame.SpriteBatch.Draw(this.HudImage, new Vector2(0, this.ParentGame.WindowHeight - this.HudImage.Height), tint);

                    this.DrawWarning(this.Warning);
                    this.DrawLife(this.ParentGame.Player.HP);
                }
            }

        }

        private void DrawLife(int hp)
        {
            //Updates the class variable for the font.
            this.Font = this.ParentGame.TextManager.Font;
            this.ParentGame.SpriteBatch.DrawString(
                this.Font,
                hp.ToString(),
                this.LifeTextPosition,
                this.DrawTextColor,
                0,
                this.EmptyVector,
                this.Scale,
                SpriteEffects.None,
                0);
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
            this.Font = this.ParentGame.TextManager.Font;

            switch (warning)
            {
                case Warnings.Wumpus:
                    this.ParentGame.SpriteBatch.DrawString(this.Font, HUD.WumpusWarning, this.WumpusTextPosition, this.DrawTextColor);
                    return;
                case Warnings.Bat:
                    this.ParentGame.SpriteBatch.DrawString(this.Font, HUD.BatWarning, this.BatTextPosition, this.DrawTextColor);
                    return;
                case Warnings.Pit:
                    this.ParentGame.SpriteBatch.DrawString(this.Font, HUD.PitWarning, this.PitTextPosition, this.DrawTextColor);
                    return;
                case Warnings.None:
                    //No Warning
                    //So nothing happens
                    return;
            }
        }
    }
}
