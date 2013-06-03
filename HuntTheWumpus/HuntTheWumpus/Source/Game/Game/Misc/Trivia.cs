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
using System.IO;

namespace HuntTheWumpus.Source
{
    public class Trivia
    {
        private MainGame mainGame;
        private List<Question> questions;
        private List<Question> unaskedQuestions;
        private Stack<Question> askedQuestions;

        public Trivia(MainGame mainGame)
        {
            this.mainGame = mainGame;
            this.questions = new List<Question>();
            this.unaskedQuestions = new List<Question>();
            this.askedQuestions = new Stack<Question>();
            this.PopulateQuestions();
        }

        public void PopulateQuestions()
        {
            this.questions.Add(new Question("What is 2 * 2", 0));
            this.questions.Add(new Question("What is 3 * 3", 0));
            this.questions.Add(new Question("What is love", 0));
            this.questions.Add(new Question("baby don't hurt me", 0));
            this.questions.Add(new Question("don't hurt me", 0));
            this.questions.Add(new Question("no more", 0));
            this.questions.Add(new Question("What is 3 * 3", 0));
            this.questions.Add(new Question("What is 3 * 3", 0));

            foreach (Question item in this.questions)
            {
                this.unaskedQuestions.Add(item);
            }
        }

        public void Reset()
        {
            while (askedQuestions.Any())
            {
                this.unaskedQuestions.Add(this.askedQuestions.Pop());
            }
        }

        private void UpdateQuestionState(int index)
        {
            this.askedQuestions.Push(this.unaskedQuestions[index]);
            this.unaskedQuestions.RemoveAt(index);
        }

        public Question RandomQuestion()
        {
            if (this.unaskedQuestions.Count == 0)
            {
                return null;
            }
            int index = this.mainGame.Random.Next(this.unaskedQuestions.Count - 1);
            Question question = this.unaskedQuestions.ElementAt(index);
            this.UpdateQuestionState(index);
            return question;
        }
    }

    public class Question
    {
        public string QuestionString { get; private set; }
        public int Answer { get; private set; }

        public Question(string question, int answer)
        {
            this.QuestionString = question;
            this.Answer = answer;
        }

        public bool IsCorrect(int answer)
        {
            return answer == this.Answer;
        }
    }
}