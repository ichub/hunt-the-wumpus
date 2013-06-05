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
using System.Reflection;

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
            using (StreamReader triviaFile = new StreamReader(this.GetPathToTrivia()))
            {
                while (!triviaFile.EndOfStream)
                {
                    Question newQuestion = new Question();
                    newQuestion.QuestionString = triviaFile.ReadLine();
                    for (int i = 0; i < 4; i++)
                    {
                        newQuestion.QuestionAnswers[i] = triviaFile.ReadLine();
                    }
                    newQuestion.CorrectAnswer = Convert.ToInt32(triviaFile.ReadLine());
                    triviaFile.ReadLine();
                    this.questions.Add(newQuestion);                    
                }
            }

            foreach (Question item in this.questions)
            {
                this.unaskedQuestions.Add(item);
            }
        }

        private string GetPathToTrivia()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Content\\Text\\trivia.txt";
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
        public string QuestionString { get; set; }
        public string[] QuestionAnswers { get; set; }
        public int CorrectAnswer { get; set; }

        public Question()
        {
            this.QuestionAnswers = new string[4];
        }

        public bool IsCorrect(int answer)
        {
            return answer == this.CorrectAnswer;
        }
    }
}