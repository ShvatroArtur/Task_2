using System.Text.RegularExpressions;
using Task2.Interface;

namespace Task2.Model
{
    class Word:IElementSentence
    {
        public string Value { get; set; }

        public override string ToString() => Value;

        public int GetAmountSymbol()
        {
            return Value.Length;
        }

        public Word(string value) => Value = value;

        public bool FirsLetterIsVowel()
        {
            string key = @"[aeiou]";
            if (!(Regex.Matches(Value[0].ToString(), key).Count > 0))
            {
                return true;
            }
            else return false;
        }
    }
}
