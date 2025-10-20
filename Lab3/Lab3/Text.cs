public class Text
{
    public List<Sentence> Sentences { get; set; } = new List<Sentence>();

    public void AddSentence(Sentence sentence)
    {
        Sentences.Add(sentence);
    }

    public List<Sentence> GetSentencesByWordCount()
    {
        var sortedSentences = new List<Sentence>(Sentences);

        for (int i = 0; i < sortedSentences.Count - 1; i++)
        {
            for (int j = i + 1; j < sortedSentences.Count; j++)
            {
                if (sortedSentences[i].GetWordCount() > sortedSentences[j].GetWordCount())
                {
                    var temp = sortedSentences[i];
                    sortedSentences[i] = sortedSentences[j];
                    sortedSentences[j] = temp;
                }
            }
        }

        return sortedSentences;
    }
    public List<Sentence> GetSentencesByLength()
    {
        var sortedSentences = new List<Sentence>(Sentences);

        for (int i = 0; i < sortedSentences.Count - 1; i++)
        {
            for (int j = i + 1; j < sortedSentences.Count; j++)
            {
                if (sortedSentences[i].Length > sortedSentences[j].Length)
                {
                    var temp = sortedSentences[i];
                    sortedSentences[i] = sortedSentences[j];
                    sortedSentences[j] = temp;
                }
            }
        }

        return sortedSentences;
    }
    public List<Word> FindWordsInQuestions(int length)
    {
        var result = new List<Word>();
        var uniqueWords = new HashSet<string>();

        foreach (var sentence in Sentences)
        {
            if (sentence.IsQuestion)
            {
                foreach (var word in sentence.GetWords())
                {
                    if (word.Value.Length == length)
                    {
                        string lowerWord = word.Value.ToLower();
                        if (!uniqueWords.Contains(lowerWord))
                        {
                            uniqueWords.Add(lowerWord);
                            result.Add(word);
                        }
                    }
                }
            }
        }

        return result;
    }
    public void RemoveWordsWithConsonant(int length)
    {
        var consonants = "бвгджзйклмнпрстфхцчшщbcdfghjklmnpqrstvwxz";

        foreach (var sentence in Sentences)
        {
            for (int i = sentence.Tokens.Count - 1; i >= 0; i--)
            {
                var token = sentence.Tokens[i];
                if (token is Word word && word.Value.Length == length)
                {
                    if (!string.IsNullOrEmpty(word.Value) &&
                        consonants.Contains(char.ToLower(word.Value[0])))
                    {
                        sentence.Tokens.RemoveAt(i);
                    }
                }
            }
        }
    }

    public void ReplaceWordsInSentence(int sentenceIndex, int wordLength, string replacement)
    {
        if (sentenceIndex >= 0 && sentenceIndex < Sentences.Count)
        {
            var sentence = Sentences[sentenceIndex];

            foreach (var token in sentence.Tokens)
            {
                if (token is Word word && word.Value.Length == wordLength)
                {
                    word.Value = replacement;
                }
            }
        }
    }

    public void RemoveStopWords(HashSet<string> stopWords)
    {
        foreach (var sentence in Sentences)
        {
            for (int i = sentence.Tokens.Count - 1; i >= 0; i--)
            {
                var token = sentence.Tokens[i];
                if (token is Word word && stopWords.Contains(word.Value.ToLower()))
                {
                    sentence.Tokens.RemoveAt(i);
                }
            }
        }
    }
}