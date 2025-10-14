class Program
{
    public class TextParser
    {
        public Text Parse(string text)
        {
            var result = new Text();
            var currentSentence = new Sentence();
            var currentWord = new System.Text.StringBuilder();

            foreach (char c in text)
            {
                if (IsLetterOrDigit(c))
                {
                    currentWord.Append(c);
                }
                else
                {
                    if (currentWord.Length > 0)
                    {
                        currentSentence.AddWord(new Word(currentWord.ToString()));
                        currentWord.Clear();
                    }
                    if (IsPunctuation(c))
                    {
                        currentSentence.AddPunctuation(new Punctuation(c.ToString()));
                    }
                    if (c == '.' || c == '!' || c == '?')
                    {
                        if (currentSentence.Elements.Count > 0)
                        {
                            result.AddSentence(currentSentence);
                            currentSentence = new Sentence();
                        }
                    }
                }
            }

            return result;
        }

        private bool IsLetterOrDigit(char c)
        {
            return char.IsLetterOrDigit(c) || c == 'ё' || c == 'Ё';
        }

        private bool IsPunctuation(char c)
        {
            return char.IsPunctuation(c) || c == ',' || c == ';' || c == ':';
        }
    }
    static void Main(string[] args)
    {
        Console.WriteLine("Hello world!");
    }
}