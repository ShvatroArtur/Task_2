using System.Collections.Generic;

namespace Task2.Worker
{
    class WorkerPunctuation
    {
        public bool IsEndOfSentencePunctuationMark(List<char> value)
        {
            string PunctuationMark = "";
            foreach (var symbol in value)
            {
                PunctuationMark += symbol.ToString();
            }
            switch (PunctuationMark)
            {
                case "?":
                    return true;
                    break;
                case ".":
                    return true;
                    break;
                case "!":
                    return true;
                    break;
                case "?!":
                    return true;
                    break;
                case "!?":
                    return true;
                    break;
                case "...":
                    return true;
                    break;
                default:
                    return false;
                    break;
            }
        }
    }
}
