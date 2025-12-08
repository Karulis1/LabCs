public class Salad : ISortableIngredient
{
    private List<Ingredient> ingredients = new List<Ingredient>();
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }

    public double Weight
    {
        get { return CalculateTotalWeight(); }
    }

    public double Calories
    {
        get { return CalculateTotalCalories(); }
    }

    public double Price
    {
        get { return CalculateTotalPrice(); }
    }

    public Salad()
    {
        CreatedDate = DateTime.Now;
    }

    public void AddIngredient(Ingredient ingredient)
    {
        ingredients.Add(ingredient);
    }

    public void RemoveIngredient(Ingredient ingredient)
    {
        ingredients.Remove(ingredient);
    }

    public List<Ingredient> GetIngredients()
    {
        List<Ingredient> result = new List<Ingredient>();
        foreach (Ingredient ingredient in ingredients)
        {
            result.Add(ingredient);
        }
        return result;
    }

    public List<Vegetable> GetVegetables()
    {
        List<Vegetable> vegetables = new List<Vegetable>();
        foreach (Ingredient ingredient in ingredients)
        {
            if (ingredient is Vegetable)
            {
                vegetables.Add((Vegetable)ingredient);
            }
        }
        return vegetables;
    }

    public List<Fruit> GetFruits()
    {
        List<Fruit> fruits = new List<Fruit>();
        foreach (Ingredient ingredient in ingredients)
        {
            if (ingredient is Fruit)
            {
                fruits.Add((Fruit)ingredient);
            }
        }
        return fruits;
    }

    public List<Protein> GetProteins()
    {
        List<Protein> proteins = new List<Protein>();
        foreach (Ingredient ingredient in ingredients)
        {
            if (ingredient is Protein)
            {
                proteins.Add((Protein)ingredient);
            }
        }
        return proteins;
    }

    public double CalculateTotalCalories()
    {
        double total = 0;
        foreach (Ingredient ingredient in ingredients)
        {
            total += ingredient.CalculateTotalCalories();
        }
        return total;
    }

    public double CalculateTotalPrice()
    {
        double total = 0;
        foreach (Ingredient ingredient in ingredients)
        {
            total += ingredient.CalculateTotalPrice();
        }
        return total;
    }

    public double CalculateTotalWeight()
    {
        double total = 0;
        foreach (Ingredient ingredient in ingredients)
        {
            total += ingredient.Weight;
        }
        return total;
    }

    public List<Ingredient> SortByCalories(bool ascending = true)
    {
        List<Ingredient> sortedIngredients = new List<Ingredient>();
        foreach (Ingredient ingredient in ingredients)
        {
            sortedIngredients.Add(ingredient);
        }

        for (int i = 0; i < sortedIngredients.Count - 1; i++)
        {
            for (int j = i + 1; j < sortedIngredients.Count; j++)
            {
                bool needToSwap = false;

                if (ascending && sortedIngredients[i].Calories > sortedIngredients[j].Calories)
                {
                    needToSwap = true;
                }
                else if (!ascending && sortedIngredients[i].Calories < sortedIngredients[j].Calories)
                {
                    needToSwap = true;
                }

                if (needToSwap)
                {
                    Ingredient temp = sortedIngredients[i];
                    sortedIngredients[i] = sortedIngredients[j];
                    sortedIngredients[j] = temp;
                }
            }
        }

        return sortedIngredients;
    }

    public List<Ingredient> SortByWeight(bool ascending = true)
    {
        List<Ingredient> sortedIngredients = new List<Ingredient>();
        foreach (Ingredient ingredient in ingredients)
        {
            sortedIngredients.Add(ingredient);
        }

        for (int i = 0; i < sortedIngredients.Count - 1; i++)
        {
            for (int j = i + 1; j < sortedIngredients.Count; j++)
            {
                bool needToSwap = false;

                if (ascending && sortedIngredients[i].Weight > sortedIngredients[j].Weight)
                {
                    needToSwap = true;
                }
                else if (!ascending && sortedIngredients[i].Weight < sortedIngredients[j].Weight)
                {
                    needToSwap = true;
                }

                if (needToSwap)
                {
                    Ingredient temp = sortedIngredients[i];
                    sortedIngredients[i] = sortedIngredients[j];
                    sortedIngredients[j] = temp;
                }
            }
        }

        return sortedIngredients;
    }

    public List<Vegetable> SortVegetablesByFiber(bool ascending = true)
    {
        List<Vegetable> vegetables = GetVegetables();
        List<Vegetable> sortedVegetables = new List<Vegetable>();
        foreach (Vegetable vegetable in vegetables)
        {
            sortedVegetables.Add(vegetable);
        }

        for (int i = 0; i < sortedVegetables.Count - 1; i++)
        {
            for (int j = i + 1; j < sortedVegetables.Count; j++)
            {
                bool needToSwap = false;

                if (ascending && sortedVegetables[i].FiberContent > sortedVegetables[j].FiberContent)
                {
                    needToSwap = true;
                }
                else if (!ascending && sortedVegetables[i].FiberContent < sortedVegetables[j].FiberContent)
                {
                    needToSwap = true;
                }

                if (needToSwap)
                {
                    Vegetable temp = sortedVegetables[i];
                    sortedVegetables[i] = sortedVegetables[j];
                    sortedVegetables[j] = temp;
                }
            }
        }

        return sortedVegetables;
    }

    public List<Vegetable> FindVegetablesInCalorieRange(double minCalories, double maxCalories)
    {
        List<Vegetable> vegetables = GetVegetables();
        List<Vegetable> result = new List<Vegetable>();

        foreach (Vegetable vegetable in vegetables)
        {
            if (vegetable.Calories >= minCalories && vegetable.Calories <= maxCalories)
            {
                result.Add(vegetable);
            }
        }

        return result;
    }

    public void PrintSaladInfo()
    {
        Console.WriteLine($"Салат: {Name}");
        Console.WriteLine($"Описание: {Description}");
        Console.WriteLine($"Дата создания: {CreatedDate.ToString("dd.MM.yyyy HH:mm:ss")}");
        Console.WriteLine($"Общий вес: {CalculateTotalWeight().ToString("F1")}г");
        Console.WriteLine($"Общая калорийность: {CalculateTotalCalories().ToString("F1")} ккал");
        Console.WriteLine($"Общая стоимость: {CalculateTotalPrice().ToString("F1")} руб.");
        Console.WriteLine("Состав:");
        Console.WriteLine("-------------------------");

        foreach (Ingredient ingredient in ingredients)
        {
            Console.WriteLine($"- {ingredient.Name}: {ingredient.Weight}г, {ingredient.Calories} ккал/100г, {ingredient.CalculateTotalPrice().ToString("F1")} руб.");
        }
    }
}