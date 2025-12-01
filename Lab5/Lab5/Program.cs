class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("ШЕФ-ПОВАР: система создания салатов\n");

        Chef chef = new Chef();

        Salad greekSalad = chef.CreateSalad("Греческий салат",
            "Классический греческий салат с сыром фета и оливками");

        greekSalad.AddIngredient(chef.CreateVegetable("Плодовый", "Помидоры", "Красный",
            200, 18, 70, 1.2, "Пасленовые", true));
        greekSalad.AddIngredient(chef.CreateVegetable("Плодовый", "Огурцы", "Зеленый",
            150, 15, 40, 0.5, "Тыквенные", true));
        greekSalad.AddIngredient(chef.CreateVegetable("Плодовый", "Сладкий перец", "Желтый",
            100, 27, 60, 1.8, "Пасленовые", true));
        greekSalad.AddIngredient(chef.CreateVegetable("Корнеплод", "Красный лук", "Фиолетовый",
            50, 40, 30, 1.1, "Луковые", false));
        greekSalad.AddIngredient(chef.CreateDressing("Масляная", "Оливковое масло", false,
            30, 884, 200, 100));

        greekSalad.PrintSaladInfo();
        Console.WriteLine();

        Salad caesarSalad = chef.CreateSalad("Салат Цезарь",
            "Классический салат Цезарь с курицей и сухариками");

        caesarSalad.AddIngredient(chef.CreateBase("Листовой", "Салат Романо", 150, 17, 90, true));
        caesarSalad.AddIngredient(chef.CreateProtein("Мясо", "Куриная грудка", false,
            120, 165, 150, 31));
        caesarSalad.AddIngredient(chef.CreateCrunchy("Сухарики", "Гренки", 8.5,
            50, 386, 80, "Хрустящие"));
        caesarSalad.AddIngredient(chef.CreateDressing("Кремовая", "Соус Цезарь", true,
            80, 350, 180, 36));

        Console.WriteLine("\nанализ салатов");

        Console.WriteLine($"Калорийность салата 'Цезарь': {caesarSalad.CalculateTotalCalories():F1} ккал");

        Console.WriteLine("\nОвощи в греческом салате, отсортированные по клетчатке:");
        var sortedVeggies = greekSalad.SortVegetablesByFiber();
        foreach (var veg in sortedVeggies)
        {
            Console.WriteLine($"- {veg.Name}: {veg.FiberContent:F1}г клетчатки на 100г");
        }

        Console.WriteLine("\nОвощи в греческом салате с калорийностью 15-30 ккал/100г:");
        var lowCalVeggies = greekSalad.FindVegetablesInCalorieRange(15, 30);
        foreach (var veg in lowCalVeggies)
        {
            Console.WriteLine($"- {veg.Name}: {veg.Calories} ккал/100г");
        }

        Console.WriteLine($"\nОбщая стоимость греческого салата: {greekSalad.CalculateTotalPrice():F1} руб.");
        Console.WriteLine("\nИнгредиенты салата 'Цезарь' отсортированные по калорийности:");
        var sortedByCalories = caesarSalad.SortByCalories();
        foreach (var ing in sortedByCalories)
        {
            Console.WriteLine($"- {ing.Name}: {ing.Calories} ккал/100г");
        }

        FileManager.SaveSaladToFile(greekSalad, "greek_salad.recipe");
        Console.WriteLine("Рецепт сохранен в файл 'greek_salad.recipe'");

        Console.WriteLine($"\nВсего создано салатов: {chef.GetAllSalads().Count}");
    }
}