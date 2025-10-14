public class Sentence
{
    public List<object> Elements { get; set; } = new List<object>();

    public void AddWord(Word word)
    {
        Elements.Add(word);
    }

    public void AddPunctuation(Punctuation punctuation)
    {
        Elements.Add(punctuation);
    }
}