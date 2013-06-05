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
    public struct SingleScore : IComparable<SingleScore>
    {
        public readonly string Name;
        public readonly int Score;

        public SingleScore(string name, int score)
        {
            this.Name = name;
            this.Score = score;
        }

        /// <summary>
        /// Sorts by Ascending order
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(SingleScore other)
        {
            return (this.Score.CompareTo(other.Score));
        }
        public override string ToString()
        {
            return this.Name + ": " + this.Score;
        }
        //100 is the worst score possible
        public static SingleScore Empty = new SingleScore(string.Empty, 100);
    }

    public class HighScore
    {
        private List<SingleScore> Scores;
        private MainGame mainGame;

        public HighScore(MainGame mainGame)
        {
            this.Scores = new List<SingleScore>();
            this.mainGame = mainGame;

            //TEMP DEBUG

            for (int i = 0; i < 10; i++)
            {
                this.Add(new SingleScore("DERP",this.mainGame.Random.Next(100, 500)));
            }

            //TEMP DEBUG END
        }

        public void Add(SingleScore score)
        {
            this.Scores.Add(score);
            this.Scores.Sort();
        }
        public List<SingleScore> GetList()
        {
            return this.Scores;
        }
        public SingleScore GetHighScore()
        {
            if (this.Scores.Count <= 0)
                return SingleScore.Empty;

            return this.Scores[this.Scores.Count - 1];
        }
    }
}
