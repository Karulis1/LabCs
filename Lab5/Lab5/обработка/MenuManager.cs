namespace Lab5.обработка
{
    internal class MenuManager
    {
        public class SaladMenu
        {
            private Chef chef;
            private List<Salad> salads;

            public SaladMenu()
            {
                chef = new Chef();
                salads = new List<Salad>();
            }

            public void Run()
            {
                Console.WriteLine("ШЕФ-ПОВАР: система создания салатов\n");
                CreatePredefinedSalads();

                while (true)
                {
                    ShowMenu();
                    ProcessSelection();
                }
            }

            private void ShowMenu()
            {
                Console.WriteLine($"Всего салатов в системе: {salads.Count}");
                Console.WriteLine();
                Console.WriteLine("1. Показать информацию о салате");
                Console.WriteLine("2. Показать детальную информацию о салате");
                Console.WriteLine("3. Анализировать салаты");
                Console.WriteLine("4. Сохранить салат в файл");
                Console.WriteLine("5. Создать новый салат");
                Console.WriteLine("6. Показать все салаты");
                Console.WriteLine("7. Удалить салат");
                Console.WriteLine("8. Поиск салата по названию");
                Console.WriteLine("9. Показать салаты отсортированные по калорийности");
                Console.WriteLine("10. Показать салаты отсортированные по стоимости");
                Console.WriteLine("11. Показать салаты отсортированные по дате создания");
                Console.WriteLine("12. Загрузить салаты из файла");
                Console.WriteLine("13. Выйти из программы");
                Console.WriteLine();
                Console.Write("Выберите действие (1-13): ");
            }

            private void ProcessSelection()
            {
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ShowSaladInfo();
                        break;
                    case "2":
                        ShowDetailedSaladInfo();
                        break;
                    case "3":
                        AnalyzeSalads();
                        break;
                    case "4":
                        SaveSaladToFile();
                        break;
                    case "5":
                        CreateCustomSalad();
                        break;
                    case "6":
                        ShowAllSalads();
                        break;
                    case "7":
                        DeleteSalad();
                        break;
                    case "8":
                        SearchSaladByName();
                        break;
                    case "9":
                        ShowSaladsSortedByCalories();
                        break;
                    case "10":
                        ShowSaladsSortedByPrice();
                        break;
                    case "11":
                        ShowSaladsSortedByDate();
                        break;
                    case "12":
                        LoadSaladsFromFile();
                        break;
                    case "13":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }

            private void CreatePredefinedSalads()
            {
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

                salads.Add(greekSalad);

                Salad caesarSalad = chef.CreateSalad("Салат Цезарь",
                    "Классический салат Цезарь с курицей и сухариками");

                caesarSalad.AddIngredient(chef.CreateBase("Листовой", "Салат Романо", 150, 17, 90, true));
                caesarSalad.AddIngredient(chef.CreateProtein("Мясо", "Куриная грудка", false,
                    120, 165, 150, 31));
                caesarSalad.AddIngredient(chef.CreateCrunchy("Сухарики", "Гренки", 8.5,
                    50, 386, 80, "Хрустящие"));
                caesarSalad.AddIngredient(chef.CreateDressing("Кремовая", "Соус Цезарь", true,
                    80, 350, 180, 36));

                salads.Add(caesarSalad);
            }

            private void ShowSaladInfo()
            {
                Console.Clear();

                if (salads.Count == 0)
                {
                    Console.WriteLine("Нет салатов для отображения.");
                    WaitForContinue();
                    return;
                }

                ShowSaladList();
                Console.Write("Введите номер салата для просмотра (0 - отмена): ");

                if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= salads.Count)
                {
                    Salad salad = salads[index - 1];
                    Console.WriteLine($"\nИнформация о салате: {salad.Name}");
                    Console.WriteLine($"Описание: {salad.Description}");
                    Console.WriteLine($"Дата создания: {salad.CreatedDate:dd.MM.yyyy HH:mm:ss}");
                    Console.WriteLine($"Общая калорийность: {salad.CalculateTotalCalories():F1} ккал");
                    Console.WriteLine($"Общая стоимость: {salad.CalculateTotalPrice():F1} руб.");
                    Console.WriteLine($"Общий вес: {salad.CalculateTotalWeight():F1} г");
                    Console.WriteLine($"Количество ингредиентов: {salad.GetIngredients().Count}");
                }
                else if (index != 0)
                {
                    Console.WriteLine("Неверный номер салата.");
                }

                WaitForContinue();
            }

            private void ShowDetailedSaladInfo()
            {
                Console.Clear();

                if (salads.Count == 0)
                {
                    Console.WriteLine("Нет салатов для отображения.");
                    WaitForContinue();
                    return;
                }

                ShowSaladList();
                Console.Write("Введите номер салата для детального просмотра (0 - отмена): ");

                if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= salads.Count)
                {
                    Salad salad = salads[index - 1];
                    salad.PrintSaladInfo();
                }
                else if (index != 0)
                {
                    Console.WriteLine("Неверный номер салата.");
                }

                WaitForContinue();
            }

            private void AnalyzeSalads()
            {
                Console.Clear();
                Console.WriteLine("Анализ салатов:");

                if (salads.Count == 0)
                {
                    Console.WriteLine("Нет салатов для анализа.");
                    WaitForContinue();
                    return;
                }

                Console.WriteLine($"\nОбщая статистика:");
                Console.WriteLine($"Всего салатов: {salads.Count}");
                Console.WriteLine($"Общее количество ингредиентов во всех салатах: {GetTotalIngredientsCount()}");
                Console.WriteLine($"Средняя калорийность салата: {CalculateAverageCalories():F1} ккал");
                Console.WriteLine($"Самый дорогой салат: {GetMostExpensiveSalad()?.Name ?? "нет данных"}");
                Console.WriteLine($"Самый низкокалорийный салат: {GetLowestCalorieSalad()?.Name ?? "нет данных"}");

                Console.WriteLine("\n1. Салаты с овощами, отсортированные по клетчатке:");
                ShowVegetableSaladsSortedByFiber();

                Console.WriteLine("\n2. Поиск овощей в диапазоне калорий:");
                SearchVegetablesInCalorieRange();

                Console.WriteLine("\n3. Сортировка ингредиентов по калорийности:");
                ShowIngredientsSortedByCalories();

                WaitForContinue();
            }

            private int GetTotalIngredientsCount()
            {
                int total = 0;
                foreach (Salad salad in salads)
                {
                    total += salad.GetIngredients().Count;
                }
                return total;
            }

            private double CalculateAverageCalories()
            {
                if (salads.Count == 0) return 0;

                double total = 0;
                foreach (Salad salad in salads)
                {
                    total += salad.CalculateTotalCalories();
                }
                return total / salads.Count;
            }

            private Salad GetMostExpensiveSalad()
            {
                if (salads.Count == 0) return null;

                Salad mostExpensive = salads[0];
                foreach (Salad salad in salads)
                {
                    if (salad.CalculateTotalPrice() > mostExpensive.CalculateTotalPrice())
                    {
                        mostExpensive = salad;
                    }
                }
                return mostExpensive;
            }

            private Salad GetLowestCalorieSalad()
            {
                if (salads.Count == 0) return null;

                Salad lowestCalorie = salads[0];
                foreach (Salad salad in salads)
                {
                    if (salad.CalculateTotalCalories() < lowestCalorie.CalculateTotalCalories())
                    {
                        lowestCalorie = salad;
                    }
                }
                return lowestCalorie;
            }

            private void ShowVegetableSaladsSortedByFiber()
            {
                if (salads.Count == 0) return;

                Console.WriteLine("Введите номер салата для анализа овощей:");
                ShowSaladList();
                Console.Write("Ваш выбор: ");

                if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= salads.Count)
                {
                    Salad salad = salads[index - 1];
                    List<Vegetable> sortedVeggies = salad.SortVegetablesByFiber();

                    if (sortedVeggies.Count > 0)
                    {
                        Console.WriteLine($"\nОвощи в салате '{salad.Name}', отсортированные по клетчатке:");
                        foreach (Vegetable veg in sortedVeggies)
                        {
                            Console.WriteLine($"   - {veg.Name}: {veg.FiberContent:F1}г клетчатки на 100г, {veg.Calories} ккал");
                        }
                    }
                    else
                    {
                        Console.WriteLine("В этом салате нет овощей.");
                    }
                }
            }

            private void SearchVegetablesInCalorieRange()
            {
                if (salads.Count == 0) return;

                Console.WriteLine("Введите номер салата для поиска овощей:");
                ShowSaladList();
                Console.Write("Ваш выбор: ");

                if (int.TryParse(Console.ReadLine(), out int saladIndex) && saladIndex > 0 && saladIndex <= salads.Count)
                {
                    Console.Write("Введите минимальную калорийность: ");
                    double minCal = Convert.ToDouble(Console.ReadLine());

                    Console.Write("Введите максимальную калорийность: ");
                    double maxCal = Convert.ToDouble(Console.ReadLine());

                    Salad salad = salads[saladIndex - 1];
                    List<Vegetable> foundVeggies = salad.FindVegetablesInCalorieRange(minCal, maxCal);

                    if (foundVeggies.Count > 0)
                    {
                        Console.WriteLine($"\nОвощи в салате '{salad.Name}' с калорийностью {minCal}-{maxCal} ккал/100г:");
                        foreach (Vegetable veg in foundVeggies)
                        {
                            Console.WriteLine($"   - {veg.Name}: {veg.Calories} ккал/100г");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Овощей в указанном диапазоне не найдено.");
                    }
                }
            }

            private void ShowIngredientsSortedByCalories()
            {
                if (salads.Count == 0) return;

                Console.WriteLine("Введите номер салата для сортировки ингредиентов:");
                ShowSaladList();
                Console.Write("Ваш выбор: ");

                if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= salads.Count)
                {
                    Salad salad = salads[index - 1];
                    List<Ingredient> sortedIngredients = salad.SortByCalories();

                    if (sortedIngredients.Count > 0)
                    {
                        Console.WriteLine($"\nИнгредиенты салата '{salad.Name}' отсортированные по калорийности:");
                        foreach (Ingredient ing in sortedIngredients)
                        {
                            Console.WriteLine($"   - {ing.Name}: {ing.Calories} ккал/100г");
                        }
                    }
                }
            }

            private void SaveSaladToFile()
            {
                Console.Clear();

                if (salads.Count == 0)
                {
                    Console.WriteLine("Нет салатов для сохранения.");
                    WaitForContinue();
                    return;
                }

                ShowSaladList();
                Console.Write("Введите номер салата для сохранения (0 - отмена): ");

                if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= salads.Count)
                {
                    Salad salad = salads[index - 1];

                    Console.Write("Введите имя файла (например, my_salad.recipe): ");
                    string fileName = Console.ReadLine();

                    try
                    {
                        FileManager.SaveSaladToFile(salad, fileName);
                        Console.WriteLine($"Рецепт сохранен в файл '{fileName}'");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка при сохранении: {ex.Message}");
                    }
                }
                else if (index != 0)
                {
                    Console.WriteLine("Неверный номер салата.");
                }

                WaitForContinue();
            }

            private void CreateCustomSalad()
            {
                Console.Clear();
                Console.WriteLine("Создание нового салата:");

                Console.Write("Введите название салата: ");
                string name = Console.ReadLine();

                Console.Write("Введите описание салата: ");
                string description = Console.ReadLine();

                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description))
                {
                    Console.WriteLine("Название и описание не могут быть пустыми!");
                    WaitForContinue();
                    return;
                }

                Salad newSalad = chef.CreateSalad(name, description);

                bool continueAdding = true;
                while (continueAdding)
                {
                    Console.WriteLine("\nВыберите тип ингредиента:");
                    Console.WriteLine("1. Овощ");
                    Console.WriteLine("2. Заправка");
                    Console.WriteLine("3. Основа (листья)");
                    Console.WriteLine("4. Белок (мясо)");
                    Console.WriteLine("5. Хрустящий элемент");
                    Console.WriteLine("6. Завершить создание салата");
                    Console.Write("Ваш выбор (1-6): ");

                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            AddVegetableToSalad(newSalad);
                            break;
                        case "2":
                            AddDressingToSalad(newSalad);
                            break;
                        case "3":
                            AddBaseToSalad(newSalad);
                            break;
                        case "4":
                            AddProteinToSalad(newSalad);
                            break;
                        case "5":
                            AddCrunchyToSalad(newSalad);
                            break;
                        case "6":
                            continueAdding = false;
                            break;
                        default:
                            Console.WriteLine("Неверный выбор. Попробуйте снова.");
                            break;
                    }
                }

                salads.Add(newSalad);
                Console.WriteLine($"\nСалат '{name}' успешно создан!");
                newSalad.PrintSaladInfo();

                WaitForContinue();
            }

            private void AddVegetableToSalad(Salad salad)
            {
                try
                {
                    Console.Write("Тип овоща: ");
                    string type = Console.ReadLine();

                    Console.Write("Название: ");
                    string name = Console.ReadLine();

                    Console.Write("Цвет: ");
                    string color = Console.ReadLine();

                    Console.Write("Вес (г): ");
                    double weight = Convert.ToDouble(Console.ReadLine());

                    Console.Write("Калории (на 100г): ");
                    double calories = Convert.ToDouble(Console.ReadLine());

                    Console.Write("Стоимость (руб/кг): ");
                    double price = Convert.ToDouble(Console.ReadLine());

                    Console.Write("Содержание клетчатки (г): ");
                    double fiber = Convert.ToDouble(Console.ReadLine());

                    Console.Write("Семейство: ");
                    string family = Console.ReadLine();

                    Console.Write("Свежий (true/false): ");
                    bool isFresh = Convert.ToBoolean(Console.ReadLine());

                    Vegetable vegetable = chef.CreateVegetable(type, name, color, weight, calories,
                        price, fiber, family, isFresh);

                    salad.AddIngredient(vegetable);
                    Console.WriteLine("Овощ успешно добавлен!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }

            private void AddDressingToSalad(Salad salad)
            {
                try
                {
                    Console.Write("Тип заправки: ");
                    string type = Console.ReadLine();

                    Console.Write("Название: ");
                    string name = Console.ReadLine();

                    Console.Write("Содержит молочные продукты (true/false): ");
                    bool hasDairy = Convert.ToBoolean(Console.ReadLine());

                    Console.Write("Количество (мл): ");
                    double amount = Convert.ToDouble(Console.ReadLine());

                    Console.Write("Калории (на 100г): ");
                    double calories = Convert.ToDouble(Console.ReadLine());

                    Console.Write("Стоимость (руб/л): ");
                    double price = Convert.ToDouble(Console.ReadLine());

                    Console.Write("Содержание жира (%): ");
                    double fatContent = Convert.ToDouble(Console.ReadLine());

                    Dressing dressing = chef.CreateDressing(type, name, hasDairy, amount,
                        calories, price, fatContent);

                    salad.AddIngredient(dressing);
                    Console.WriteLine("Заправка успешно добавлена!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }

            private void AddBaseToSalad(Salad salad)
            {
                try
                {
                    Console.Write("Тип основы: ");
                    string type = Console.ReadLine();

                    Console.Write("Название: ");
                    string name = Console.ReadLine();

                    Console.Write("Вес (г): ");
                    double weight = Convert.ToDouble(Console.ReadLine());

                    Console.Write("Калории (на 100г): ");
                    double calories = Convert.ToDouble(Console.ReadLine());

                    Console.Write("Стоимость (руб/кг): ");
                    double price = Convert.ToDouble(Console.ReadLine());

                    Console.Write("Органический (true/false): ");
                    bool isOrganic = Convert.ToBoolean(Console.ReadLine());

                    Base baseIngredient = chef.CreateBase(type, name, weight, calories,
                        price, isOrganic);

                    salad.AddIngredient(baseIngredient);
                    Console.WriteLine("Основа успешно добавлена!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }

            private void AddProteinToSalad(Salad salad)
            {
                try
                {
                    Console.Write("Тип белка: ");
                    string type = Console.ReadLine();

                    Console.Write("Название: ");
                    string name = Console.ReadLine();

                    Console.Write("Жареный (true/false): ");
                    bool isGrilled = Convert.ToBoolean(Console.ReadLine());

                    Console.Write("Вес (г): ");
                    double weight = Convert.ToDouble(Console.ReadLine());

                    Console.Write("Калории (на 100г): ");
                    double calories = Convert.ToDouble(Console.ReadLine());

                    Console.Write("Стоимость (руб/кг): ");
                    double price = Convert.ToDouble(Console.ReadLine());

                    Console.Write("Содержание белка (г): ");
                    double proteinContent = Convert.ToDouble(Console.ReadLine());

                    Protein protein = chef.CreateProtein(type, name, isGrilled, weight,
                        calories, price, proteinContent);

                    salad.AddIngredient(protein);
                    Console.WriteLine("Белок успешно добавлен!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }

            private void AddCrunchyToSalad(Salad salad)
            {
                try
                {
                    Console.Write("Тип: ");
                    string type = Console.ReadLine();

                    Console.Write("Название: ");
                    string name = Console.ReadLine();

                    Console.Write("Размер (см): ");
                    double size = Convert.ToDouble(Console.ReadLine());

                    Console.Write("Вес (г): ");
                    double weight = Convert.ToDouble(Console.ReadLine());

                    Console.Write("Калории (на 100г): ");
                    double calories = Convert.ToDouble(Console.ReadLine());

                    Console.Write("Стоимость (руб/кг): ");
                    double price = Convert.ToDouble(Console.ReadLine());

                    Console.Write("Текстура: ");
                    string texture = Console.ReadLine();

                    Crunchy crunchy = chef.CreateCrunchy(type, name, size, weight,
                        calories, price, texture);

                    salad.AddIngredient(crunchy);
                    Console.WriteLine("Хрустящий элемент успешно добавлен!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }

            private void ShowAllSalads()
            {
                Console.Clear();
                Console.WriteLine("Все салаты:");

                if (salads.Count == 0)
                {
                    Console.WriteLine("Салатов пока нет.");
                }
                else
                {
                    for (int i = 0; i < salads.Count; i++)
                    {
                        Salad salad = salads[i];
                        Console.WriteLine($"\n{i + 1}. {salad.Name}");
                        Console.WriteLine($"   Описание: {salad.Description}");
                        Console.WriteLine($"   Дата создания: {salad.CreatedDate:dd.MM.yyyy HH:mm:ss}");
                        Console.WriteLine($"   Калорийность: {salad.CalculateTotalCalories():F1} ккал");
                        Console.WriteLine($"   Стоимость: {salad.CalculateTotalPrice():F1} руб.");
                        Console.WriteLine($"   Вес: {salad.CalculateTotalWeight():F1} г");
                        Console.WriteLine($"   Количество ингредиентов: {salad.GetIngredients().Count}");
                    }
                }

                WaitForContinue();
            }

            private void DeleteSalad()
            {
                Console.Clear();
                Console.WriteLine("Удаление салата");

                if (salads.Count == 0)
                {
                    Console.WriteLine("Нет салатов для удаления.");
                    WaitForContinue();
                    return;
                }

                ShowSaladList();

                Console.Write("\nВведите номер салата для удаления (0 - отмена): ");

                if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= salads.Count)
                {
                    Salad saladToRemove = salads[index - 1];
                    Console.Write($"Вы уверены, что хотите удалить салат '{saladToRemove.Name}'? (y/n): ");

                    string confirm = Console.ReadLine();
                    if (confirm.ToLower() == "y")
                    {
                        salads.RemoveAt(index - 1);
                        Console.WriteLine("Салат успешно удален!");
                    }
                    else
                    {
                        Console.WriteLine("Удаление отменено.");
                    }
                }
                else if (index != 0)
                {
                    Console.WriteLine("Неверный номер салата.");
                }

                WaitForContinue();
            }

            private void SearchSaladByName()
            {
                Console.Clear();
                Console.WriteLine("Поиск салата по названию");

                Console.Write("Введите часть названия для поиска: ");
                string searchTerm = Console.ReadLine().ToLower();

                List<Salad> foundSalads = new List<Salad>();
                foreach (Salad salad in salads)
                {
                    if (salad.Name.ToLower().Contains(searchTerm))
                    {
                        foundSalads.Add(salad);
                    }
                }

                if (foundSalads.Count > 0)
                {
                    Console.WriteLine($"\nНайдено салатов: {foundSalads.Count}");
                    for (int i = 0; i < foundSalads.Count; i++)
                    {
                        Salad salad = foundSalads[i];
                        Console.WriteLine($"{i + 1}. {salad.Name} - {salad.Description}");
                    }
                }
                else
                {
                    Console.WriteLine("Салаты не найдены.");
                }

                WaitForContinue();
            }

            private void ShowSaladsSortedByCalories()
            {
                Console.Clear();
                Console.WriteLine("Салаты отсортированные по калорийности");

                if (salads.Count == 0)
                {
                    Console.WriteLine("Нет салатов для сортировки.");
                    WaitForContinue();
                    return;
                }

                Console.WriteLine("1. По возрастанию");
                Console.WriteLine("2. По убыванию");
                Console.Write("Выберите вариант сортировки: ");

                string choice = Console.ReadLine();
                bool ascending = choice == "1";

                List<Salad> sortedSalads = new List<Salad>(salads);

                for (int i = 0; i < sortedSalads.Count - 1; i++)
                {
                    for (int j = i + 1; j < sortedSalads.Count; j++)
                    {
                        bool needToSwap = false;

                        if (ascending && sortedSalads[i].CalculateTotalCalories() > sortedSalads[j].CalculateTotalCalories())
                        {
                            needToSwap = true;
                        }
                        else if (!ascending && sortedSalads[i].CalculateTotalCalories() < sortedSalads[j].CalculateTotalCalories())
                        {
                            needToSwap = true;
                        }

                        if (needToSwap)
                        {
                            Salad temp = sortedSalads[i];
                            sortedSalads[i] = sortedSalads[j];
                            sortedSalads[j] = temp;
                        }
                    }
                }

                Console.WriteLine($"\nСалаты отсортированы по калорийности ({GetSortDirectionText(ascending)}):");
                for (int i = 0; i < sortedSalads.Count; i++)
                {
                    Salad salad = sortedSalads[i];
                    Console.WriteLine($"{i + 1}. {salad.Name} - {salad.CalculateTotalCalories():F1} ккал");
                }

                WaitForContinue();
            }

            private void ShowSaladsSortedByPrice()
            {
                Console.Clear();
                Console.WriteLine("Салаты отсортированные по стоимости");

                if (salads.Count == 0)
                {
                    Console.WriteLine("Нет салатов для сортировки.");
                    WaitForContinue();
                    return;
                }

                Console.WriteLine("1. По возрастанию");
                Console.WriteLine("2. По убыванию");
                Console.Write("Выберите вариант сортировки: ");

                string choice = Console.ReadLine();
                bool ascending = choice == "1";

                List<Salad> sortedSalads = new List<Salad>(salads);

                for (int i = 0; i < sortedSalads.Count - 1; i++)
                {
                    for (int j = i + 1; j < sortedSalads.Count; j++)
                    {
                        bool needToSwap = false;

                        if (ascending && sortedSalads[i].CalculateTotalPrice() > sortedSalads[j].CalculateTotalPrice())
                        {
                            needToSwap = true;
                        }
                        else if (!ascending && sortedSalads[i].CalculateTotalPrice() < sortedSalads[j].CalculateTotalPrice())
                        {
                            needToSwap = true;
                        }

                        if (needToSwap)
                        {
                            Salad temp = sortedSalads[i];
                            sortedSalads[i] = sortedSalads[j];
                            sortedSalads[j] = temp;
                        }
                    }
                }

                Console.WriteLine($"\nСалаты отсортированы по стоимости ({GetSortDirectionText(ascending)}):");
                for (int i = 0; i < sortedSalads.Count; i++)
                {
                    Salad salad = sortedSalads[i];
                    Console.WriteLine($"{i + 1}. {salad.Name} - {salad.CalculateTotalPrice():F1} руб.");
                }

                WaitForContinue();
            }

            private void ShowSaladsSortedByDate()
            {
                Console.Clear();
                Console.WriteLine("Салаты отсортированные по дате создания");

                if (salads.Count == 0)
                {
                    Console.WriteLine("Нет салатов для сортировки.");
                    WaitForContinue();
                    return;
                }

                Console.WriteLine("1. От старых к новым");
                Console.WriteLine("2. От новых к старым");
                Console.Write("Выберите вариант сортировки: ");

                string choice = Console.ReadLine();
                bool ascending = choice == "1";

                List<Salad> sortedSalads = new List<Salad>(salads);

                for (int i = 0; i < sortedSalads.Count - 1; i++)
                {
                    for (int j = i + 1; j < sortedSalads.Count; j++)
                    {
                        bool needToSwap = false;

                        if (ascending && sortedSalads[i].CreatedDate > sortedSalads[j].CreatedDate)
                        {
                            needToSwap = true;
                        }
                        else if (!ascending && sortedSalads[i].CreatedDate < sortedSalads[j].CreatedDate)
                        {
                            needToSwap = true;
                        }

                        if (needToSwap)
                        {
                            Salad temp = sortedSalads[i];
                            sortedSalads[i] = sortedSalads[j];
                            sortedSalads[j] = temp;
                        }
                    }
                }

                Console.WriteLine($"\nСалаты отсортированы по дате создания ({GetDateSortDirectionText(ascending)}):");
                for (int i = 0; i < sortedSalads.Count; i++)
                {
                    Salad salad = sortedSalads[i];
                    Console.WriteLine($"{i + 1}. {salad.Name} - {salad.CreatedDate:dd.MM.yyyy HH:mm:ss}");
                }

                WaitForContinue();
            }

            private void LoadSaladsFromFile()
            {
                Console.Clear();
                Console.WriteLine("Загрузка салатов из файла");

                Console.Write("Введите имя файла для загрузки: ");
                string fileName = Console.ReadLine();

                try
                {
                    List<Salad> loadedSalads = Chef.LoadRecipesFromFile(fileName);
                    if (loadedSalads.Count > 0)
                    {
                        salads.AddRange(loadedSalads);
                        Console.WriteLine($"Загружено {loadedSalads.Count} салатов из файла '{fileName}'");
                    }
                    else
                    {
                        Console.WriteLine("В файле не найдено салатов.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при загрузке: {ex.Message}");
                }

                WaitForContinue();
            }

            private void ShowSaladList()
            {
                if (salads.Count == 0)
                {
                    Console.WriteLine("Салатов пока нет.");
                    return;
                }

                Console.WriteLine("\nСписок салатов:");
                for (int i = 0; i < salads.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {salads[i].Name}");
                }
                Console.WriteLine();
            }

            private string GetSortDirectionText(bool ascending)
            {
                return ascending ? "по возрастанию" : "по убыванию";
            }

            private string GetDateSortDirectionText(bool ascending)
            {
                return ascending ? "от старых к новым" : "от новых к старым";
            }

            private void WaitForContinue()
            {
                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}