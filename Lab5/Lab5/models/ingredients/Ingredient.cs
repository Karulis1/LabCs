public abstract class Ingredient
{
    public string Name { get; set; }
    public double Weight { get; set; }
    public double Calories { get; set; }
    public double Price { get; set; }

    public abstract double CalculateTotalCalories();
    public virtual double CalculateTotalPrice()
    {
        return Math.Round((Weight / 100) * Price, 2);
    }

    public virtual string GetInfo()
    {
        return string.Format("{0} ({1}г) - {2} ккал/100г, {3} руб./100г",
            Name, Weight, Calories, Price);
    }
}
