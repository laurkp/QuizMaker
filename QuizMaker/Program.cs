using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QuizMaker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Creating a continuous loop for the quiz program
            while (true)
            {
                // Prompt the user to choose a mode using UIQuiz.Choice() method
                string mode = UIQuiz.Choice();

                // Using a switch statement to handle the chosen mode
                switch (mode)
                {
                    case "1":
                        UIQuiz.AddQuestion();
                        break;
                    case "2":
                        UIQuiz.StartQuiz();
                        break;
                    case "3":
                        string saveFileName = UIQuiz.SaveFile();
                        UIQuiz.SaveQuestions(saveFileName);
                        break;
                    case "4":
                        string loadFileName = UIQuiz.LoadFile();   
                        UIQuiz.LoadQuestions(loadFileName);
                        break;
                    case "5":
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