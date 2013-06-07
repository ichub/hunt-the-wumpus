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
    public class Wumpus : Enemy
    {
        public int RoomIndex { get; set; }

        public const int WumpusStartHealth = 100;

        private AnimatedTexture facingLeft;
        private AnimatedTexture facingRight;
        private AnimatedTexture facingUp;
        private AnimatedTexture facingDown;

        public Wumpus(MainGame mainGame, ILevel parentLevel)
            : base(mainGame, parentLevel)
        {
            this.ObjectTeam = Team.Enemy;
            this.HP = Wumpus.WumpusStartHealth;
            this.Position = new Vector2(this.MainGame.WindowWidth / 2, this.MainGame.WindowHeight / 2);
            this.LastPosition = new Vector2(this.MainGame.WindowWidth / 2, this.MainGame.WindowHeight / 2);
        }

        /// <summary>
        /// Loads all the directional spritesheets.
        /// </summary>
        /// <param name="content"> The content manager with which to load the content. </param>
        public override void LoadContent(ContentManager content)
        {
            this.facingLeft = new AnimatedTexture(content.Load<Texture2D>("Textures\\Enemies\\Wumpus\\wumpus_leftspritesheet"), 4, 677, 5, 0.5f, Helper.EnemyLayer);
            this.facingRight = new AnimatedTexture(content.Load<Texture2D>("Textures\\Enemies\\Wumpus\\wumpus_rightspritesheet"), 4, 677, 5, 0.5f, Helper.EnemyLayer);
            this.facingUp = new AnimatedTexture(content.Load<Texture2D>("Textures\\Enemies\\Wumpus\\wumpus_backspritesheet"), 4, 677, 5, 0.5f, Helper.EnemyLayer);
            this.facingDown = new AnimatedTexture(content.Load<Texture2D>("Textures\\Enemies\\Wumpus\\wumpus_frontspritesheet"), 4, 677, 5, 0.5f, Helper.EnemyLayer);
            this.Texture = this.facingDown;
        }
        /// <summary>
        /// Provides the Texture
        /// </summary>
        private void ChoseTexture()
        {
            switch (Helper.GetDirection(this.Velocity))
            {
                case Direction.Up:
                    this.Texture = this.facingUp;
                    break;
                case Direction.Down:
                    this.Texture = this.facingDown;
                    break;
                case Direction.Left:
                    this.Texture = this.facingLeft;
                    break;
                case Direction.Right:
                    this.Texture = this.facingRight;
                    break;
            }
        }
        /// <summary>
        /// is called when Wumpus is damaged
        /// </summary>
        public override void OnDamage()
        {
            base.OnDamage();
            if (this.HP % 10 == 0)
            {
                this.Remove();
                this.MainGame.LevelManager.GameCave.MoveWumpus();
            }
        }
        /// <summary>
        /// Updates the wumpus
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        /// <summary>
        /// Does damage to the Wumpus
        /// </summary>
        public void DoDamage()
        {
            this.HP -= 10;
        }
    }
}
