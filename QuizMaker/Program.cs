﻿

namespace QuizMaker
{
    internal class Program
    {
        /// <summary>
        /// Static list to store quiz questions
        /// </summary>
        public static List<Question> Questions = new List<Question>();

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
                    case UIQuiz.GetUserChoice.AddQuestion:
                        UIQuiz.AddQuestion();
                        break;
                    case UIQuiz.GetUserChoice.StartQuiz:
                        Logic.StartQuiz(Questions);
                        break;
                    case UIQuiz.GetUserChoice.SaveToXml:
                        string saveFileName = UIQuiz.InputFileName();
                        Logic.SaveQuestions(saveFileName);
                        break;
                    case UIQuiz.GetUserChoice.LoadFromXml:
                        string loadFileName = UIQuiz.InputFileName();   
                        Logic.LoadQuestions(loadFileName);
                        break;
                    case UIQuiz.GetUserChoice.Quit:
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