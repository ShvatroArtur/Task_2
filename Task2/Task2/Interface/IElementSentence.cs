namespace Task2.Interface
{
    interface IElementSentence
    {
        string Value { get; set; }
        int GetAmountSymbol();
        bool FirsLetterIsVowel();
    }
}
