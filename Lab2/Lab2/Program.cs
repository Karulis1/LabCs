class Program
{
    static void Main(string[] args)
    {
        Game.InputFile = "ChaseData.txt";
        Game.OutputFile = "PursuitLog.txt";
        try
        {
            string[] lines = File.ReadAllLines(Game.InputFile);
            if (lines.Length == 0 || !int.TryParse(lines[0], out int fieldSize))
            {
                Console.WriteLine("ERROR");
                return;
            }
            Game game = new Game(fieldSize);
            game.Run();
            game.PrintConsole();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR: {ex.Message}");
        }
        Console.ReadKey();
    }   
}