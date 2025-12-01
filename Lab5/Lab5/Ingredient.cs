public abstract class Ingredient
{
    public string Name { get; set; }
    public double Weight { get; set; }
    public double Calories { get; set; }
    public double Price { get; set; }

    public abstract double CalculateTotalCalories();
    public virtual double CalculateTotalPrice()
    {
        return (Weight / 100) * Price;
    }
}
