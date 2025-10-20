using System.Text;

public class TextParser
{
    string PunctuationChar = ".,!?;:-()\"'";
    string PunctuationCharEnd = ".!?";

    public Text ParseFromFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Файл не найден: {filePath}");
        }

        string text = File.ReadAllText(filePath);
        return Parse(text);
    }

    public Text Parse(string text)
    {
        var result = new Text();
        var currentSentence = new Sentence();
        var currentWord = new StringBuilder();

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
                    currentSentence.AddToken(new Word(currentWord.ToString()));
                    currentWord.Clear();
                }

                if (IsPunctuation(c))
                {
                    currentSentence.AddToken(new Punctuation(c.ToString()));
                }

                if (IsSentenceEnd(c))
                {
                    if (currentSentence.Tokens.Count > 0)
                    {
                        result.AddSentence(currentSentence);
                        currentSentence = new Sentence();
                    }
                }
            }
        }

        if (currentWord.Length > 0)
        {
            currentSentence.AddToken(new Word(currentWord.ToString()));
        }

        if (currentSentence.Tokens.Count > 0)
        {
            result.AddSentence(currentSentence);
        }

        return result;
    }

    private bool IsLetterOrDigit(char c)
    {
        return char.IsLetterOrDigit(c) || c == 'ё' || c == 'Ё';
    }

    private bool IsPunctuation(char c)
    {
        return PunctuationChar.Contains(c);
    }

    private bool IsSentenceEnd(char c)
    {
        return PunctuationCharEnd.Contains(c);
    }
}

public class StopWordsLoader
{
    public static HashSet<string> LoadStopWords(string filePath)
    {
        var stopWords = new HashSet<string>();

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Файл со стоп-словами не найден: {filePath}");
            return stopWords;
        }

        try
        {
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string word = line.Trim().ToLower();
                if (!string.IsNullOrEmpty(word))
                {
                    stopWords.Add(word);
                }
            }

            Console.WriteLine($"Загружено стоп-слов: {stopWords.Count} из файла {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при загрузке стоп-слов: {ex.Message}");
        }

        return stopWords;
    }
}