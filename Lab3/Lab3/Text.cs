public class Text
{
    public List<Sentence> Sentences { get; set; } = new List<Sentence>();

    public void AddSentence(Sentence sentence)
    {
        Sentences.Add(sentence);
    }
}