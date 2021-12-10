using Task2.Interface;

namespace Task2.Model
{
    class PunctuationMark : IElementSentence
    {
        private readonly bool isEndSentence;

        public string Value { get; set; }

        public PunctuationMark(string value, bool isEndSentence)
        {
            Value = value;
            this.isEndSentence = isEndSentence;

        }

        public int GetAmountSymbol()
        {
            return Value.Length;
        }

        public bool IsEndSentence { get => isEndSentence; private set { } }

        public override string ToString()
        {
            return Value;
        }

        public bool FirsLetterIsConsonant()
        {
            return false;
        }
    }
}
