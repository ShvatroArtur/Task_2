using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Interface;
using Task2.Model;
using Task2.Worker;

namespace Task2.Parser
{
    class TextParser
    {
        private Text text = new Text();
        private Sentence bufferSentence = new Sentence(new List<IElementSentence>());
        private List<Word> bufferWord = new List<Word>();
        private StringBuilder bufferLetters = new StringBuilder();
        private List<char> bufferPunctuationMarks = new List<char>();
        public bool isStart;
        private char previousCharacter;
        private char actualCharacter;
        private string dictKey;
        private Dictionary<string, Action> dictActionParser = new Dictionary<string, Action>();
        private WorkerSentence workerSentence = new WorkerSentence();       
        private WorkerPunctuation workerPunctuation = new WorkerPunctuation();


        public Text Text { get => text; private set { } }

        public TextParser(bool isStart)
        {
            this.isStart = isStart;
            dictKey = "";
            InitializationDictActionParser();
        }

        private void InitializationDictActionParser()
        {
            dictActionParser.Add("ss", SymbolSymbolAction);
            dictActionParser.Add(" s", SpaseSymbolAction);
            dictActionParser.Add("s ", SymbolSpaseAction);
            dictActionParser.Add("  ", SpaseSpaceAction);
            dictActionParser.Add(" ,", SpacePunctuationMarkAction);
            dictActionParser.Add(",,", PunctuationMarkPunctuationMarkAction);
            dictActionParser.Add(", ", PunctuationMarkSpaceAction);
            dictActionParser.Add("s,", SymbolPunctuationMarkAction);
        }

        public void Parsing(char value, bool isParsing)
        {
            if (isParsing)
            {
                actualCharacter = value;
                if (isStart)
                {
                    isStart = false;
                    previousCharacter = ' ';
                }
                dictKey = ParsingPunctuationMark(previousCharacter).ToString() + ParsingPunctuationMark(actualCharacter).ToString();
                if (dictActionParser.ContainsKey(dictKey))
                {
                    dictActionParser[dictKey]();
                }
                else
                {
                    dictActionParser["ss"]();
                }
                previousCharacter = actualCharacter;
            }
            else
            {
                AddWordToSentence();
                AddPunctuationMarkToSentence();
            }
        }
        
        private char ParsingPunctuationMark(char symbol)
        {
            if (Char.IsPunctuation(symbol))
            {
                return ',';
            }
            else
            {
                if (Char.IsWhiteSpace(symbol))
                    return ' ';
                else
                    return 's';
            }
        }
        
        private void AddWordToSentence()
        {
            if (bufferLetters.Length != 0)
            {
                bufferSentence.AddElementToSentence(new Word(bufferLetters.ToString()));
                bufferLetters.Clear();
            }
        }

        private void AddLetterToWord(char value)
        {
            bufferLetters.Append(value);
        }

        private void AddPunctuationMarkToSentence()
        {
            if (bufferPunctuationMarks.Count != 0)
            {
                var builder = new StringBuilder();
                foreach (var elementPunctuationMark in bufferPunctuationMarks)
                {
                    builder.Append(elementPunctuationMark);
                }
                bufferSentence.AddElementToSentence(new PunctuationMark(builder.ToString(), workerPunctuation.IsEndOfSentencePunctuationMark(bufferPunctuationMarks)));
                if (workerPunctuation.IsEndOfSentencePunctuationMark(bufferPunctuationMarks))
                {
                    AddSentenceToText();
                }
                bufferPunctuationMarks.Clear();
            }
        }

        private void AddSentenceToText()
        {
            text.Add(new Sentence(bufferSentence.ElementsSentence));
            bufferSentence.ClearAllElements();
        }

        private void AddPunctuationMarkToBuffer(char value)
        {
            bufferPunctuationMarks.Add(value);
        }

        private void SymbolSymbolAction()
        {
            AddLetterToWord(actualCharacter);
        }
        private void SymbolSpaseAction()
        {
            AddWordToSentence();
        }
        private void SpaseSymbolAction()
        {
            SymbolSymbolAction();
        }
        private void SpaseSpaceAction()
        {
        }
        private void SpacePunctuationMarkAction()
        {
            AddPunctuationMarkToBuffer(actualCharacter);            
        }
        private void PunctuationMarkPunctuationMarkAction()
        {
            SpacePunctuationMarkAction();
        }

        private void SymbolPunctuationMarkAction()
        {           
            AddPunctuationMarkToBuffer(actualCharacter);
        }
        private void PunctuationMarkSpaceAction()
        {
            AddWordToSentence();
            AddPunctuationMarkToSentence();
        }   
    }
}
