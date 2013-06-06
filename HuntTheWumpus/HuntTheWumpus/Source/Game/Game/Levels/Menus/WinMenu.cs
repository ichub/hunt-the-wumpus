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
    class WinMenu : BaseMenu
    {
        public WinMenu(MainGame mainGame)
            : base(mainGame) { }

        public override void Initialize()
        {
            base.Initialize();
            this.background = this.MainGame.Content.Load<Texture2D>("Textures\\youwinscreen");
        }

        public override void OnLoad()
        {
            this.GameObjects.Add(new Button(this.MainGame,
                this,
                () => this.MainGame.LevelManager.CurrentLevel = new StartMenu(this.MainGame),
                ButtonName.Menu) { Position = this.MainGame.ScreenDimensions / 2 - new Vector2(100, 50) });
        }
    }
}
