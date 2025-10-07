public enum GameState
{
    Start,
    End
}
public class Game
{
    public int size;
    public Player cat;
    public Player mouse;
    public GameState state;
    private List<string> outputLines;
    private List<string> inputCommands;
    private int currentCommandIndex;

    public static string InputFile = "ChaseData.txt";
    public static string OutputFile = "PursuitLog.txt";

    public Game(int size)
    {
        this.size = size;
        cat = new Player("Cat");
        mouse = new Player("Mouse");
        state = GameState.Start;
        outputLines = new List<string>();
        inputCommands = new List<string>();
        currentCommandIndex = 0;
    }
    public void LoadInputFile()
    {
        try
        {
            if (File.Exists(InputFile))
            {
                inputCommands = new List<string>(File.ReadAllLines(InputFile));
                inputCommands.RemoveAll(string.IsNullOrWhiteSpace);
            }
            else
            {
                throw new FileNotFoundException($"{InputFile} not found");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR: {ex.Message}");
        }
    }

    public void Run()
    {
        LoadInputFile();

        if (inputCommands.Count == 0)
        {
            Console.WriteLine("No commands");
            return;
        }

        if (!int.TryParse(inputCommands[0], out int fileSize))
        {
            Console.WriteLine("ERROR: field size");
            return;
        }

        size = fileSize;
        currentCommandIndex = 1;

        InitializeOutput();

        while (state != GameState.End && currentCommandIndex < inputCommands.Count)
        {
            string commandLine = inputCommands[currentCommandIndex].Trim();
            currentCommandIndex++;

            if (string.IsNullOrEmpty(commandLine))
                continue;

            string[] parts = commandLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0)
                continue;

            char commandType = parts[0][0];

            switch (commandType)
            {
                case 'M':
                case 'C':
                    if (parts.Length >= 2 && int.TryParse(parts[1], out int steps))
                    {
                        DoMoveCommand(commandType, steps);
                        CheckGameEnd();
                    }
                    break;
                case 'P':
                    DoPrintCommand();
                    break;
            }

            if (state == GameState.End)
                break;
        }

        FinalOutput();
        SaveOutputFile();
    }

    private void InitializeOutput()
    {
        outputLines.Clear();
        outputLines.Add("Cat and Mouse");
        outputLines.Add("");
        outputLines.Add("Cat Mouse Distance");
        outputLines.Add("___________________");
    }

    private void DoMoveCommand(char command, int steps)
    {
        switch (command)
        {
            case 'M':
                mouse.Move(steps, size);
                break;
            case 'C':
                cat.Move(steps, size);
                break;
        }
    }

    private void DoPrintCommand()
    {
        string catPos = (cat.state == State.NotInGame) ? "??" : cat.location.ToString();
        string mousePos = (mouse.state == State.NotInGame) ? "??" : mouse.location.ToString();
        string distance = "??";

        if (cat.state != State.NotInGame && mouse.state != State.NotInGame)
        {
            int dist = Math.Abs(cat.location - mouse.location);
            distance = dist.ToString();
        }

        outputLines.Add($"{catPos} {mousePos} {distance}");
    }

    private void CheckGameEnd()
    {
        if (cat.state == State.Playing && mouse.state == State.Playing &&
            cat.location == mouse.location)
        {
            state = GameState.End;
            DoPrintCommand();
        }
    }

    private void FinalOutput()
    {
        outputLines.Add("___________________");
        outputLines.Add("");
        outputLines.Add("");
        outputLines.Add($"Distance traveled: Mouse {mouse.distanceTraveled} Cat {cat.distanceTraveled}");
        outputLines.Add("");

        if (state == GameState.End && cat.location == mouse.location)
        {
            outputLines.Add($"Mouse caught at: {cat.location}");
        }
        else
        {
            outputLines.Add("Mouse evaded Cat");
        }
    }

    private void SaveOutputFile()
    {
        try
        {
            File.WriteAllLines(OutputFile, outputLines);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"save file error: {ex.Message}");
        }
    }

    public void PrintConsole()
    {
        foreach (string line in outputLines)
        {
            Console.WriteLine(line);
        }
    }
}