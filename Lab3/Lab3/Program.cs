using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Serialization;

class Program
{
private static Text text;
private static HashSet<string> stopWords;

public static void Main()
{
    try
    {
        stopWords = StopWordsLoader.LoadStopWords("StopWordsRU.txt");

        var parser = new TextParser();
        text = parser.ParseFromFile("TEXT_Lab3.txt");

        Console.WriteLine($"Текст успешно загружен. Предложений: {text.Sentences.Count}");

        ShowMenu();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ошибка: {ex.Message}");
        Console.ReadKey();
    }
}

public static void ShowMenu()
{
    while (true)
    {
        Console.WriteLine();
        Console.WriteLine("1. Предложения по количеству слов");
        Console.WriteLine("2. Предложения по длине");
        Console.WriteLine("3. Слова в вопросительных предложениях");
        Console.WriteLine("4. Удалить слова по длине и первой букве");
        Console.WriteLine("5. Заменить слова в предложении");
        Console.WriteLine("6. Удалить стоп-слова");
        Console.WriteLine("7. Показать весь текст");
        Console.WriteLine("8. Экспорт в Xml");
        Console.WriteLine("9. Построить конкорданс");
        Console.WriteLine("0. Выход");
        Console.Write("Выберите действие: ");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                ShowSentencesByWordCount();
                break;
            case "2":
                ShowSentencesByLength();
                break;
            case "3":
                ShowWordsInQuestions();
                break;
            case "4":
                RemoveWordsBySMTH();
                break;
            case "5":
                ReplaceWordsInSentence();
                break;
            case "6":
                RemoveStopWords();
                break;
            case "7":
                ShowAllText();
                break;
            case "8":
                ExportToXml();
                break;
            case "9":
                    BuildConcordance();
                break;
                case "0":
                return;
            default:
                Console.WriteLine("Неверный выбор!");
                break;
        }
    }
}

public static void ShowSentencesByWordCount()
{
    var sentences = text.GetSentencesByWordCount();

    foreach (var sentence in sentences)
    {
        Console.WriteLine($"Слов: {sentence.GetWordCount()} - {sentence}");
    }
}

public static void ShowSentencesByLength()
{
    var sentences = text.GetSentencesByLength();

    foreach (var sentence in sentences)
    {
        Console.WriteLine($"Длина: {sentence.Length} - {sentence}");
    }
}

public static void ShowWordsInQuestions()
{
    Console.Write("\nВведите длину слова для поиска: ");
    if (int.TryParse(Console.ReadLine(), out int length))
    {
        var words = text.FindWordsInQuestions(length);

        if (words.Count == 0)
        {
            Console.WriteLine("Слова не найдены");
        }
        else
        {
            foreach (var word in words)
            {
                Console.WriteLine($"- {word.Value}");
            }
        }
    }
    else
    {
        Console.WriteLine("Неверная длина");
    }
}

    public static void RemoveWordsBySMTH()
{
    Console.Write("\nВведите длину слова для удаления: ");
    if (int.TryParse(Console.ReadLine(), out int length))
    {
        text.RemoveWordsWithConsonant(length);
        Console.WriteLine($"Слова длиной {length}, начинающиеся с согласной, удалены");
        ShowAllText();
    }
    else
    {
        Console.WriteLine("Неверная длина");
    }
}

public static void ReplaceWordsInSentence()
{
    Console.Write("Введите номер предложения (с 1): ");
    if (int.TryParse(Console.ReadLine(), out int sentenceIndex))
    {
        Console.Write("Введите длину слова для замены: ");
        if (int.TryParse(Console.ReadLine(), out int wordLength))
        {
            Console.Write("Введите строку для замены: ");
            string replacement = Console.ReadLine();

            text.ReplaceWordsInSentence(sentenceIndex - 1, wordLength, replacement);
            Console.WriteLine("Слова заменены");
            ShowAllText();
        }
        else
        {
            Console.WriteLine("Неверная длина");
        }
    }
    else
    {
        Console.WriteLine("Неверный номер предложения");
    }
}

public static void RemoveStopWords()
{
    if (stopWords.Count == 0)
    {
        Console.WriteLine("Стоп-слова не загружены");
        return;
    }

    text.RemoveStopWords(stopWords);
    Console.WriteLine($"Стоп-слова удалены (использовано {stopWords.Count} слов)");
    ShowAllText();
}

public static void ShowAllText()
{
    for (int i = 0; i < text.Sentences.Count; i++)
    {
        Console.WriteLine($"{text.Sentences[i]}");
    }
}
    public static void ExportToXml()
    {
        Console.Write("Введите имя XML файла (по умолчанию output.xml): ");
        string fileName = Console.ReadLine();

        if (string.IsNullOrEmpty(fileName))
        {
            fileName = "output.xml";
        }

        text.ExportToXml(fileName);
    }
    public static void BuildConcordance()
    {
        string filePath = "TEXT_Lab3.txt";
        text.BuildConcordance(filePath);
    }
}
