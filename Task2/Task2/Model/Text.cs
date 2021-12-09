using System.Collections.Generic;
using System.Linq;

namespace Task2.Model
{
    class Text
    {
        private List<Sentence> sentences;    

        public List<Sentence> Sentences { get => sentences; private set { } }

        public Text() => sentences = new List<Sentence>();

        public void Add(Sentence sentence) => sentences.Add(sentence);

        public IEnumerable<Sentence> SortSentences()
        {
            return sentences.OrderBy(x => x.GetAmountWords());
        }    
        
        public override string ToString()
        {

            return base.ToString();
        }
    }
}
