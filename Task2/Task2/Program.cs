using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Task2.Model;
using Task2.Parser;
using Task2.TextReader;
using Task2.Worker;

namespace Task2
{
    class Program
    {
        private static void OutputSeparatorOnDisplay(string str)
        {
            Console.WriteLine("=======================================");
            Console.WriteLine(str);
            Console.WriteLine("=======================================");
        }

        private static void OuputSentencesOnDisplay(IEnumerable<Sentence> sentences)
        {
            foreach (var sentence in sentences)
            {
                Console.WriteLine(sentence);
            }
        }
        static void Main(string[] args)
        {
            OutputSeparatorOnDisplay("Задача №2");
            WorkerSentence workerSentence = new WorkerSentence();
            var path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + ConfigurationManager.AppSettings["TextFilePath"]; ;
            TextParser textParser = new TextParser(true);
            Reader textReader = new Reader(path, textParser);
            if (textReader.ExistsFile())
            {
                
                textReader.Read();
                var text = textParser.Text;

                OutputSeparatorOnDisplay("Вывести все предложения.");
                OuputSentencesOnDisplay(text.Sentences);


                OutputSeparatorOnDisplay("Вывести все предложения заданного текста в порядке возрастания количества слов в каждом из них.");
                OuputSentencesOnDisplay(text.SortSentences());



                OutputSeparatorOnDisplay("Во всех вопросительных предложениях текста найти и напечатать без повторений слова заданной длины.");
                foreach (var word in workerSentence.FindWordsInInterrogativeSentences(text.Sentences, 2))
                {
                    Console.WriteLine(word);
                }

                OutputSeparatorOnDisplay("В некотором предложении текста слова заданной длины заменить указанной подстрокой, длина которой может не совпадать с длиной слова.");
                if (text.Sentences.Count > 0)
                {
                    workerSentence.ReplaceWordsToSentence(text.Sentences[0], 5, "replace_text");
                    OuputSentencesOnDisplay(text.Sentences);
                }


                OutputSeparatorOnDisplay("Из текста удалить все слова заданной длины, начинающиеся на согласную букву.");
                workerSentence.DeleteWordsGivenLength(text.Sentences, 5);
                OuputSentencesOnDisplay(text.Sentences);
            }
            else
            {
                Console.WriteLine("File not found.");
                Console.ReadLine();
            }
        }
    }
}
