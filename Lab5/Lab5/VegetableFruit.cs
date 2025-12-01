public abstract class VegetableFruit : Ingredient
{
    public string Color { get; set; }
    public override double CalculateTotalCalories()
    {
        return (Weight / 100) * Calories;
    }
}