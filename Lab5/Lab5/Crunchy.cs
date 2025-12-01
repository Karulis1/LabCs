public class Crunchy : Ingredient
{
    public double CrunchinessLevel { get; set; }
    public string CrunchyType { get; set; }
    public string Texture { get; set; }

    public override double CalculateTotalCalories()
    {
        return (Weight / 100) * Calories;
    }
}
