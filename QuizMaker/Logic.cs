
using System.Xml.Serialization;
using static QuizMaker.Question;


namespace QuizMaker
{
    public class Logic
    {
        public static Random random = new Random();

        /// <summary>
        /// Serialize and save the questions to a file with the given filename
        /// </summary>
        /// <param name="filename"></param>
        public static void SaveQuestions(string filename)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Question>));

                string folderPath = "..\\XML";
                Directory.CreateDirectory(folderPath);
                string fullPath = Path.Combine(folderPath, filename);
                using (FileStream stream = new FileStream(fullPath, FileMode.Create))
                {  
                    serializer.Serialize(stream, Program.Questions);
                }

                UIQuiz.QuestionsSavedSuccesfully(filename);
            }
            catch (IOException)
            {
                UIQuiz.ErrorSavingFile();
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
                XmlSerializer serializer = new XmlSerializer(typeof(List<Question>));

                string folderPath = "..\\XML";
                string fullPath = Path.Combine(folderPath, filename);
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    Program.Questions = (List<Question>)serializer.Deserialize(stream);
                }

                UIQuiz.QuestionsLoadedSuccesfully();
            }
            catch (FileNotFoundException)
            {
                UIQuiz.FileNotFound();
            }
            catch (IOException)
            {
                UIQuiz.ErrorLoadingQuestions();
            }
            catch (InvalidOperationException)
            {
                UIQuiz.ErrorDurringDeserialization();
            }
        }

        /// <summary>
        /// Start the quiz session
        /// </summary>
        public static void StartQuiz(List<Question> quizQuestions)
        {
            int score = 0;

            if (quizQuestions.Count == 0)
            {
                UIQuiz.NoQuestions();
                return;
            }

            UIQuiz.HowManyQuestions();

            int numRounds = UIQuiz.NumberOfRounds();

            if (numRounds < 1 || numRounds > quizQuestions.Count)
            {
                UIQuiz.InvalidNumberOfRounds();
                return;
            }

            List<Question> questionsToAsk = new List<Question>(quizQuestions);

            for (int i = 0; i < numRounds; i++)
            {

                int randomIndex = random.Next(questionsToAsk.Count);
                Question question = questionsToAsk[randomIndex];

                UIQuiz.ShowQuestions(question);

                for (int j = 0; j < question.Choices.Count; j++)
                {
                    UIQuiz.ShowAnswers(j, question);
                }

                UIQuiz.InputTheNumberOfAnswers(score, numRounds, question);

                questionsToAsk.RemoveAt(randomIndex);
            }
        }
    }
}
