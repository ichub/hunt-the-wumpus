using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpus.Source.Game.Game.Misc
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

        public HighScore()
        {
            this.Scores = new List<SingleScore>();
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
