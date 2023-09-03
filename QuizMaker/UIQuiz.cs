﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static QuizMaker.Logic;

namespace QuizMaker
{
    internal class UIQuiz
    {
        /// <summary>
        /// Static list to store quiz questions
        /// </summary>
        public static List<Question> questions = new List<Question>();

        /// <summary>
        /// Display the main menu and return the user's choice
        /// </summary>
        /// <returns></returns>
        public static string Choice()
        {
            Console.WriteLine("1.Add a Question");
            Console.WriteLine("2.Start Quiz");
            Console.WriteLine("3.Save Questions to XML");
            Console.WriteLine("4.Load Questions from XML");
            Console.WriteLine("5.Quit\n");

            string choice = Console.ReadLine();

            return choice;
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
            int numAnswers = int.Parse(Console.ReadLine());

            for (int i = 0; i < numAnswers; i++)
            {
                Console.Write($"Enter answer {i + 1}: \n");
                string answer = Console.ReadLine();
                answers.Add(answer);

                Console.Write($"Is answer {i + 1} correct? (yes/no): \n");
                string isCorrect = Console.ReadLine().ToLower();

                if (isCorrect == "yes")
                {
                    correctAnswers.Add(i);
                }
            }
            // Creating a new Question object and add it to the questions list
            Question newQuestion = new Question()
            {
                QuestionText = questionText,
                Choices = answers,
                CorrectChoices = correctAnswers
            };

            questions.Add(newQuestion);
        }
        /// <summary>
        /// Prompt the user to enter a file name to save questions
        /// </summary>
        /// <returns></returns>
        public static string SaveFile()
        {
            Console.WriteLine("Enter a file name to save questions: \n");
            string saveFile = Console.ReadLine();
            return saveFile;
        }
        /// <summary>
        /// Prompt the user to enter a file name to load questions
        /// </summary>
        /// <returns></returns>
        public static string LoadFile()
        {
            Console.WriteLine("Enter a file name to load questions: \n");
            string loadFile = Console.ReadLine();
            return loadFile;
        }
        /// <summary>
        /// Serialize and save the questions to a file with the given filename
        /// </summary>
        /// <param name="filename"></param>
        public static void SaveQuestions(string filename)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Question>));
                using (FileStream stream = new FileStream(filename, FileMode.Create))
                {
                    serializer.Serialize(stream, questions);
                }

                Console.WriteLine("Questions saved to {filename} successfully.\n");
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
                XmlSerializer serializer = new XmlSerializer(typeof(List<Question>));
                using (FileStream stream = new FileStream(filename, FileMode.Open))
                {
                    questions = (List<Question>)serializer.Deserialize(stream);
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

            if (questions.Count == 0)
            {
                Console.WriteLine("No questions available. Please add questions first.\n");
                return;
            }

            Console.Write($"How many questions do you want to answer? (1-{questions.Count}):\n");
            int numRounds = int.Parse(Console.ReadLine());

            if (numRounds < 1 || numRounds > questions.Count)
            {
                Console.WriteLine("Invalid number of rounds.\n");
                return;
            }

            List<Question> questionsToAsk = new List<Question>(questions);

            Random random = new Random();

            for(int i = 0; i < numRounds; i++)
            {
                if (questionsToAsk.Count == 0)
                {
                    questionsToAsk = new List<Question>(questions);
                }

                int randomIndex = random.Next(questionsToAsk.Count);
                Question question = questionsToAsk[randomIndex];

                Console.WriteLine(question.QuestionText);

                for(int j = 0; j < question.Choices.Count; j++)
                {
                    Console.WriteLine($"{j + 1}.{question.Choices[j]}\n");
                }

                Console.Write("Enter the number(s) of your answer(s) (comma-separated):\n");
                string userInput = Console.ReadLine();
                string[] userInpuArray = userInput.Split(',');

                List<int> userChoices = new List<int>();

                foreach (var choice in userInpuArray)
                {
                    if(int.TryParse(choice.Trim(), out int choiceIndex) && choiceIndex >= 1 && choiceIndex <= question.Choices.Count)
                    {
                        userChoices.Add(choiceIndex - 1);
                    }
                }

                if (userChoices.Count == question.CorrectChoices.Count && userChoices.All(c => question.CorrectChoices.Contains(c)))
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
        /// <summary>
        /// Display a message for an invalid choice
        /// </summary>
        public static void InvalidChoice()
        {
            Console.WriteLine("Invalid choice. Please select a valid option.\n");
        }
    }
}