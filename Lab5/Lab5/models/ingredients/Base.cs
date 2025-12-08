public class Base : Ingredient
{
    public string BaseType { get; set; }
    public override double CalculateTotalCalories()
    {
        return Math.Round((Weight / 100) * Calories, 2);
    }
}