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
    class ArrowMenu : BaseMenu
    {
        public ArrowMenu(MainGame mainGame, ILevel cameFrom)
            : base(mainGame, cameFrom) { }

        public override void OnLoad()
        {
            base.OnLoad();

            this.GameObjects.Add(
                new Button(this.MainGame,
                    this,
                    () => ShootArrow(RoomDirection.North),
                    "1button") { Position = new Vector2(0, 0) });

            this.GameObjects.Add(
                new Button(this.MainGame,
                    this,
                    () => ShootArrow(RoomDirection.NorthEast),
                    "1button") { Position = new Vector2(0, 0) });

            this.GameObjects.Add(
                new Button(this.MainGame,
                    this,
                    () => ShootArrow(RoomDirection.SouthEast),
                    "1button") { Position = new Vector2(0, 0) });

            this.GameObjects.Add(
                new Button(this.MainGame,
                    this,
                    () => ShootArrow(RoomDirection.South),
                    "1button") { Position = new Vector2(0, 0) });

            this.GameObjects.Add(
                new Button(this.MainGame,
                    this,
                    () => ShootArrow(RoomDirection.SouthWest),
                    "1button") { Position = new Vector2(0, 0) });

            this.GameObjects.Add(
                new Button(this.MainGame,
                    this,
                    () => ShootArrow(RoomDirection.NorthWest),
                    "1button") { Position = new Vector2(0, 0) });
        }

        public void ShootArrow(RoomDirection direction)
        {
            // TODO: implement arrow shooting, need to check whether or not a wumpus is in the room;
        }
    }
}
