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
    /// <summary>
    /// A button.
    /// </summary>
    class Button : IClickable, IUpdateable, IDrawable, IGameObject, IHoverable
    {
        public static readonly Vector2 DefaultDimensions = new Vector2(1024, 768);

        public MainGame MainGame { get; set; }
        public ILevel ParentLevel { get; set; }
        public BoundingBox BoundingBox { get; set; }
        public AnimatedTexture Texture { get; set; }
        public Vector2 Position { get; set; }
        public Team ObjectTeam { get; set; }

        public bool IsClicked { get; set; }
        public bool ContentLoaded { get; set; }
        public bool IsHidden { get; set; }
        public bool IsMouseOver { get; set; }

        public Action OnClick { get; set; }

        private AnimatedTexture clickedTexture;
        private AnimatedTexture notClickedTexture;
        private AnimatedTexture mousedOverTexture;

        private ButtonName buttonName;

        private static Dictionary<ButtonName, string> buttonResourceNames;

        static Button()
        {
            buttonResourceNames = new Dictionary<ButtonName, string>();
            buttonResourceNames.Add(ButtonName.ChoiceOne, "1button");
            buttonResourceNames.Add(ButtonName.ChoiceTwo, "2button");
            buttonResourceNames.Add(ButtonName.ChoiceThree, "3button");
            buttonResourceNames.Add(ButtonName.ChoiceFour, "4button");
            buttonResourceNames.Add(ButtonName.HighScore, "highscore");
            buttonResourceNames.Add(ButtonName.Menu, "menubutton");
            buttonResourceNames.Add(ButtonName.North, "n");
            buttonResourceNames.Add(ButtonName.NorthEast, "ne");
            buttonResourceNames.Add(ButtonName.NorthWest, "nw");
            buttonResourceNames.Add(ButtonName.Quit, "quit");
            buttonResourceNames.Add(ButtonName.South, "s");
            buttonResourceNames.Add(ButtonName.SouthEast, "se");
            buttonResourceNames.Add(ButtonName.SouthWest, "sw");
            buttonResourceNames.Add(ButtonName.Start, "startbutton");
        }

        /// <summary>
        /// Creates a new button.
        /// </summary>
        /// <param name="mainGame"> The game to which this button belongs. </param>
        /// <param name="parentLevel"> The level to which this button belongs. </param>
        /// <param name="onClick"> The action to take when this button is clicked on. </param>
        public Button(MainGame mainGame, ILevel parentLevel, Action onClick, ButtonName name)
        {
            this.MainGame = mainGame;
            this.ParentLevel = parentLevel;
            this.ObjectTeam = Team.Neutral;
            this.OnClick = onClick;
            this.buttonName = name;
        }

        private static string ButtonPathFromName(ButtonName name, GameButtonState state)
        {
            switch (state)
            {
                case GameButtonState.Hovored:
                    return "Textures\\Buttons\\" + Button.buttonResourceNames[name] + "_mouseover";
                case GameButtonState.Default:
                    return "Textures\\Buttons\\" + Button.buttonResourceNames[name] + "_default";
                case GameButtonState.Clicked:
                    return "Textures\\Buttons\\" + Button.buttonResourceNames[name] + "_mouseclicked";
            }
            return null;
        }

        public void LoadContent(ContentManager content)
        {
            this.notClickedTexture = new AnimatedTexture(content.Load<Texture2D>(Button.ButtonPathFromName(this.buttonName, GameButtonState.Default)));
            this.clickedTexture = new AnimatedTexture(content.Load<Texture2D>(Button.ButtonPathFromName(this.buttonName, GameButtonState.Clicked)));
            this.mousedOverTexture = new AnimatedTexture(content.Load<Texture2D>(Button.ButtonPathFromName(this.buttonName, GameButtonState.Hovored)));
            this.Texture = this.notClickedTexture;
        }

        public void CenterOnScreen()
        {
            this.Position = Button.DefaultDimensions - this.Texture.Size;
            this.Position /= 2;
            this.Position = Extensions.RoundVector(this.Position);
        }

        public void Update(GameTime gameTime)
        {
            this.BoundingBox = this.BoundingBox.Set2D(this.Position, this.Position + this.Texture.Size);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            this.Texture.Draw(spriteBatch, this.Position, gameTime);
        }

        public void OnClickBegin(Vector2 clickPosition)
        {
            this.Texture = this.clickedTexture;
        }

        public void OnClickRelease()
        {
            this.OnClick.Invoke();

            this.Texture = this.notClickedTexture;

            this.MainGame.SoundManager.PlaySound(Sound.MenuChange);
        }

        public void OnHoverBegin()
        {
            this.Texture = this.mousedOverTexture;
        }

        public void OnHoverEnd()
        {
            this.Texture = this.notClickedTexture;
        }
    }

    public enum ButtonName
    {
        ChoiceOne,
        ChoiceTwo,
        ChoiceThree,
        ChoiceFour,
        HighScore,
        Start,
        Menu,
        Quit,
        North,
        NorthEast,
        NorthWest,
        South,
        SouthEast,
        SouthWest,
    }

    public enum GameButtonState
    {
        Hovored,
        Default,
        Clicked,
    }
}
