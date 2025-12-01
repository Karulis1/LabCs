public class Base : Ingredient
{
    public string BaseType { get; set; }
    public override double CalculateTotalCalories()
    {
        return (Weight / 100) * Calories;
    }
}