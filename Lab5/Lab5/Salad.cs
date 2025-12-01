public class Salad : ISortableIngredient
{
    private List<Ingredient> ingredients = new List<Ingredient>();
    public string Name { get; set; }
    public string Description { get; set; }

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
        return new List<Ingredient>(ingredients);
    }

    public List<Vegetable> GetVegetables()
    {
        return ingredients.OfType<Vegetable>().ToList();
    }

    public List<Fruit> GetFruits()
    {
        return ingredients.OfType<Fruit>().ToList();
    }

    public List<Protein> GetProteins()
    {
        return ingredients.OfType<Protein>().ToList();
    }

    public double CalculateTotalCalories()
    {
        return ingredients.Sum(i => i.CalculateTotalCalories());
    }

    public double CalculateTotalPrice()
    {
        return ingredients.Sum(i => i.CalculateTotalPrice());
    }

    public double CalculateTotalWeight()
    {
        return ingredients.Sum(i => i.Weight);
    }

    public List<Ingredient> SortByCalories(bool ascending = true)
    {
        return ascending ?
            ingredients.OrderBy(i => i.Calories).ToList() :
            ingredients.OrderByDescending(i => i.Calories).ToList();
    }

    public List<Ingredient> SortByWeight(bool ascending = true)
    {
        return ascending ?
            ingredients.OrderBy(i => i.Weight).ToList() :
            ingredients.OrderByDescending(i => i.Weight).ToList();
    }

    public List<Vegetable> SortVegetablesByFiber(bool ascending = true)
    {
        var vegetables = GetVegetables();
        return ascending ?
            vegetables.OrderBy(v => v.FiberContent).ToList() :
            vegetables.OrderByDescending(v => v.FiberContent).ToList();
    }

    public List<Vegetable> FindVegetablesInCalorieRange(double minCalories, double maxCalories)
    {
        return GetVegetables()
            .Where(v => v.Calories >= minCalories && v.Calories <= maxCalories)
            .ToList();
    }

    public double Weight => CalculateTotalWeight();
    public double Calories => CalculateTotalCalories();
    public double Price => CalculateTotalPrice();

    public void PrintSaladInfo()
    {
        Console.WriteLine($"Салат: {Name}");
        Console.WriteLine($"Описание: {Description}");
        Console.WriteLine($"Общий вес: {CalculateTotalWeight():F1}г");
        Console.WriteLine($"Общая калорийность: {CalculateTotalCalories():F1} ккал");
        Console.WriteLine($"Общая стоимость: {CalculateTotalPrice():F1} руб.");
        Console.WriteLine("Состав:");
        Console.WriteLine("-------------------------");

        foreach (var ingredient in ingredients)
        {
            Console.WriteLine($"- {ingredient.Name}: {ingredient.Weight}г, {ingredient.Calories} ккал/100г, {ingredient.CalculateTotalPrice():F1} руб.");
        }
    }
}