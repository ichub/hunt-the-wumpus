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
            //initilizes position of the buttons.
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

        /// <summary>
        /// Allows you to shoot an arrow 
        /// </summary>
        /// <param name="direction">The direction the arrow will go</param>
        public void ShootArrow(RoomDirection direction)
        {
            Room previousRoom = this.cameFrom as Room;
            switch (direction)
            {
                case RoomDirection.North:
                        this.ShootArrow(previousRoom.AdjacentRooms[0].RoomIndex);
                    break;
                case RoomDirection.NorthEast:
                        this.ShootArrow(previousRoom.AdjacentRooms[1].RoomIndex);
                    break;
                case RoomDirection.SouthEast:
                        this.ShootArrow(previousRoom.AdjacentRooms[2].RoomIndex);
                    break;
                case RoomDirection.South:
                        this.ShootArrow(previousRoom.AdjacentRooms[3].RoomIndex);
                    break;
                case RoomDirection.SouthWest:
                        this.ShootArrow(previousRoom.AdjacentRooms[4].RoomIndex);
                    break;
                case RoomDirection.NorthWest:
                        this.ShootArrow(previousRoom.AdjacentRooms[5].RoomIndex);
                    break;
            }
        }
        /// <summary>
        /// Allows you to shoot an arrow 
        /// </summary>
        /// <param name="RoomIndex">The index of the room where the arrow will go</param>
        public void ShootArrow(int RoomIndex)
        {
            bool atLeastOneArrow = false;
            if (this.MainGame.Player.AmountOfArrows > 0)
            {
                atLeastOneArrow = true;
                this.MainGame.Player.AmountOfArrows--;
            }
            if (this.MainGame.LevelManager.GameCave.RoomContainsWumpus(MainGame.LevelManager.GameCave.Wumpus.RoomIndex))
            {
                if (atLeastOneArrow)
                {
                    this.MainGame.LevelManager.GameCave.Wumpus.DoDamage();

                }
            }
        }

        /// <summary>
        /// Draws the current frame of the wumpus 
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
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
