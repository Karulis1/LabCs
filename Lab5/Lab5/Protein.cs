public class Protein : Ingredient
{
    public bool IsVegetarian { get; set; }
    public string ProteinType { get; set; }
    public double ProteinContent { get; set; }

    public override double CalculateTotalCalories()
    {
        return (Weight / 100) * Calories;
    }
}