public class Chef
{
    private List<Salad> createdSalads = new List<Salad>();

    public Salad CreateSalad(string name, string description = "")
    {
        var salad = new Salad { Name = name, Description = description };
        createdSalads.Add(salad);
        return salad;
    }

    public Base CreateBase(string baseType, string name, double weight, double calories, double price, bool isOrganic = false)
    {
        return new Base
        {
            BaseType = baseType,
            Name = name,
            Weight = weight,
            Calories = calories,
            Price = price
        };
    }

    public Vegetable CreateVegetable(string vegetableType, string name, string color, double weight,
                                     double calories, double price, double fiberContent, string plantFamily = "", bool isOrganic = false)
    {
        return new Vegetable
        {
            VegetableType = vegetableType,
            Name = name,
            Color = color,
            Weight = weight,
            Calories = calories,
            Price = price,
            FiberContent = fiberContent,
        };
    }

    public Fruit CreateFruit(string fruitType, string name, string color, double weight,
                            double calories, double price, double sugarContent, string plantFamily = "", bool isOrganic = false)
    {
        return new Fruit
        {
            FruitType = fruitType,
            Name = name,
            Color = color,
            Weight = weight,
            Calories = calories,
            Price = price,
            SugarContent = sugarContent,
        };
    }

    public Protein CreateProtein(string proteinType, string name, bool isVegetarian, double weight,
                                double calories, double price, double proteinContent)
    {
        return new Protein
        {
            ProteinType = proteinType,
            Name = name,
            IsVegetarian = isVegetarian,
            Weight = weight,
            Calories = calories,
            Price = price,
            ProteinContent = proteinContent
        };
    }

    public Crunchy CreateCrunchy(string crunchyType, string name, double crunchinessLevel,
                                double weight, double calories, double price, string texture = "")
    {
        return new Crunchy
        {
            CrunchyType = crunchyType,
            Name = name,
            CrunchinessLevel = crunchinessLevel,
            Weight = weight,
            Calories = calories,
            Price = price,
            Texture = texture
        };
    }

    public Dressing CreateDressing(string dressingType, string name, bool isCreamy,
                                  double weight, double calories, double price, double fatContent)
    {
        return new Dressing
        {
            DressingType = dressingType,
            Name = name,
            IsCreamy = isCreamy,
            Weight = weight,
            Calories = calories,
            Price = price,
            FatContent = fatContent
        };
    }

    public List<Salad> GetAllSalads()
    {
        return new List<Salad>(createdSalads);
    }

    public static List<Salad> LoadRecipesFromFile(string filePath)
    {
        return new List<Salad>();
    }
}