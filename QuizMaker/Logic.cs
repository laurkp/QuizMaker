using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace QuizMaker
{
    internal class Logic
    {
        /// <summary>
        /// Defining a serializable class called 'Question'
        /// </summary>
        [Serializable]
        public class Question
        {
            // Property to store the text of the question
            public string QuestionText { get; set; }
            // Property to store a list of possible answer choices
            public List<string> Choices { get; set; }
            // Property to store a list of indices representing correct choices
            public List<int> CorrectChoices { get; set; }
        }
    }
}
