using System;
using System.IO;
using Task2.Parser;

namespace Task2.TextReader
{
    class Reader
    {
        private readonly string path;
        private readonly TextParser parser;

        public Reader(string path, TextParser parser)
        {
            this.path = path;
            this.parser = parser;
        }
        public bool ExistsFile()
        {
            if (File.Exists(path))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public void Read()
        {
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                var isParsing = true;
                do
                {
                    if (sr.Peek() > -1)
                    {
                        parser.Parsing((char)sr.Read(), isParsing);
                    }
                    else
                    {
                        isParsing = false;
                        parser.Parsing(' ', isParsing);
                    }
                }
                while (isParsing);
            }
        }
    }
}
