using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task2.Interface;

namespace Task2.Model
{
    class Sentence
    {
        private List<IElementSentence> elementsSentence;
        public List<IElementSentence> ElementsSentence { get => elementsSentence; private set { } }

        public Sentence(List<IElementSentence> elementsSentece)
        {
            List<IElementSentence> newList = new List<IElementSentence>(elementsSentece.Count);
            elementsSentece.ForEach((item) =>
            {
                if (item is Word)
                {
                    newList.Add(new Word(item.Value));
                }
                else
                {
                    PunctuationMark punctuationMark = (PunctuationMark)item;
                    newList.Add(new PunctuationMark(punctuationMark.Value, punctuationMark.IsEndSentence));
                }
            });
            this.elementsSentence = newList;
        }

        public void AddElementToSentence(IElementSentence elementsSentence)
        {
            this.elementsSentence.Add(elementsSentence);
        }

        public void ClearAllElements()
        {
            elementsSentence.Clear();
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(elementsSentence[0].ToString());
            IElementSentence previousValue = elementsSentence[0];
            for (int i = 1; i < elementsSentence.Count; i++)
            {
                if (!(elementsSentence[i] is PunctuationMark))
                {
                    builder.Append(" ");
                }
                builder.Append(elementsSentence[i].ToString());
            }
            return builder.ToString();
        }

        public int GetAmountWords()
        {
            int number = 0;
            foreach (var elementsSentence in this.elementsSentence)
            {
                if (elementsSentence is Word)
                {
                    number++;
                }
            }
            return number;
        }

        public IElementSentence GetElementByIndex(int index)
        {
            if (index < 0 || index >= elementsSentence.Count) return null;
            return elementsSentence[index];
        }

        public int GetAmountElements()
        {
            return elementsSentence.Count;
        }

        public string GetElementValueByIndex(int index)
        {
            if (index < 0 || index >= elementsSentence.Count) return null;
            return elementsSentence[index].Value;
        }

    }
}
