using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Model;

namespace Task2.Worker
{
    class WorkerSentence
    {
        public List<Word> FindWordOfSpecificLength(Sentence sentence, int lengthWord)
        {
            var selectedWords = from elementSentence in sentence.ElementsSentence
                                where (elementSentence is Word) && (elementSentence.GetAmountSymbol() == lengthWord)
                                select elementSentence;
            var newList = new List<Word>();
            foreach (var element in selectedWords)
            {
                newList.Add((Word)element);
            }
            return newList;
        }

        public IEnumerable<string> FindWordsInInterrogativeSentences(List<Sentence> text, int lengthWord)
        {
            var newListWords = new List<Word>();
            var newListWordsString = new List<string>();
            var selecteSentens = from sentence in text
                                 where (sentence.GetElementValueByIndex(sentence.GetAmountElements() - 1) == "?")
                                 select sentence;
            foreach (var sentence in selecteSentens)
            {
                newListWords = FindWordOfSpecificLength(sentence, lengthWord);
                foreach (var word in newListWords)
                {
                    newListWordsString.Add(word.ToString());
                }
            }
            return newListWordsString.Distinct();
        }

        public void ReplaceWordsToSentence(Sentence sentence, int length, string newWordValue)
        {
            var selectedWord = from word in sentence.ElementsSentence
                               where ((word is Word) && (word.GetAmountSymbol() == length))
                               select word;
            foreach (var word in selectedWord)
            {
                word.Value = newWordValue;
            }
        }

        public void DeleteWordsGivenLength(List<Sentence> sentences, int lengthWord)
        {
            foreach (var sentence in sentences)
            {
                sentence.ElementsSentence.RemoveAll(x => (x is Word) && (x.GetAmountSymbol() == lengthWord) && (x.FirsLetterIsVowel()));
            }
        }
    }
}
