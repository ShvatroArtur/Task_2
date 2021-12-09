using System;
using System.IO;
using Task2.Parser;

namespace Task2.TextReader
{
    class Reader
    {
        private string path;
        public Reader(string path)
        {
            this.path = path;
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
        public void Read(TextParser parser)
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
