
using System.Xml.Serialization;
using static QuizMaker.Question;


namespace QuizMaker
{
    public class Logic
    {

        /// <summary>
        /// Serialize and save the questions to a file with the given filename
        /// </summary>
        /// <param name="filename"></param>
        public static void SaveQuestions( string filename)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Questions>));

                string folderPath = $@"E:\dssa\XML";
                string fullPath = Path.Combine(folderPath, filename);
                using (FileStream stream = new FileStream(fullPath, FileMode.Create))
                {
                    serializer.Serialize(stream, Program.questions);
                }

                Console.WriteLine($"Questions saved to {filename} successfully.\n");
            }
            catch (IOException e)
            {
                Console.WriteLine($"Error saving questions: {e.Message}\n");
            }
        }

        /// <summary>
        /// Deserialize and load questions from a file with the given filename
        /// </summary>
        /// <param name="filename"></param>
        public static void LoadQuestions(string filename)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Questions>));

                string folderPath = $@"E:\dssa\XML";
                string fullPath = Path.Combine(folderPath, filename);
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    Program.questions = (List<Questions>)serializer.Deserialize(stream);
                }

                Console.WriteLine("Questions loaded successfully.\n");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found. No questions loaded.\n");
            }
            catch (IOException e)
            {
                Console.WriteLine($"Error loading questions: {e.Message}\n");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Error during XML deserialization.\n");
            }
        }

        /// <summary>
        /// Start the quiz session
        /// </summary>
        public static void StartQuiz()
        {
            int score = 0;

            if (Program.questions.Count == 0)
            {
                Console.WriteLine("No questions available. Please add questions first.\n");
                return;
            }

            Console.Write($"How many questions do you want to answer? (1-{Program.questions.Count}):\n");
            int numRounds;
            while (!int.TryParse(Console.ReadLine(), out numRounds))
            {
                Console.WriteLine("Invalid input. Please choose how many questions do you want to answer.");
            }

            if (numRounds < 1 || numRounds > Program.questions.Count)
            {
                Console.WriteLine("Invalid number of rounds.\n");
                return;
            }

            List<Questions> questionsToAsk = new List<Questions>(Program.questions);

            Random random = new Random();

            for (int i = 0; i < numRounds; i++)
            {
                if (questionsToAsk.Count == 0)
                {
                    questionsToAsk = new List<Questions>(Program.questions);
                }

                int randomIndex = random.Next(questionsToAsk.Count);
                Questions question = questionsToAsk[randomIndex];

                Console.WriteLine(question.QuestionText);

                for (int j = 0; j < question.Choices.Count; j++)
                {
                    Console.WriteLine($"{j + 1}.{question.Choices[j]}\n");
                }

                Console.Write("Enter the number(s) of your answer(s) (comma-separated):\n");
                string userInput = Console.ReadLine();
                string[] userInpuArray = userInput.Split(',');

                List<int> userChoices = new List<int>();

                foreach (var choice in userInpuArray)
                {
                    if (int.TryParse(choice.Trim(), out int choiceIndex) && choiceIndex >= 1 && choiceIndex <= question.Choices.Count)
                    {
                        userChoices.Add(choiceIndex - 1);
                    }
                }

                if (userChoices.Count == question.CorrectChoiceIndexes.Count && userChoices.All(c => question.CorrectChoiceIndexes.Contains(c)))
                {
                    Console.WriteLine("Correct!\n");
                    score++;
                }
                else
                {
                    Console.WriteLine("Incorrect.\n");
                }
                questionsToAsk.RemoveAt(randomIndex);
            }

            Console.WriteLine($"You got {score}/{numRounds} correct.\n");
        }
    }
}
