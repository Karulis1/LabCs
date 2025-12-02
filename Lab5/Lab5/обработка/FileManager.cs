using System.Text;

public static class FileManager
{
    public static void SaveSaladToFile(Salad salad, string filePath)
    {
        try
        {
            StringBuilder content = new StringBuilder();

            content.AppendLine($"Салат: {salad.Name}");
            content.AppendLine($"Описание: {salad.Description}");
            content.AppendLine($"Дата создания: {salad.CreatedDate:dd.MM.yyyy HH:mm:ss}");
            content.AppendLine($"Общий вес: {salad.CalculateTotalWeight():F1} г");
            content.AppendLine($"Общая калорийность: {salad.CalculateTotalCalories():F1} ккал");
            content.AppendLine($"Общая стоимость: {salad.CalculateTotalPrice():F1} руб.");
            content.AppendLine();
            content.AppendLine("Ингредиенты:");
            content.AppendLine("--------------------------------------------------");

            var ingredients = salad.GetIngredients();
            for (int i = 0; i < ingredients.Count; i++)
            {
                var ingredient = ingredients[i];
                content.AppendLine($"{i + 1}. {ingredient.Name}");
                content.AppendLine($"   Вес: {ingredient.Weight} г");
                content.AppendLine($"   Калорийность: {ingredient.Calories} ккал/100г");
                content.AppendLine($"   Стоимость: {ingredient.CalculateTotalPrice():F1} руб.");

                if (ingredient is Vegetable vegetable)
                {
                    content.AppendLine($"   Тип: Овощ");
                    content.AppendLine($"   Цвет: {vegetable.Color}");
                    content.AppendLine($"   Клетчатка: {vegetable.FiberContent:F1} г");
                }
                else if (ingredient is Protein protein)
                {
                    content.AppendLine($"   Тип: Белок");
                    content.AppendLine($"   Содержание белка: {protein.ProteinContent} г");
                }
                else if (ingredient is Dressing dressing)
                {
                    content.AppendLine($"   Тип: Заправка");
                    content.AppendLine($"   Содержание жира: {dressing.FatContent}%");
                }
                else if (ingredient is Base baseIngredient)
                {
                    content.AppendLine($"   Тип: Основа");
                }
                else if (ingredient is Crunchy crunchy)
                {
                    content.AppendLine($"   Тип: Хрустящий элемент");
                }

                content.AppendLine();
            }

            File.WriteAllText(filePath, content.ToString(), Encoding.UTF8);

            Console.WriteLine($"Салат успешно сохранен в файл: {filePath}");
        }
        catch (Exception ex)
        {
            throw new Exception($"Ошибка при сохранении: {ex.Message}");
        }
    }

    public static Salad LoadSaladFromFile(string filePath)
    {
        return new Salad();
    }
}