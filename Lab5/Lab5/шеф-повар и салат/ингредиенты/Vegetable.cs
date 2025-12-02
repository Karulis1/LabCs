public class Vegetable : VegetableFruit, ISearchableIngredient
{
    public string VegetableType { get; set; }
    public double FiberContent { get; set; }
    public override string GetInfo()
    {
        return string.Format("{0}{1} - {2}г, {3}, {4} ккал/100г, {5} руб.",
            Name, Weight, Color, Calories, CalculateTotalPrice());
    }
    public bool IsInCalorieRange(double min, double max)
    {
        return this.Calories >= min && this.Calories <= max;
    }
}