public abstract class VegetableFruit : Ingredient
{
    public string Color { get; set; }
    public override double CalculateTotalCalories()
    {
        return Math.Round((Weight / 100) * Calories, 2);
    }
}