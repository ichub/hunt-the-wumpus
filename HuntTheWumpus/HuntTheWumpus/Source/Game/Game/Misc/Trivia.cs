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

        /// <summary>
        /// Reads and populates the question 
        /// from a specified text.
        /// </summary>
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

        /// <summary>
        /// Gets the path fromtrivia
        /// </summary>
        /// <returns>path to trivia</returns>
        private string GetPathToTrivia()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Content\\Text\\trivia.txt";
        }

        /// <summary>
        /// resets the Trivia class
        /// </summary>
        public void Reset()
        {
            while (askedQuestions.Any())
            {
                this.unaskedQuestions.Add(this.askedQuestions.Pop());
            }
        }

        /// <summary>
        /// Updates the state of the question
        /// </summary>
        /// <param name="index"></param>
        private void UpdateQuestionState(int index)
        {
            this.askedQuestions.Push(this.unaskedQuestions[index]);
            this.unaskedQuestions.RemoveAt(index);
        }

        /// <summary>
        /// Generates random questions
        /// </summary>
        /// <returns></returns>
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

    /// <summary>
    /// class that represents a single question
    /// </summary>
    public class Question
    {
        public string QuestionString { get; set; }
        public string[] QuestionAnswers { get; set; }
        public int CorrectAnswer { get; set; }

        public Question()
        {
            this.QuestionAnswers = new string[4];
        }

        /// <summary>
        /// checks if answer is correct
        /// </summary>
        /// <param name="answer">index of answer 0-3 inclusive</param>
        /// <returns>true if correct,false if otherwise</returns>
        public bool IsCorrect(int answer)
        {
            return answer == this.CorrectAnswer;
        }
    }
}