﻿

namespace QuizMaker
{
    internal class Program
    {
        /// <summary>
        /// Static list to store quiz questions
        /// </summary>
        public static List<Question> questions = new List<Question>();

        static void Main(string[] args)
        {
            // Creating a continuous loop for the quiz program
            while (true)
            {
                // Prompt the user to choose a mode using UIQuiz.Choice() method
                var mode = UIQuiz.Choice();

                // Using a switch statement to handle the chosen mode
                switch (mode)
                {
                    case UIQuiz.UserChoices.AddQuestion:
                        UIQuiz.AddQuestion();
                        break;
                    case UIQuiz.UserChoices.StartQuiz:
                        Logic.StartQuiz(questions);
                        break;
                    case UIQuiz.UserChoices.SaveToXml:
                        string saveFileName = UIQuiz.InputFileName();
                        Logic.SaveQuestions(saveFileName);
                        break;
                    case UIQuiz.UserChoices.LoadFromXml:
                        string loadFileName = UIQuiz.AskForFileName();   
                        Logic.LoadQuestions(loadFileName);
                        break;
                    case UIQuiz.UserChoices.Quit:
                        Environment.Exit(0);
                        break;
                    default:
                        UIQuiz.InvalidChoice(); 
                        break;
                }
            }
        }
    }
}