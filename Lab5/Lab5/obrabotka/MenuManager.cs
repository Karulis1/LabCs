using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab5.обработка
{
    internal class MenuManager
    {
        public class SaladMenu
        {
            private Chef chef;
            private List<Salad> salads;
            private List<Ingredient> predefinedIngredients;

            public SaladMenu()
            {
                chef = new Chef();
                salads = new List<Salad>();
                predefinedIngredients = new List<Ingredient>();
                CreatePredefinedIngredients();
            }

            public void Run()
            {
                Console.Clear();
                Console.WriteLine("Конструктор салатов\n");

                AuthenticateUser();
                CreatePredefinedSalads();
                while (true)
                {
                    ShowMainMenu();
                }
            }

            private void AuthenticateUser()
            {
                bool authenticated = false;

                while (!authenticated)
                {
                    Console.Write("Имя пользователя: ");
                    string username = Console.ReadLine();

                    Console.Write("Пароль: ");
                    string password = ReadPassword();

                    if (Logger.Login(username, password))
                    {
                        authenticated = true;
                        Console.WriteLine($"\n✓ Успешный вход! Добро пожаловать, {username}");

                        if (Logger.IsAdmin())
                        {
                            Console.WriteLine("Режим: АДМИНИСТРАТОР");
                        }

                        WaitForContinue();
                    }
                    else
                    {
                        Console.WriteLine("\nНеверное имя пользователя или пароль.");
                        Console.WriteLine("\nПопробуйте снова...\n");
                    }
                }
            }

            private string ReadPassword()
            {
                string password = "";
                ConsoleKeyInfo key;

                do
                {
                    key = Console.ReadKey(true);

                    if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                    {
                        password += key.KeyChar;
                        Console.Write("*");
                    }
                    else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        password = password.Substring(0, password.Length - 1);
                        Console.Write("\b \b");
                    }
                } while (key.Key != ConsoleKey.Enter);

                Console.WriteLine();
                return password;
            }

            private void ShowMainMenu()
            {
                Console.Clear();
                Console.WriteLine($"Конструктор салатов");
                Console.WriteLine($"Пользователь: {Logger.CurrentUserName}");
                Console.WriteLine($"Режим: {(Logger.IsAdmin() ? "АДМИНИСТРАТОР" : "ЮЗЕР")}");
                Console.WriteLine($"Всего салатов: {salads.Count}");
                Console.WriteLine();
                Console.WriteLine("1. Показать информацию о салате");
                Console.WriteLine("2. Показать детальную информацию о салате");
                Console.WriteLine("3. Анализировать салаты");
                Console.WriteLine("4. Сохранить салат в файл");
                Console.WriteLine("5. Создать новый салат");
                Console.WriteLine("6. Показать все салаты");
                Console.WriteLine("7. Поиск салата по названию");
                Console.WriteLine("8. Показать салаты отсортированные по калорийности");
                Console.WriteLine("9. Показать салаты отсортированные по стоимости");
                Console.WriteLine("10. Показать салаты отсортированные по дате создания");
                Console.WriteLine("11. Загрузить салаты из файла");
                Console.WriteLine("12. Показать овощи в диапазоне калорий");

                if (Logger.IsAdmin())
                {
                    Console.WriteLine("13. Удалить салат");
                    Console.WriteLine("14. Выйти из системы");
                    Console.WriteLine("15. Выйти из программы");
                    Console.Write("\nВыберите действие (1-15): ");
                }
                else
                {
                    Console.WriteLine("13. Выйти из системы");
                    Console.WriteLine("14. Выйти из программы");
                    Console.Write("\nВыберите действие (1-14): ");
                }

                ProcessMenuSelection();
            }

            private void ProcessMenuSelection()
            {
                string input = Console.ReadLine();
                int maxOption = Logger.IsAdmin() ? 15 : 14;

                if (!int.TryParse(input, out int choice) || choice < 1 || choice > maxOption)
                {
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    Logger.LogWarning("Неверный выбор в меню", $"Ввод: {input}");
                    WaitForContinue();
                    return;
                }

                switch (choice)
                {
                    case 1:
                        ShowSaladInfo();
                        break;
                    case 2:
                        ShowDetailedSaladInfo();
                        break;
                    case 3:
                        AnalyzeSalads();
                        break;
                    case 4:
                        SaveSaladToFile();
                        break;
                    case 5:
                        CreateCustomSalad();
                        break;
                    case 6:
                        ShowAllSalads();
                        break;
                    case 7:
                        SearchSaladByName();
                        break;
                    case 8:
                        ShowSaladsSortedByCalories();
                        break;
                    case 9:
                        ShowSaladsSortedByPrice();
                        break;
                    case 10:
                        ShowSaladsSortedByDate();
                        break;
                    case 11:
                        LoadSaladsFromFile();
                        break;
                    case 12:
                        SearchVegetablesByCalorieRange();
                        break;
                    case 13:
                        if (Logger.IsAdmin())
                        {
                            DeleteSalad();
                        }
                        else
                        {
                            LogoutAndReturnToAuth();
                        }
                        break;
                    case 14:
                        if (Logger.IsAdmin())
                        {
                            LogoutAndReturnToAuth();
                        }
                        else
                        {
                            ExitProgram();
                        }
                        break;
                    case 15:
                        if (Logger.IsAdmin())
                        {
                            ExitProgram();
                        }
                        break;
                }
            }

            private void CreatePredefinedIngredients()
            {
                predefinedIngredients.Add(chef.CreateVegetable("Плодовый", "Помидоры", "Красный",
                    100, 18, 70, 1.2, "Пасленовые", true));
                predefinedIngredients.Add(chef.CreateVegetable("Плодовый", "Огурцы", "Зеленый",
                    100, 15, 40, 0.5, "Тыквенные", true));
                predefinedIngredients.Add(chef.CreateVegetable("Плодовый", "Сладкий перец", "Желтый",
                    100, 27, 60, 1.8, "Пасленовые", true));
                predefinedIngredients.Add(chef.CreateVegetable("Корнеплод", "Морковь", "Оранжевый",
                    100, 41, 30, 2.8, "Зонтичные", true));
                predefinedIngredients.Add(chef.CreateVegetable("Корнеплод", "Картофель", "Коричневый",
                    100, 77, 20, 2.2, "Пасленовые", true));
                predefinedIngredients.Add(chef.CreateVegetable("Листовой", "Салат Айсберг", "Зеленый",
                    100, 14, 50, 1.2, "Астровые", true));
                predefinedIngredients.Add(chef.CreateVegetable("Листовой", "Шпинат", "Темно-зеленый",
                    100, 23, 80, 2.2, "Амарантовые", true));

                predefinedIngredients.Add(chef.CreateProtein("Мясо", "Куриная грудка", false,
                    100, 165, 150, 31));
                predefinedIngredients.Add(chef.CreateProtein("Мясо", "Говядина", false,
                    100, 250, 200, 26));
                predefinedIngredients.Add(chef.CreateProtein("Рыба", "Лосось", false,
                    100, 208, 300, 20));
                predefinedIngredients.Add(chef.CreateProtein("Бобовые", "Тофу", true,
                    100, 76, 120, 8));

                predefinedIngredients.Add(chef.CreateDressing("Масляная", "Оливковое масло", false,
                    100, 884, 200, 100));
                predefinedIngredients.Add(chef.CreateDressing("Кремовая", "Майонез", true,
                    100, 680, 150, 75));
                predefinedIngredients.Add(chef.CreateDressing("Кислая", "Бальзамический уксус", false,
                    100, 88, 180, 0));
                predefinedIngredients.Add(chef.CreateDressing("Кремовая", "Сметана", true,
                    100, 206, 80, 20));

                predefinedIngredients.Add(chef.CreateBase("Листовой", "Салат Романо", 100, 17, 90, true));
                predefinedIngredients.Add(chef.CreateBase("Листовой", "Руккола", 100, 25, 120, true));
                predefinedIngredients.Add(chef.CreateBase("Листовой", "Кресс-салат", 100, 32, 110, true));

                predefinedIngredients.Add(chef.CreateCrunchy("Сухарики", "Гренки", 8.5,
                    100, 386, 80, "Хрустящие"));
                predefinedIngredients.Add(chef.CreateCrunchy("Орехи", "Грецкие орехи", 7.5,
                    100, 654, 200, "Хрустящие"));
                predefinedIngredients.Add(chef.CreateCrunchy("Семена", "Семечки подсолнечника", 6.5,
                    100, 584, 150, "Хрустящие"));

                Logger.LogInfo("Созданы предопределенные ингредиенты", $"Количество: {predefinedIngredients.Count}");
            }

            private void CreatePredefinedSalads()
            {
                Salad greekSalad = chef.CreateSalad("Греческий салат",
                    "Классический греческий салат");

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

                Logger.LogInfo("Созданы предопределенные салаты", $"Количество: {salads.Count}");
            }

            private void ShowSaladInfo()
            {
                Console.Clear();
                Logger.LogInfo("Показать информацию о салате", $"Пользователь: {Logger.CurrentUserName}");

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

                    Logger.LogInfo("Просмотр информации о салате",
                        $"Салат: {salad.Name}, Ингредиентов: {salad.GetIngredients().Count}");
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
                Logger.LogInfo("Показать детальную информацию о салате", $"Пользователь: {Logger.CurrentUserName}");

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

                    Logger.LogInfo("Детальный просмотр салата",
                        $"Салат: {salad.Name}, Вес: {salad.CalculateTotalWeight():F1}г, Калории: {salad.CalculateTotalCalories():F1}ккал");
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
                Logger.LogInfo("Анализ салатов", $"Пользователь: {Logger.CurrentUserName}");

                if (salads.Count == 0)
                {
                    Console.WriteLine("Нет салатов для анализа.");
                    WaitForContinue();
                    return;
                }

                Console.WriteLine("Анализ салатов:");

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

                Logger.LogInfo("Проведен анализ салатов",
                    $"Салатов: {salads.Count}, Ингредиентов: {GetTotalIngredientsCount()}");

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
                Console.WriteLine("Сохранение в xml");
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

                    Console.WriteLine($"\nВыбран салат: {salad.Name}");

                    Console.WriteLine("\nВарианты сохранения:");
                    Console.WriteLine("1. Сохранить в указанный файл");
                    Console.WriteLine("2. Сохранить в стандартный файл (рекомендуется)");
                    Console.Write("Выберите вариант (1 или 2): ");

                    string saveOption = Console.ReadLine();
                    string filePath;

                    if (saveOption == "1")
                    {
                        Console.Write("\nВведите имя XML файла (например, my_salad.xml): ");
                        string fileName = Console.ReadLine();

                        if (!fileName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                        {
                            fileName += ".xml";
                            Console.WriteLine($"Файл будет сохранен как: {fileName}");
                        }

                        filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
                    }
                    else
                    {
                        string safeName = MakeFileNameSafe(salad.Name);
                        string fileName = $"{safeName}.xml";
                        filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SavedSalads", fileName);

                        Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                        Console.WriteLine($"\nСалат будет сохранен в: {filePath}");
                    }

                    try
                    {
                        FileManager.SaveSaladToFile(salad, filePath);
                        Console.WriteLine($"\n✓ Салат успешно сохранен!");

                        Logger.LogFileSaved(filePath, salad.Name);

                        FileInfo fileInfo = new FileInfo(filePath);
                        Console.WriteLine($"Размер файла: {fileInfo.Length} байт");
                        Console.WriteLine($"Полный путь: {filePath}");

                        Console.Write("\nПоказать содержимое XML файла? (y/n): ");
                        string showContent = Console.ReadLine();
                        if (showContent.ToLower() == "y")
                        {
                            try
                            {
                                string xmlContent = File.ReadAllText(filePath);
                                Console.WriteLine(xmlContent);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Не удалось прочитать файл: {ex.Message}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\n✗ Ошибка при сохранении: {ex.Message}");
                        Console.WriteLine("Проверьте права доступа к папке и убедитесь, что путь корректен.");
                        Logger.LogError("Ошибка при сохранении салата в файл", ex.Message);
                    }
                }
                else if (index != 0)
                {
                    Console.WriteLine("Неверный номер салата.");
                }

                WaitForContinue();
            }

            private string MakeFileNameSafe(string fileName)
            {
                string invalidChars = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
                foreach (char c in invalidChars)
                {
                    fileName = fileName.Replace(c.ToString(), "_");
                }
                return fileName.Trim();
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
                    Logger.LogWarning("Попытка создать салат без названия или описания",
                        $"Пользователь: {Logger.CurrentUserName}");
                    WaitForContinue();
                    return;
                }

                Salad newSalad = chef.CreateSalad(name, description);

                bool continueAdding = true;
                int ingredientCount = 0;

                while (continueAdding)
                {
                    Console.WriteLine("\nВыберите тип ингредиента:");
                    Console.WriteLine("1. Добавить новый ингредиент");
                    Console.WriteLine("2. Выбрать из предопределенных ингредиентов");
                    Console.WriteLine("3. Завершить создание салата");
                    Console.Write("Ваш выбор (1-3): ");

                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            AddNewIngredientToSalad(newSalad);
                            ingredientCount++;
                            break;
                        case "2":
                            if (AddPredefinedIngredientToSalad(newSalad))
                                ingredientCount++;
                            break;
                        case "3":
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

                // Логируем создание салата
                Logger.LogSaladCreated(name, ingredientCount);

                WaitForContinue();
            }

            private bool AddPredefinedIngredientToSalad(Salad salad)
            {
                Console.WriteLine("\nВыберите категорию ингредиентов:");
                Console.WriteLine("1. Овощи");
                Console.WriteLine("2. Белки");
                Console.WriteLine("3. Заправки");
                Console.WriteLine("4. Основы (листья)");
                Console.WriteLine("5. Хрустящие элементы");
                Console.WriteLine("6. Показать все ингредиенты");
                Console.WriteLine("7. Отмена");
                Console.Write("Ваш выбор (1-7): ");

                string categoryChoice = Console.ReadLine();

                List<Ingredient> filteredIngredients = new List<Ingredient>();

                switch (categoryChoice)
                {
                    case "1":
                        filteredIngredients = predefinedIngredients.Where(i => i is Vegetable).ToList();
                        Console.WriteLine("\nПредопределенные овощи:");
                        break;
                    case "2":
                        filteredIngredients = predefinedIngredients.Where(i => i is Protein).ToList();
                        Console.WriteLine("\nПредопределенные белки:");
                        break;
                    case "3":
                        filteredIngredients = predefinedIngredients.Where(i => i is Dressing).ToList();
                        Console.WriteLine("\nПредопределенные заправки:");
                        break;
                    case "4":
                        filteredIngredients = predefinedIngredients.Where(i => i is Base).ToList();
                        Console.WriteLine("\nПредопределенные основы:");
                        break;
                    case "5":
                        filteredIngredients = predefinedIngredients.Where(i => i is Crunchy).ToList();
                        Console.WriteLine("\nПредопределенные хрустящие элементы:");
                        break;
                    case "6":
                        filteredIngredients = predefinedIngredients;
                        Console.WriteLine("\nВсе предопределенные ингредиенты:");
                        break;
                    case "7":
                        Console.WriteLine("Отмена выбора ингредиента.");
                        return false;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        return false;
                }

                if (filteredIngredients.Count == 0)
                {
                    Console.WriteLine("Нет ингредиентов в выбранной категории.");
                    return false;
                }

                for (int i = 0; i < filteredIngredients.Count; i++)
                {
                    var ingredient = filteredIngredients[i];
                    string type = GetIngredientType(ingredient);
                    Console.WriteLine($"{i + 1}. {ingredient.Name} ({type}) - {ingredient.Calories} ккал/100г, {ingredient.Price} руб/кг");
                }

                Console.Write("\nВыберите номер ингредиента (0 - отмена): ");
                if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= filteredIngredients.Count)
                {
                    var selectedIngredient = filteredIngredients[index - 1];

                    Console.Write($"Введите вес для '{selectedIngredient.Name}' (г): ");
                    if (double.TryParse(Console.ReadLine(), out double weight) && weight > 0)
                    {
                        Ingredient newIngredient = CreateIngredientCopy(selectedIngredient, weight);

                        if (newIngredient != null)
                        {
                            salad.AddIngredient(newIngredient);
                            Console.WriteLine($"Ингредиент '{selectedIngredient.Name}' ({weight}г) добавлен в салат!");
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("Ошибка при создании ингредиента.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Неверный вес. Ингредиент не добавлен.");
                    }
                }
                else if (index != 0)
                {
                    Console.WriteLine("Неверный номер ингредиента.");
                }

                return false;
            }

            private string GetIngredientType(Ingredient ingredient)
            {
                if (ingredient is Vegetable) return "Овощ";
                if (ingredient is Protein) return "Белок";
                if (ingredient is Dressing) return "Заправка";
                if (ingredient is Base) return "Основа";
                if (ingredient is Crunchy) return "Хрустящий элемент";
                return "Неизвестно";
            }

            private Ingredient CreateIngredientCopy(Ingredient original, double weight)
            {
                if (original is Vegetable veg)
                {
                    return chef.CreateVegetable(
                        veg.VegetableType,
                        veg.Name,
                        veg.Color,
                        weight,
                        veg.Calories,
                        veg.Price,
                        veg.FiberContent,
                        "",
                        true
                    );
                }
                else if (original is Protein protein)
                {
                    return chef.CreateProtein(
                        protein.ProteinType,
                        protein.Name,
                        protein.IsVegetarian,
                        weight,
                        protein.Calories,
                        protein.Price,
                        protein.ProteinContent
                    );
                }
                else if (original is Dressing dressing)
                {
                    return chef.CreateDressing(
                        dressing.DressingType,
                        dressing.Name,
                        dressing.IsCreamy,
                        weight,
                        dressing.Calories,
                        dressing.Price,
                        dressing.FatContent
                    );
                }
                else if (original is Base baseIngredient)
                {
                    return chef.CreateBase(
                        baseIngredient.BaseType,
                        baseIngredient.Name,
                        weight,
                        baseIngredient.Calories,
                        baseIngredient.Price,
                        true
                    );
                }
                else if (original is Crunchy crunchy)
                {
                    return chef.CreateCrunchy(
                        crunchy.CrunchyType,
                        crunchy.Name,
                        crunchy.CrunchinessLevel,
                        weight,
                        crunchy.Calories,
                        crunchy.Price,
                        crunchy.Texture
                    );
                }

                return null;
            }

            private void AddNewIngredientToSalad(Salad salad)
            {
                Console.WriteLine("\nВыберите тип ингредиента:");
                Console.WriteLine("1. Овощ");
                Console.WriteLine("2. Заправка");
                Console.WriteLine("3. Основа (листья)");
                Console.WriteLine("4. Белок (мясо)");
                Console.WriteLine("5. Хрустящий элемент");
                Console.WriteLine("6. Отмена");
                Console.Write("Ваш выбор (1-6): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddVegetableToSalad(salad);
                        break;
                    case "2":
                        AddDressingToSalad(salad);
                        break;
                    case "3":
                        AddBaseToSalad(salad);
                        break;
                    case "4":
                        AddProteinToSalad(salad);
                        break;
                    case "5":
                        AddCrunchyToSalad(salad);
                        break;
                    case "6":
                        Console.WriteLine("Отмена создания ингредиента.");
                        break;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }
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
                Logger.LogInfo("Показать все салаты", $"Пользователь: {Logger.CurrentUserName}");

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

                        Logger.LogSaladDeleted(saladToRemove.Name);
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
                Logger.LogInfo("Поиск салата по названию", $"Пользователь: {Logger.CurrentUserName}");

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

                    Logger.LogInfo("Поиск салатов по названию",
                        $"Запрос: '{searchTerm}', Найдено: {foundSalads.Count}");
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
                Logger.LogInfo("Сортировка салатов по калорийности", $"Пользователь: {Logger.CurrentUserName}");

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
                Logger.LogInfo("Сортировка салатов по стоимости", $"Пользователь: {Logger.CurrentUserName}");

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
                Logger.LogInfo("Сортировка салатов по дате", $"Пользователь: {Logger.CurrentUserName}");

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
                    if (!File.Exists(fileName))
                    {
                        Console.WriteLine($"Файл '{fileName}' не найден.");
                        Logger.LogWarning("Попытка загрузки несуществующего файла", $"Файл: {fileName}");
                        WaitForContinue();
                        return;
                    }

                    Salad loadedSalad = FileManager.LoadSaladFromFile(fileName);
                    if (loadedSalad != null)
                    {
                        salads.Add(loadedSalad);
                        Console.WriteLine($"Салат '{loadedSalad.Name}' успешно загружен из файла '{fileName}'");
                        Logger.LogFileLoaded(fileName, 1);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при загрузке: {ex.Message}");
                    Logger.LogError("Ошибка при загрузке салата из файла", ex.Message);
                }

                WaitForContinue();
            }

            private void SearchVegetablesByCalorieRange()
            {
                Console.Clear();
                Console.WriteLine("Поиск овощей по диапазону калорий");
                Console.WriteLine("----------------------------------------------");

                if (salads.Count == 0)
                {
                    Console.WriteLine("Нет салатов для поиска.");
                    WaitForContinue();
                    return;
                }

                Console.WriteLine("\nВыберите салат для поиска овощей:");
                ShowSaladList();
                Console.Write("Ваш выбор (0 - отмена): ");

                if (!int.TryParse(Console.ReadLine(), out int saladIndex) || saladIndex < 1 || saladIndex > salads.Count)
                {
                    if (saladIndex != 0) Console.WriteLine("Неверный номер салата.");
                    WaitForContinue();
                    return;
                }

                Salad selectedSalad = salads[saladIndex - 1];

                List<Vegetable> vegetables = selectedSalad.GetVegetables();

                if (vegetables.Count == 0)
                {
                    Console.WriteLine($"\nВ салате '{selectedSalad.Name}' нет овощей.");
                    WaitForContinue();
                    return;
                }

                Console.Write("\nВведите минимальную калорийность (ккал/100г): ");
                if (!double.TryParse(Console.ReadLine(), out double minCalories))
                {
                    Console.WriteLine("Некорректное значение.");
                    WaitForContinue();
                    return;
                }

                Console.Write("Введите максимальную калорийность (ккал/100г): ");
                if (!double.TryParse(Console.ReadLine(), out double maxCalories))
                {
                    Console.WriteLine("Некорректное значение.");
                    WaitForContinue();
                    return;
                }

                if (minCalories > maxCalories)
                {
                    Console.WriteLine("Минимальное значение не может быть больше максимального.");
                    WaitForContinue();
                    return;
                }

                Console.WriteLine($"\nПоиск овощей в салате '{selectedSalad.Name}'");
                Console.WriteLine($"в диапазоне калорий: {minCalories} - {maxCalories} ккал/100г");
                Console.WriteLine("-----------------------------");

                List<Vegetable> foundVegetables = new List<Vegetable>();

                foreach (Vegetable vegetable in vegetables)
                {
                    if (vegetable is ISearchableIngredient searchableVegetable)
                    {
                        if (searchableVegetable.IsInCalorieRange(minCalories, maxCalories))
                        {
                            foundVegetables.Add(vegetable);
                        }
                    }
                }

                if (foundVegetables.Count > 0)
                {
                    Console.WriteLine($"\nНайдено овощей: {foundVegetables.Count}");
                    Console.WriteLine("----------------------------------------------");

                    for (int i = 0; i < foundVegetables.Count; i++)
                    {
                        Vegetable veg = foundVegetables[i];
                        Console.WriteLine($"{i + 1}. {veg.Name}");
                        Console.WriteLine($"   Тип: {veg.VegetableType}");
                        Console.WriteLine($"   Цвет: {veg.Color}");
                        Console.WriteLine($"   Калорийность: {veg.Calories} ккал/100г");
                        Console.WriteLine($"   Клетчатка: {veg.FiberContent:F1}г/100г");
                        Console.WriteLine($"   Вес в салате: {veg.Weight}г");
                        Console.WriteLine($"   Стоимость: {veg.CalculateTotalPrice():F1} руб.");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("\nОвощей в указанном диапазоне калорий не найдено.");
                }
                if (vegetables.Count > 0)
                {
                    Vegetable minCalVeg = vegetables[0];
                    Vegetable maxCalVeg = vegetables[0];

                    foreach (Vegetable veg in vegetables)
                    {
                        if (veg.Calories < minCalVeg.Calories)
                            minCalVeg = veg;
                        if (veg.Calories > maxCalVeg.Calories)
                            maxCalVeg = veg;
                    }

                    Console.WriteLine($"Овощ с минимальной калорийностью: {minCalVeg.Name} ({minCalVeg.Calories} ккал)");
                    Console.WriteLine($"Овощ с максимальной калорийностью: {maxCalVeg.Name} ({maxCalVeg.Calories} ккал)");
                }

                Logger.LogInfo("Поиск овощей по диапазону калорий",
                    $"Салат: {selectedSalad.Name}, Диапазон: {minCalories}-{maxCalories}, Найдено: {foundVegetables.Count}");

                WaitForContinue();
            }

            private void LogoutAndReturnToAuth()
            {
                Logger.Logout();
                Console.WriteLine("Вы вышли из системы.");
                WaitForContinue();
                AuthenticateUser();
            }

            private void ExitProgram()
            {
                Logger.LogInfo("Выход из программы",
                    $"Пользователь: {Logger.CurrentUserName}, Салатов в системе: {salads.Count}");
                Console.WriteLine("\nСпасибо за использование программы!");
                Environment.Exit(0);
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