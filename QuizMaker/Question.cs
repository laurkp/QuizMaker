
using System.Xml.Serialization;
using System.IO;

namespace QuizMaker
{
    [Serializable]
    public class Question
    {
        /// <summary>
        /// Defining a serializable class called 'Question'
        /// </summary>
        
            // Adding XML serialization attributes
            // Property to store the text of the question
            [XmlElement("QuestionText")]
            public string QuestionText { get; set; }

            // Property to store a list of possible answer choices
            [XmlArray("Choices")]
            [XmlArrayItem("Choice")]
            public List<string> Choices { get; set; }

            // Property to store a list of indices representing correct choices
            [XmlArray("CorrectChoices")]
            [XmlArrayItem("CorrectChoice")]
            public List<int> CorrectChoiceIndexes { get; set; }
        
    }
}
