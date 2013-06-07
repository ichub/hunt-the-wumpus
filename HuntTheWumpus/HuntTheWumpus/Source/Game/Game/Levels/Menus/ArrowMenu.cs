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
        private Vector2[] buttonPositions;
        private Vector2 buttonPositionShift;

        public ArrowMenu(MainGame mainGame, ILevel cameFrom)
            : base(mainGame, cameFrom)
        {
            this.buttonPositionShift = new Vector2(0, 200);
            this.buttonPositions = new Vector2[] 
            { 
                new Vector2(285, 140), 
                new Vector2(285 + 150 + 10, 140),
                new Vector2(285 + 300 + 20, 140),
                new Vector2(285, 280),
                new Vector2(285 + 150 + 10, 280),
                new Vector2(285 + 300 + 20, 280),
            };
        }

        public override void OnLoad()
        {
            base.OnLoad();

            this.GameObjects.Add(
                new Button(this.MainGame,
                    this,
                    () => ShootArrow(RoomDirection.North),
                    ButtonName.North) { Position = this.buttonPositions[0] + buttonPositionShift });

            this.GameObjects.Add(
                new Button(this.MainGame,
                    this,
                    () => ShootArrow(RoomDirection.NorthEast),
                    ButtonName.NorthEast) { Position = this.buttonPositions[1] + buttonPositionShift });

            this.GameObjects.Add(
                new Button(this.MainGame,
                    this,
                    () => ShootArrow(RoomDirection.SouthEast),
                    ButtonName.SouthEast) { Position = this.buttonPositions[2] + buttonPositionShift });

            this.GameObjects.Add(
                new Button(this.MainGame,
                    this,
                    () => ShootArrow(RoomDirection.South),
                    ButtonName.South) { Position = this.buttonPositions[3] + buttonPositionShift });

            this.GameObjects.Add(
                new Button(this.MainGame,
                    this,
                    () => ShootArrow(RoomDirection.SouthWest),
                    ButtonName.SouthWest) { Position = this.buttonPositions[4] + buttonPositionShift });

            this.GameObjects.Add(
                new Button(this.MainGame,
                    this,
                    () => ShootArrow(RoomDirection.NorthWest),
                    ButtonName.NorthWest) { Position = this.buttonPositions[5] + buttonPositionShift });
        }

        public void ShootArrow(RoomDirection direction)
        {
            Room previousRoom = this.cameFrom as Room;
            switch (direction)
            {
                case RoomDirection.North:
                    if (this.MainGame.LevelManager.GameCave.RoomContainsSuperBat(previousRoom.AdjacentRooms[0].RoomIndex))
                    {
                        this.KillWumpus();
                    }
                    break;
                case RoomDirection.NorthEast:
                    if (this.MainGame.LevelManager.GameCave.RoomContainsSuperBat(previousRoom.AdjacentRooms[1].RoomIndex))
                    {
                        this.KillWumpus();
                    }
                    break;
                case RoomDirection.SouthEast:
                    if (this.MainGame.LevelManager.GameCave.RoomContainsSuperBat(previousRoom.AdjacentRooms[2].RoomIndex))
                    {
                        this.KillWumpus();
                    }
                    break;
                case RoomDirection.South:
                    if (this.MainGame.LevelManager.GameCave.RoomContainsSuperBat(previousRoom.AdjacentRooms[3].RoomIndex))
                    {
                        this.KillWumpus();
                    }
                    break;
                case RoomDirection.SouthWest:
                    if (this.MainGame.LevelManager.GameCave.RoomContainsSuperBat(previousRoom.AdjacentRooms[4].RoomIndex))
                    {
                        this.KillWumpus();
                    }
                    break;
                case RoomDirection.NorthWest:
                    if (this.MainGame.LevelManager.GameCave.RoomContainsSuperBat(previousRoom.AdjacentRooms[5].RoomIndex))
                    {
                        this.KillWumpus();
                    }
                    break;
            }
        }

        public void KillWumpus()
        {
            //this.MainGame.LevelManager.GameCave.Wumpus.RoomIndex
        }

        public override void FrameDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.FrameDraw(gameTime, spriteBatch);
            this.MainGame.TextManager.DrawTextBlock(new Vector2(290, 140),
                "Shoot the wumpus with the arrow, and damage him by 10 points! If the wumpus reaches 0, you win!",
                400, 
                Color.Black);
        }
    }
}
