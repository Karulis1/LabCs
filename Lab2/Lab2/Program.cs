namespace CatAndMouse
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Игра Кот и Мышь ===\n");

            Console.Write("Введите размер поля: ");
            int fieldSize = int.Parse(Console.ReadLine());
            Console.WriteLine();

            int catPos = 1;
            int mousePos = fieldSize;

            while (true)
            {
                ShowField(fieldSize);
                Visualize(fieldSize, catPos, mousePos);

                Console.Write("Введите шаги для кота: ");
                string catInput = Console.ReadLine().Trim();

                Console.Write("Введите шаги для мыши: ");
                string mouseInput = Console.ReadLine().Trim();

                if (catInput == "EXIT" || mouseInput == "EXIT") break;

                if (int.TryParse(catInput, out int catSteps) &&
                    int.TryParse(mouseInput, out int mouseSteps))
                {
                    int newCatPos = Move(catPos, catSteps, fieldSize);
                    int newMousePos = Move(mousePos, mouseSteps, fieldSize);

                    catPos = newCatPos;
                    mousePos = newMousePos;

                    if (catPos == mousePos)
                    {
                        ShowField(fieldSize);
                        Visualize(fieldSize, catPos, mousePos);
                        Console.WriteLine($"*** МЫШЬ ПОЙМАНА в клетке {catPos}! ***");
                        break;
                    }

                    Console.WriteLine($"Расстояние: {Math.Abs(catPos - mousePos)} клеток\n");
                }
                else
                {
                    Console.WriteLine("Ошибка! Введите целые числа.\n");
                }
            }
        }

        static int Move(int position, int steps, int fieldSize)
        {
            int newPos = position + steps;
            while (newPos < 1) newPos += fieldSize;
            while (newPos > fieldSize) newPos -= fieldSize;
            return newPos;
        }

        static void ShowField(int fieldSize)
        {
            Console.WriteLine("Поле:");
            for (int i = 1; i <= fieldSize; i++)
            {
                Console.Write($"{i,2} ");
            }
            Console.WriteLine();
        }

        static void Visualize(int fieldSize, int catPos, int mousePos)
        {
            for (int i = 1; i <= fieldSize; i++)
            {
                if (i == catPos && i == mousePos)
                    Console.Write("КМ ");
                else if (i == catPos)
                    Console.Write("К  ");
                else if (i == mousePos)
                    Console.Write("М  ");
                else
                    Console.Write(".  ");
            }
            Console.WriteLine("\n");
        }
    }
}