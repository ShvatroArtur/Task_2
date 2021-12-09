using System;
using System.Configuration;
using Task2.Parser;
using Task2.TextReader;
using Task2.Worker;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            var separator = "------------------------------------";
            WorkerSentence workerSentence = new WorkerSentence();
            string textFilePath = ConfigurationManager.AppSettings["TextFilePath"];
            var path = ConfigurationManager.AppSettings["TextFilePath"]; ;
            Reader textReader = new Reader(path);
            if (textReader.ExistsFile())
            {
                TextParser textParser = new TextParser(true);
                textReader.Read(textParser);
                var text = textParser.Text;

                //Вывести все предложения.
                Console.WriteLine(separator);
                foreach (var word in text.Sentences)
                {
                    Console.WriteLine(word);
                }

                //Вывести все предложения заданного текста в порядке возрастания количества слов в каждом из них.
                Console.WriteLine(separator);
                foreach (var sentence in text.SortSentences())
                {
                    Console.WriteLine(sentence.ToString());
                }

                //Во всех вопросительных предложениях текста найти и напечатать без повторений слова заданной длины.
                Console.WriteLine(separator);
                foreach (var word in workerSentence.FindWordsInInterrogativeSentences(text.Sentences, 2))
                {
                    Console.WriteLine(word);
                }

                //В некотором предложении текста слова заданной длины заменить указанной подстрокой, длина которой может не совпадать с длиной слова.
                Console.WriteLine(separator);
                workerSentence.ReplaceWordsToSentence(text.Sentences[0], 5, "replace_text");
                foreach (var word in text.Sentences)
                {
                    Console.WriteLine(word);
                }

                //Из текста удалить все слова заданной длины, начинающиеся на согласную букву.
                Console.WriteLine(separator);
                workerSentence.DeleteWordsGivenLength(text.Sentences, 5);
                foreach (var word in text.Sentences)
                {
                    Console.WriteLine(word);
                }
            }
            else
            {
                Console.WriteLine("File not found.");
            }
        }
    }
}
