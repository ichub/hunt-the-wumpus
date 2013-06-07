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
        public readonly int FromGold;
        public readonly int FromTrivia;
        public readonly int FromMisc;

        public SingleScore(string name, int score, int fromGold, int fromTrivia, int fromMisc)
        {
            this.Name = name;
            this.Score = score;
            this.FromGold = fromGold;
            this.FromTrivia = fromTrivia;
            this.FromMisc = fromMisc;
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
        public static SingleScore Empty = new SingleScore(string.Empty, 100, 0, 0, 100);

        /// <summary>
        /// Equates by name
        /// </summary>
        /// <param name="other"></param>
        /// <returns>true if yes,false if otherwise</returns>
        public bool Equals(SingleScore other)
        {
            return (this.Name == other.Name
                && this.Score == other.Score);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is SingleScore))
            {
                return false;
            }

            return this.Equals((SingleScore)obj);
        }

        /// <summary>
        /// Hashes by name and score
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return (this.Name.GetHashCode() ^ this.Score.GetHashCode());
        }
    }

    public class HighScores
    {
        private readonly List<SingleScore> Scores = new List<SingleScore>();

        /// <summary>
        /// Adds SingleScore to innerlist.
        /// </summary>
        /// <param name="score"></param>
        public void Add(SingleScore score)
        {
            this.Scores.Add(score);
            this.Scores.Sort();
        }
        /// <summary>
        /// Gets the innerlist
        /// </summary>
        /// <returns></returns>
        public List<SingleScore> GetList()
        {
            return this.Scores;
        }
        /// <summary>
        /// Gets the highest score
        /// </summary>
        /// <returns></returns>
        public SingleScore GetHighScore()
        {
            if (this.Scores.Count <= 0)
                return SingleScore.Empty;

            return this.Scores[this.Scores.Count - 1];
        }
    }
}
