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

        public Texture2D GoldImage { get; private set; }
        public Texture2D GemImage { get; private set; }
        public Texture2D SpeedRingImage { get; private set; }

        public Texture2D ArrowImage { get; private set; }

        public const string WumpusWarning = "I Smell a Wumpus!";
        public const string BatWarning = "Super Bats Nearby";
        public const string PitWarning = "I Feel a Draft";

        public const int WidthOfFirstWindow = 391;
        public const int WidthOfInventory = 552 - 424;

        private Vector2 WumpusTextPosition { get; set; }
        private Vector2 BatTextPosition { get; set; }
        private Vector2 PitTextPosition { get; set; }

        private Vector2 LifeTextPosition { get; set; }
        private Vector2 ScoreTextPosition { get; set; }

        private Vector2 BasicTextPosition { get; set; }

        private Vector2 ArrowTextPosition { get; set; }
        private bool ShouldDraw = false;

        private Warnings Warning;
        private string Message;
        private Vector2 EmptyVector;

        /// <summary>
        /// Constructor that demands the MainGame
        /// </summary>
        /// <param name="parent"></param>
        public HUD(MainGame parent)
        {
            this.ParentGame = parent;

            this.HudImage = this.ParentGame.Content.Load<Texture2D>("Textures\\HUDFULL");
            this.GoldImage = this.ParentGame.Content.Load<Texture2D>("Textures\\Items\\Gold");
            this.GemImage = this.ParentGame.Content.Load<Texture2D>("Textures\\Items\\Gem");
            this.SpeedRingImage = this.ParentGame.Content.Load<Texture2D>("Textures\\Items\\SpeedRing");
            this.ArrowImage = this.ParentGame.Content.Load<Texture2D>("Textures\\arrow");

            this.DrawTextColor = Color.Blue;
            this.Font = this.ParentGame.TextManager.Font;
            this.EmptyVector = Helper.EmptyVector;
            this.Scale = 2;
            this.Message = null;
            this.WumpusTextPosition = new Vector2(35, this.ParentGame.WindowHeight - 35);
            this.BatTextPosition = new Vector2(35, this.ParentGame.WindowHeight - 35);
            this.PitTextPosition = new Vector2(35, this.ParentGame.WindowHeight - 35);

            this.LifeTextPosition = new Vector2(this.ParentGame.WindowWidth - 80, this.ParentGame.WindowHeight - 110);
            this.ScoreTextPosition = new Vector2(this.ParentGame.WindowWidth - 80, this.ParentGame.WindowHeight - 55);
            this.BasicTextPosition = new Vector2(0, this.ParentGame.WindowHeight - 100);

            this.ArrowTextPosition = new Vector2(450, this.ParentGame.WindowHeight - 100);
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
                if ((this.ParentGame.LevelManager.CurrentLevel is Room
                    || this.ParentGame.LevelManager.CurrentLevel is TriviaMenu
                    || this.ParentGame.LevelManager.CurrentLevel is ArrowMenu)
                    && this.ShouldDraw)
                {
                    Color tint = new Color(Color.White.R, Color.White.G, Color.White.B, this.Transparency);
                    this.ParentGame.SpriteBatch.Draw(this.HudImage, new Vector2(0, this.ParentGame.WindowHeight - this.HudImage.Height), tint);

                    if (this.Message == null)
                        this.DrawWarning(this.Warning);
                    else
                        this.DrawWarning(this.Message);

                    this.DrawLife(this.ParentGame.Player.HP);
                    this.DrawScore(this.ParentGame.Player.Score);
                    this.DrawInventory(this.ParentGame.Player.Inventory);
                    this.DrawArrows(this.ParentGame.Player.AmountOfArrows);
                }
            }

        }
        /// <summary>
        /// Draws the arrows
        /// </summary>
        /// <param name="amountOfArrows">the amount of arrows</param>
        private void DrawArrows(int amountOfArrows)
        {
            this.ParentGame.SpriteBatch.Draw(
             this.ArrowImage,
             new Vector2(this.ArrowTextPosition.X - 10, this.ArrowTextPosition.Y + 50),
             null,
             Color.White,
             0,
             Helper.EmptyVector,
             1,
             SpriteEffects.None,
             0);

            //Updates the class variable for the font.
            this.Font = this.ParentGame.TextManager.Font;
            this.ParentGame.SpriteBatch.DrawString(
                this.Font,
                amountOfArrows.ToString(),
                this.ArrowTextPosition,
                this.DrawTextColor,
                0,
                this.EmptyVector,
                Helper.CalculateScaleForDrawingText(amountOfArrows.ToString().Length, 40),
                SpriteEffects.None,
                0);

        }
        /// <summary>
        /// Draws the relavent inventory into the HUD
        /// </summary>
        /// <param name="inventory">The inventory of the player</param>
        private void DrawInventory(Inventory inventory)
        {
            if (inventory.AmountOfGems() > 0)
            {
                for (int i = 0; i < 9; i++)
                {
                    this.ParentGame.SpriteBatch.Draw(
                       this.GemImage,
                       new Vector2(440 + HUD.WidthOfInventory, this.ParentGame.WindowHeight - 115 + i * 10),
                       null,
                       Color.White,
                       0,
                       Helper.EmptyVector,
                       0.25f,
                       SpriteEffects.None,
                       0);
                }
            }
            if (inventory.Contains("SpeedRing"))
            {
                for (int i = 0; i < 9; i++)
                {
                    this.ParentGame.SpriteBatch.Draw(
                       this.SpeedRingImage,
                       new Vector2(470 + HUD.WidthOfInventory * 2, this.ParentGame.WindowHeight - 115 + i * 10),
                       null,
                       Color.White,
                       0,
                       Helper.EmptyVector,
                       0.30f,
                       SpriteEffects.None,
                       0);
                }
            }
        }
        /// <summary>
        /// Draws the score on the HUD
        /// </summary>
        /// <param name="score">players score</param>
        private void DrawScore(int score)
        {
            //Updates the class variable for the font.
            this.Font = this.ParentGame.TextManager.Font;
            this.ParentGame.SpriteBatch.DrawString(
                this.Font,
                score.ToString(),
                this.ScoreTextPosition,
                this.DrawTextColor,
                0,
                this.EmptyVector,
                Helper.CalculateScaleForDrawingText(score.ToString().Length, 80),
                SpriteEffects.None,
                0);
        }
        /// <summary>
        /// Draws the hp or life of the player
        /// </summary>
        /// <param name="hp">life of the player</param>
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
            if (warning != Warnings.None)
            {
                this.Message = null;
            }
            this.Warning = warning;

        }
        /// <summary>
        /// Switches the warning into a string
        /// </summary>
        /// <param name="st">the warning</param>
        public void SwitchWarning(string st)
        {
            this.Message = st;
        }
        /// <summary>
        /// Generic Method to show any string
        /// </summary>
        /// <param name="toShow">string to show</param>
        public void DrawWarning(string toShow)
        {
            this.Font = this.ParentGame.TextManager.Font;
            this.ParentGame.SpriteBatch.DrawString(
                this.Font,
                toShow,
                this.PitTextPosition,
                this.DrawTextColor,
                0,
                this.EmptyVector,
                Helper.CalculateScaleForDrawingText(toShow.ToString().Length, HUD.WidthOfFirstWindow),
                SpriteEffects.None,
                0);
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
