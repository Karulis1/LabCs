public class Dressing : Ingredient
{
    public bool IsCreamy { get; set; }
    public string DressingType { get; set; }
    public double FatContent { get; set; }

    public override double CalculateTotalCalories()
    {
        return Math.Round((Weight / 100) * Calories, 2);
    }
}