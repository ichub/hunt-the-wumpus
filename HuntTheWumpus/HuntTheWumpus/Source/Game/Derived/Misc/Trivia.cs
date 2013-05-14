using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HuntTheWumpus.Source.Game.Derived.Misc
{
    public class Trivia
    {
        String fileName;
        List<QuestionAndAnswer> unaskedQuestions;
        List<QuestionAndAnswer> askedQuestions;
        List<QuestionAndAnswer> triviaNotGiven;
        List<QuestionAndAnswer> triviaGiven; 
        QuestionAndAnswer lastQuestionAsked;
        Statistics stats;
       
        public Trivia(String fileName)
        {
            stats = new Statistics();
            askedQuestions = new List<QuestionAndAnswer>();
            triviaGiven = new List<QuestionAndAnswer>();
            this.fileName = fileName;
            readFromFile();
        }

        private void readFromFile()
        {
            unaskedQuestions = new List<QuestionAndAnswer>();
            triviaNotGiven = new List<QuestionAndAnswer>();
            using (StreamReader reader =  new StreamReader(fileName))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] list = line.Split('|');
                    if (list.Length == 3)
                    {
                        string question = list[0].Trim();
                        string answer = list[1].Trim();
                        string trivia = list[2].Trim();
                        QuestionAndAnswer q = new QuestionAndAnswer(question, answer, trivia);
                        unaskedQuestions.Add(q);
                        triviaNotGiven.Add(q);
                    }
                }
            }
        }

        /// <summary> 
        /// Gives a random piece of trivia that has not been given before
        /// </summary>
        /// <returns> a piece of trivia </returns>
        public String getTrivia()
        {
            Random generator = new Random();
            if (triviaNotGiven.Count == 0)
            {
                throw new InvalidOperationException("No more trivia to give.");
            }
            int index = generator.Next(triviaNotGiven.Count);
            QuestionAndAnswer t = triviaNotGiven[index];
            triviaNotGiven.RemoveAt(index);
            triviaGiven.Add(t);
            return t.getTrivia();
        }

        /// <summary>
        /// Starts a trivia challenge with a given number of questions
        /// </summary>
        /// <param name="questions"> the number of questions to be asked (depending on game situation) </param>
        /// <param name="correct"> the number of questions that need to be answered correctly to win </param>
        public void startTriviaChallenge(int questions, int correct)
        {
            if (questions < correct)
            {
                throw new ArgumentException("Number of questions should be greater than or equal to the number needed to win.");
            }
            stats = new Statistics();
            stats.totalQuestions = questions;
            stats.numNeededToWin = correct;
        }

        /// <summary>
        /// Gets a trivia question that has not been asked before. Can only be called after startTriviaChalenge().
        /// </summary>
        /// <returns> a new trivia question </returns>
        public String getQuestion()
        {
            Random generator = new Random();
            if (unaskedQuestions.Count == 0)
            {
                throw new InvalidOperationException("No more questions to ask.");
            }
            int index = generator.Next(unaskedQuestions.Count);
            lastQuestionAsked = unaskedQuestions[index];
            unaskedQuestions.RemoveAt(index);
            askedQuestions.Add(lastQuestionAsked);
            stats.numAsked++;
            return lastQuestionAsked.getQuestion();
        }
        /// <summary>
        /// Checks if the user's answer to the question returned by the previous call to getQuestion() is correct. 
        /// This method should only be called after getQuestion()  
        /// </summary>
        /// <param name="answer"> the user's answer </param>
        /// <returns> whether or not the answer to the question is correct, and if the user has passed or failed the trivia challenge </returns>
        public TriviaResult checkAnswer(String answer)
        {
            if(answer.Equals(lastQuestionAsked.getAnswer()))
            {
                TriviaResult t = new TriviaResult();
                t.correctAnswer = true;
                stats.numCorrect++;
                if (stats.numCorrect >= stats.numNeededToWin)
                {
                    t.passedChallenge = true;
                }
                return t;
            }
            else
            {
                TriviaResult t = new TriviaResult();
                t.correctAnswer = false;
                if (stats.numAsked - stats.numCorrect > stats.totalQuestions - stats.numNeededToWin)
                {
                    t.failedChallenge = true;
                }
                return t;
            }
        }
        /// <summary>
        /// Gets all statistics about the current challenge
        /// </summary>
        /// <returns> the number of questions asked, the number the user has answered correctly,
        /// and the total number of questions for this trivia challenge </returns>
        public Statistics getCurrentChallengeStatistics()
        {
            return stats;
        }

    }
    public class QuestionAndAnswer
    {
        string question;
        string answer;
        string trivia;

        public QuestionAndAnswer(string question, string answer, string trivia)
        {
            this.question = question;
            this.answer = answer;
            this.trivia = trivia;
        }

        public string getQuestion()
        {
            return question;
        }

        public string getAnswer()
        {
            return answer;
        }

        public string getTrivia()
        {
            return trivia;
        }

    }

    public class TriviaResult
    {
        public bool correctAnswer;
        public bool passedChallenge;
        public bool failedChallenge;
        public override string ToString()
        {
            return "Correct Answer = " + correctAnswer + " Passed Challenge = " + passedChallenge + " Failed Challenge = " + failedChallenge;
        }
    }

    public class Statistics
    {
        public int numAsked;
        public int numCorrect;
        public int totalQuestions;
        public int numNeededToWin;
    }

}
