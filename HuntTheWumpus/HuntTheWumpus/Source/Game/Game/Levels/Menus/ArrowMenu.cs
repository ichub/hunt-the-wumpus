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
                    ButtonName.North) { Position = new Vector2(0, 0) });

            this.GameObjects.Add(
                new Button(this.MainGame,
                    this,
                    () => ShootArrow(RoomDirection.NorthEast),
                    ButtonName.NorthEast) { Position = new Vector2(0, 0) });

            this.GameObjects.Add(
                new Button(this.MainGame,
                    this,
                    () => ShootArrow(RoomDirection.SouthEast),
                    ButtonName.SouthEast) { Position = new Vector2(0, 0) });

            this.GameObjects.Add(
                new Button(this.MainGame,
                    this,
                    () => ShootArrow(RoomDirection.South),
                    ButtonName.South) { Position = new Vector2(0, 0) });

            this.GameObjects.Add(
                new Button(this.MainGame,
                    this,
                    () => ShootArrow(RoomDirection.SouthWest),
                    ButtonName.SouthWest) { Position = new Vector2(0, 0) });

            this.GameObjects.Add(
                new Button(this.MainGame,
                    this,
                    () => ShootArrow(RoomDirection.NorthWest),
                    ButtonName.NorthWest) { Position = new Vector2(0, 0) });
        }

        public void ShootArrow(RoomDirection direction)
        {
            // TODO: implement arrow shooting, need to check whether or not a wumpus is in the room;
        }
    }
}
