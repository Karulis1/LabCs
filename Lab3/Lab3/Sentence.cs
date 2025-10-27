using System.Text;
using System.Xml.Serialization;
[XmlRoot("Sentence")]
public class Sentence
{
    [XmlElement("Word", Type = typeof(Word))]
    [XmlElement("Punctuation", Type = typeof(Punctuation))]
    public List<Token> Tokens { get; set; } = new List<Token>();

    public int Length
    {
        get
        {
            int length = 0;
            foreach (var token in Tokens)
            {
                length += token.Value.Length;
            }
            return length;
        }
    }

    public bool IsQuestion
    {
        get
        {
            if (Tokens.Count == 0) return false;
            var lastToken = Tokens[Tokens.Count - 1];
            return lastToken is Punctuation && lastToken.Value == "?";
        }
    }

    public void AddToken(Token token)
    {
        Tokens.Add(token);
    }

    public List<Word> GetWords()
    {
        var words = new List<Word>();
        foreach (var token in Tokens)
        {
            if (token is Word word)
            {
                words.Add(word);
            }
        }
        return words;
    }

    public int GetWordCount()
    {
        int count = 0;
        foreach (var token in Tokens)
        {
            if (token is Word)
            {
                count++;
            }
        }
        return count;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var token in Tokens)
        {
            if (token is Word)
            {
                if (sb.Length > 0 && !char.IsPunctuation(sb[sb.Length - 1]))
                    sb.Append(' ');
                sb.Append(token.Value);
            }
            else if (token is Punctuation)
            {
                sb.Append(token.Value);
            }
        }
        return sb.ToString();
    }
}