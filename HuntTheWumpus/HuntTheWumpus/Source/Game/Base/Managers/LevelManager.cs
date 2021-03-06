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
using System.Timers;
using HuntTheWumpus.Source.Game.Derived.Wumpus_Objects;

namespace HuntTheWumpus.Source
{
    /// <summary>
    /// Class responsible for updating, drawing, and changing levels.
    /// </summary>
    public class LevelManager
    {
        /// <summary>
        /// The game to which this level manager belongs.
        /// </summary>
        public MainGame MainGame { get; private set; }

        /// <summary>
        /// The hud, which is used to display stats, as well as health and events.
        /// </summary>
        public HUD Hud { get; private set; }

        /// <summary>
        /// A black texture which covers the whole screen. Used to fade in 
        /// and out of levels.
        /// </summary>
        private Texture2D levelFade;

        /// <summary>
        /// A value changed every frame if a level fade is in progress. Used to 
        /// determine the alpha value of the levelFade texture so that it fades
        /// out smoothly.
        /// </summary>
        private float fadeCount;

        /// <summary>
        /// The amount fadeCount is changed by ever time interval.
        /// </summary>
        private int fadeSpeed;

        /// <summary>
        /// Used to determine if a fade is in progress at the current frame.
        /// </summary>
        private bool isLevelChanging;

        /// <summary>
        /// A timer which is called every time interval to fade in or out.
        /// </summary>
        private Timer fadeTimer;

        /// <summary>
        /// The game cave used for generating and storing rooms.
        /// </summary>
        public Cave GameCave { get; set; }

        /// <summary>
        /// The current level.
        /// </summary>
        private ILevel currentLevel;

        /// <summary>
        /// A queue for holding levels which are to be changed to 
        /// after the changing level is changed.
        /// </summary>
        private Queue<ILevel> nextLevels;

        /// <summary>
        /// The level to load when the current level is completely faded out.
        /// </summary>
        private ILevel nextLevel;

        /// <summary>
        /// Amount of miliseconds that pass until the fade value is updated.
        /// </summary>
        private const int fadeInterval = 30;

        /// <summary>
        /// Gets or sets the current level, if the level is not changing at the
        /// current frame. Starts the fading process if the level is not changing.
        /// </summary>
        public ILevel CurrentLevel
        {
            get
            {
                return this.currentLevel;
            }
            set
            {
                // if it's not currently in the process of fading in or out of a level.
                if (!this.isLevelChanging)
                {
                    // if there aren't any levels in the queue
                    if (this.nextLevels.Count == 0)
                    {
                        // if the there is a current level.
                        if (this.currentLevel != null)
                        {
                            //Makes sure the specific level arent repeating
                            if (this.currentLevel is GameOverMenu && value is GameOverMenu)
                                return;
                            if (this.currentLevel is HighScoreMenu && value is HighScoreMenu)
                                return;
                            if (this.currentLevel is Room && value is Room && (this.currentLevel as Room).RoomIndex == (value as Room).RoomIndex)
                                return;
                            if (this.currentLevel is TriviaMenu && value is TriviaMenu)
                                return;
                            // starts the faiding process
                            this.StartFade(value);
                            this.isLevelChanging = true;
                        }
                        // there is not a current level
                        else
                        {
                            this.currentLevel = value;
                            this.currentLevel.OnLoad();
                        }
                    }
                    // there is a level in the queue.
                    else
                    {
                        this.DequeueLevels();
                    }
                }
                // the level is not currently changing
                else
                {
                    // if there is not a level in the queue
                    if (this.nextLevels.Count == 0)
                    {
                        this.nextLevels.Enqueue(value);
                    }
                    else
                    {
                        // if the level was not previously added
                        if (!this.nextLevels.Contains(value))
                        {
                            this.nextLevels.Enqueue(value);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Creates a new level manager.
        /// </summary>
        /// <param name="parentGame"> Game to which this level manager belongs. </param>
        public LevelManager(MainGame parentGame)
        {
            this.MainGame = parentGame;
            this.Hud = new HUD(this.MainGame);

            this.fadeSpeed = 20;
            this.nextLevels = new Queue<ILevel>();

            // initializes the big black fade box.
            this.levelFade = new Texture2D(parentGame.GraphicsDevice, parentGame.WindowWidth, parentGame.WindowHeight);
            Helper.FillTexture(levelFade, Color.Black);

            // initializes the game cave.
            this.GameCave = new Cave(this.MainGame);
        }

        /// <summary>
        /// Updates the current level.
        /// </summary>
        public void FrameUpdate()
        {
            // if there is a level
            if (this.CurrentLevel != null)
            {
                // if the level is not initialized, initialize it.
                if (!this.CurrentLevel.Initialized)
                {
                    this.CurrentLevel.Initialize();
                    this.CurrentLevel.Initialized = true;
                }

                // updates everything.
                this.CurrentLevel.FrameUpdate(this.MainGame.GameTime, this.MainGame.Content);
                this.GameCave.UpdateSuperBats();
            }
            // dequeues the next level if the level state is not changing.
            this.DequeueLevels();
        }

        /// <summary>
        /// Dequeues level so that the next level is set.
        /// </summary>
        private void DequeueLevels()
        {
            if (!this.isLevelChanging)
            {
                if (this.nextLevels.Count > 0)
                {
                    this.CurrentLevel = this.nextLevels.Dequeue();
                }
            }
        }

        /// <summary>
        /// Starts fading from one level to the given level.
        /// </summary>
        /// <param name="toFadeInto"> Level to fade into. </param>
        public void StartFade(ILevel toFadeInto)
        {
            // stops the previous timer.
            if (this.fadeTimer != null)
            {
                this.fadeTimer.Stop();
                this.fadeCount = 0;
            }

            // starts the timer
            this.fadeTimer = new Timer(LevelManager.fadeInterval);
            this.fadeTimer.Elapsed += this.FadeOut;
            this.fadeTimer.Start();
            this.nextLevel = toFadeInto;
        }

        /// <summary>
        /// Called when the current level has been faded out of. Changes the current
        /// level and starts fading into it.
        /// </summary>
        public void OnFadeOutEnd()
        {
            this.currentLevel.OnUnLoad();
            this.currentLevel = this.nextLevel;
            this.currentLevel.OnLoad();

            this.fadeTimer.Stop();
            this.fadeTimer = new Timer(LevelManager.fadeInterval);
            this.fadeTimer.Elapsed += this.FadeIn;
            this.fadeTimer.Start();
            this.nextLevel = null;
        }

        /// <summary>
        /// Called when the current level has been completely faded in.
        /// </summary>
        public void OnFadeInEnd()
        {
            this.fadeTimer.Stop();
            this.fadeTimer = null;
            this.isLevelChanging = false;
        }

        /// <summary>
        /// Method that fades into the current level.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FadeIn(object sender, ElapsedEventArgs e)
        {
            this.fadeCount -= this.fadeSpeed;
            if (this.fadeCount < 0)
            {
                this.fadeCount = 0;
                this.OnFadeInEnd();
            }
        }

        /// <summary>
        /// Method that fades out of the current level.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FadeOut(object sender, ElapsedEventArgs e)
        {
            this.fadeCount += this.fadeSpeed;
            if (this.fadeCount > 255)
            {
                this.fadeCount = 255;
                this.OnFadeOutEnd();
            }
        }

        /// <summary>
        /// Draws the current level.
        /// </summary>
        public void FrameDraw()
        {
            if (this.CurrentLevel != null)
            {
                this.CurrentLevel.FrameDraw(this.MainGame.GameTime, this.MainGame.SpriteBatch);
                this.HandlePit();
                this.HandleAnyWarnings();
                this.Hud.DrawHud();
            }

            this.MainGame.SpriteBatch.Draw(this.levelFade, Vector2.Zero, new Color(255, 255, 255, (int)this.fadeCount));
        }

        /// <summary>
        /// Handles the warnings: such as 
        /// The Wumpus
        /// The Pit
        /// The SuperBats
        /// </summary>
        public void HandleAnyWarnings()
        {
            Room currentRoom = this.currentLevel as Room;
            if (null == currentRoom)
                return;

            foreach (Room item in currentRoom.ConnectedRooms.Where((x) => x != null))
            {
                if (this.GameCave.Wumpus.RoomIndex == item.RoomIndex)
                {
                    this.Hud.SwitchWarning(Warnings.Wumpus);
                    return;
                }
                if (item.RoomType == RoomType.Pit)
                {
                    this.Hud.SwitchWarning(Warnings.Pit);
                    return;
                }
                foreach (SuperBat bat in this.GameCave.SuperBats)
                {
                    if (item.RoomIndex == bat.ParentRoomIndex)
                    {
                        this.Hud.SwitchWarning(Warnings.Bat);
                        return;
                    }
                }
            }
            //No Warnings
            this.Hud.SwitchWarning(Warnings.None);
        }

        /// <summary>
        /// Handles anything that has to do with the pit
        /// </summary>
        public void HandlePit()
        {
            Room curRo = this.CurrentLevel as Room;
            if (curRo != null && curRo.RoomType == RoomType.Pit)
            {
                this.CurrentLevel = new TriviaMenu(this.MainGame, this.currentLevel);
            }
        }

        /// <summary>
        /// Resets the game, reseting the player score, etc.
        /// </summary>
        public void Reset()
        {
            SingleScore score = new SingleScore(this.MainGame.Player.Name, 
                this.MainGame.Player.Score, 
                this.MainGame.Player.FromGold, 
                this.MainGame.Player.FromTrivia, 
                this.MainGame.Player.FromMisc);

            this.MainGame.HighScore.Add(score);

            this.MainGame.Player.Reset();
            this.MainGame.MiniMap.Reset();

            this.MainGame.LevelManager.GameCave.Reset();
        }
    }
}
