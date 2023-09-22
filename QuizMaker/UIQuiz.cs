﻿
using static QuizMaker.Question.Questions;
using static QuizMaker.Question;


namespace QuizMaker
{
    internal class UIQuiz
    {
        public const char USER_CHOICE_YES = 'Y';
        public const char USER_CHOICE_NO = 'N';
        /// <summary>
        /// enum to distinguish the user choices
        /// </summary>
        public enum UserChoices
        {
            AddQuestion = 1,
            StartQuiz,
            SaveToXml,
            LoadFromXml,
            Quit 
        }
        /// <summary>
        /// Display the main menu and return the user's choice
        /// </summary>
        /// <returns></returns>
        public static UserChoices Choice()
        {
            Console.WriteLine("1.Add a Question");
            Console.WriteLine("2.Start Quiz");
            Console.WriteLine("3.Save Questions to XML");
            Console.WriteLine("4.Load Questions from XML");
            Console.WriteLine("5.Quit\n");

            if(Enum.TryParse(Console.ReadLine(), out UserChoices choice))
            {
                return choice;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please select a valid option.");
                return Choice();
            }
        }

        /// <summary>
        /// Method to add a new question to the quiz
        /// </summary>
        public static void AddQuestion()
        {
            Console.Write("Enter the question: \n");
            string questionText = Console.ReadLine();

            List<string> answers = new List<string>();
            List<int> correctAnswers = new List<int>();

            Console.Write("Enter the number of answers: \n");
            int numAnswers;
            while (!int.TryParse(Console.ReadLine(), out numAnswers))
            {
                Console.WriteLine("Invalid input. Please choose how many answers the question has.");
            }

            for (int i = 0; i < numAnswers; i++)
            {
                Console.Write($"Enter answer {i + 1}: \n");
                string answer = Console.ReadLine();
                answers.Add(answer);

                char isCorrect;
                while (true)
                {
                    Console.Write($"Is answer {i + 1} correct? ({USER_CHOICE_YES} = yes/{USER_CHOICE_NO} = no): \n");
                    isCorrect = char.ToUpper(Console.ReadKey().KeyChar);
                    Console.WriteLine();
                    if (isCorrect == USER_CHOICE_YES || isCorrect == USER_CHOICE_NO)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input please write 'Y' or 'N' for the answer.");
                    }
                }
                if (isCorrect == USER_CHOICE_YES)
                {
                    correctAnswers.Add(i);
                }
            }
            // Creating a new Question object and add it to the questions list
            Question.Questions newQuestion = new Question.Questions()
            {
                QuestionText = questionText,
                Choices = answers,
                CorrectChoiceIndexes = correctAnswers
            };

            Program.questions.Add(newQuestion);
        }
        /// <summary>
        /// Prompt the user to enter a file name to save questions
        /// </summary>
        /// <returns></returns>
        public static string InputFileName()
        {
            Console.WriteLine("Enter a file name to save questions: \n");
            string saveFile = Console.ReadLine();
            return saveFile;
        }
        /// <summary>
        /// Prompt the user to enter a file name to load questions
        /// </summary>
        /// <returns></returns>
        public static string AskForFileName()
        {
            Console.WriteLine("Enter a file name to load questions: \n");
            string loadFile = Console.ReadLine();
            return loadFile;
        }

        /// <summary>
        /// Display a message for an invalid choice
        /// </summary>
        public static void InvalidChoice()
        {
            Console.WriteLine("Invalid choice. Please select a valid option.\n");
        }
    }
}
