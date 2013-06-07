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
    class InstructionsMenu : BaseMenu
    {
        public InstructionsMenu(MainGame mainGame)
            : base(mainGame)
        {
        }

        public override void OnLoad()
        {
            this.GameObjects.Add(new Button(this.MainGame,
                this,
                () => this.MainGame.LevelManager.CurrentLevel = this.MainGame.LevelManager.GameCave.PickRandomStartRoom(),
                ButtonName.Start) { Position = new Vector2(1024 / 2 - 100, 555) });
        }

        public override void FrameDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.FrameDraw(gameTime, spriteBatch);
            this.MainGame.TextManager.DrawText(new Vector2(450, 140), "Instructions:", Color.Black);
            this.MainGame.TextManager.DrawTextBlock(new Vector2(290, 180),
                "Press WASD to move around. Press the arrow keys to shoot. Press Q to go to trivia. Press U to go to arrow mode. Press M to open the map. Press H to to show the HUD.",
                450,
                Color.Black);

        }
    }
}
