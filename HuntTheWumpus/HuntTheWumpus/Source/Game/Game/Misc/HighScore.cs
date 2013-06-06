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
using System.Diagnostics;

namespace HuntTheWumpus.Source
{
    public struct SingleScore : IEquatable<SingleScore>, IComparable<SingleScore>
    {
        public readonly string Name;
        public readonly int Score;

        public SingleScore(string name, int score)
        {
            if (null == name)
            {
                throw new ArgumentNullException("name");
            }

            Debug.Assert(score >= 0);

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
            return this.Score.CompareTo(other.Score);
        }

        public override string ToString()
        {
            return (this.Name + ": " + this.Score);
        }

        //100 is the worst score possible
        public static SingleScore Empty = new SingleScore(string.Empty, 100);

        public bool Equals(SingleScore other)
        {
            return (this.Name == other.Name && this.Score == other.Score);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is SingleScore))
            {
                return false;
            }

            return this.Equals((SingleScore)obj);
        }

        public override int GetHashCode()
        {
            return (this.Name.GetHashCode() ^ this.Score.GetHashCode());
        }
    }

    public class HighScores
    {
        private readonly List<SingleScore> Scores = new List<SingleScore>();

        public HighScores()
        {
            //TEMP DEBUG. REMOVE

            for (int i = 0; i < 20; i++)
            {
                this.Add(new SingleScore("DERP", new Random().Next(100, 500)));
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
