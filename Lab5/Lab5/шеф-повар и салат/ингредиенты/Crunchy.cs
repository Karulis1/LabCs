public class Crunchy : Ingredient
{
    public double CrunchinessLevel { get; set; }
    public string CrunchyType { get; set; }
    public string Texture { get; set; }

    public override double CalculateTotalCalories()
    {
        return Math.Round((Weight / 100) * Calories, 2);
    }
    public override string GetInfo()
    {
        return string.Format("{0} - {1}г, хрусткость: {2}/10, {3} руб.",
            Name, Weight, CrunchinessLevel, CalculateTotalPrice());
    }
}
