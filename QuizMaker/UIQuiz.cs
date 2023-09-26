

using static QuizMaker.Question;
using static System.Formats.Asn1.AsnWriter;


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
            Question newQuestion = new Question()
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

        public static void QuestionsSavedSuccesfully(string savedFile)
        {
            Console.WriteLine($"Questions saved to {savedFile} successfully.\n");
        }

        public static void ErrorSavingFile()
        {
            Console.WriteLine($"Error saving questions.\n");
        }

        public static void QuestionsLoadedSuccesfully()
        {
            Console.WriteLine("Questions loaded successfully.\n");
        }

        public static void FileNotFound()
        {
            Console.WriteLine("File not found. No questions loaded.\n");
        }

        public static void ErrorLoadingQuestions()
        {
            Console.WriteLine($"Error loading questions.\n");
        }

        public static void ErrorDurringDeserialization()
        {
            Console.WriteLine("Error during XML deserialization.\n");
        }

        public static void NoQuestions()
        {
            Console.WriteLine("No questions available. Please add questions first.\n");
        }

        public static void HowManyQuestions()
        {
            Console.Write($"How many questions do you want to answer? (1-{Program.questions.Count}):\n");
        }

        public static int NumberOfRounds()
        {
            int number;
            while (!int.TryParse(Console.ReadLine(), out number))
            {
                Console.WriteLine("Invalid input. Please choose how many questions do you want to answer.");
            }
            return number;
        }

        public static void InvalidNumberOfRounds()
        {
            Console.WriteLine("Invalid number of rounds.\n");
        }

        public static void ShowQuestions(Question userQuestion)
        {
            Console.WriteLine(userQuestion.QuestionText);
        }

        public static void ShowAnswers(int questionAnswers, Question userQuestion)
        {
            Console.WriteLine($"{questionAnswers + 1}.{userQuestion.Choices[questionAnswers]}\n");
        }

        public static void InputTheNumberOfAnswers(int count, int numAnswers, Question userQuestion)
        {
            Console.Write("Enter the number(s) of your answer(s) (comma-separated):\n");
            string userInput = Console.ReadLine();
            string[] userInpuArray = userInput.Split(',');

            List<int> userChoices = new List<int>();

            foreach (var choice in userInpuArray)
            {
                if (int.TryParse(choice.Trim(), out int choiceIndex) && choiceIndex >= 1 && choiceIndex <= userQuestion.Choices.Count)
                {
                    userChoices.Add(choiceIndex - 1);
                }
            }

            if (userChoices.Count == userQuestion.CorrectChoiceIndexes.Count && userChoices.All(c => userQuestion.CorrectChoiceIndexes.Contains(c)))
            {
                Console.WriteLine("Correct!\n");
                count++;
            }
            else
            {
                Console.WriteLine("Incorrect.\n");
            }

            Console.WriteLine($"You got {count+1}/{numAnswers} correct.\n");
        }

    }
}
