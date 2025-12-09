using System.Text;
using System.Xml.Serialization;
using System.Xml;

public static class FileManager
{
    public static void SaveSaladToFile(Salad salad, string filePath)
    {
        try
        {
            XmlSalad xmlSalad = new XmlSalad(salad);

            XmlSerializer serializer = new XmlSerializer(typeof(XmlSalad));

            using (TextWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                serializer.Serialize(writer, xmlSalad);
            }

            Console.WriteLine($"Салат успешно сохранен в XML файл: {filePath}");

            Console.WriteLine($"\nИнформация о сохраненном салате:");
            Console.WriteLine($"Название: {salad.Name}");
            Console.WriteLine($"Описание: {salad.Description}");
            Console.WriteLine($"Количество ингредиентов: {salad.GetIngredients().Count}");
            Console.WriteLine($"Общий вес: {salad.CalculateTotalWeight():F1} г");
            Console.WriteLine($"Калорийность: {salad.CalculateTotalCalories():F1} ккал");
            Console.WriteLine($"Стоимость: {salad.CalculateTotalPrice():F1} руб.");
        }
        catch (Exception ex)
        {
            throw new Exception($"Ошибка при сохранении в XML: {ex.Message}");
        }
    }

    public static Salad LoadSaladFromFile(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"XML файл не найден: {filePath}"); 
            }

            if (!filePath.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Внимание: Файл '{filePath}' не имеет расширения .xml. Попытка загрузки...");
            }

            XmlSerializer serializer = new XmlSerializer(typeof(XmlSalad));

            using (TextReader reader = new StreamReader(filePath, Encoding.UTF8))
            {
                XmlSalad xmlSalad = (XmlSalad)serializer.Deserialize(reader);
                Salad salad = xmlSalad.ToSalad();

                Console.WriteLine($"Салат успешно загружен из XML файла: {filePath}");
                Console.WriteLine($"\nИнформация о загруженном салате:");
                Console.WriteLine($"Название: {salad.Name}");
                Console.WriteLine($"Описание: {salad.Description}");
                Console.WriteLine($"Дата создания: {salad.CreatedDate:dd.MM.yyyy HH:mm:ss}");
                Console.WriteLine($"Количество ингредиентов: {salad.GetIngredients().Count}");

                return salad;
            }
        }
        catch (InvalidOperationException ex) when (ex.InnerException is XmlException)
        {
            throw new Exception($"Ошибка формата XML: {ex.InnerException.Message}");
        }
        catch (Exception ex)
        {
            throw new Exception($"Ошибка при загрузке из XML: {ex.Message}");
        }
    }

    public static void ExportAllSaladsToXml(List<Salad> salads, string filePath)
    {
        try
        {
            if (salads.Count == 0)
            {
                throw new InvalidOperationException("Нет салатов для экспорта.");
            }

            XmlSaladCollection collection = new XmlSaladCollection();
            foreach (var salad in salads)
            {
                collection.Salads.Add(new XmlSalad(salad));
            }

            XmlSerializer serializer = new XmlSerializer(typeof(XmlSaladCollection));

            using (TextWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                serializer.Serialize(writer, collection);
            }

            Console.WriteLine($"Все {salads.Count} салатов успешно экспортированы в XML файл: {filePath}");
        }
        catch (Exception ex)
        {
            throw new Exception($"Ошибка при экспорте всех салатов в XML: {ex.Message}");
        }
    }

    public static List<Salad> ImportAllSaladsFromXml(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"XML файл не найден: {filePath}");
            }

            XmlSerializer serializer = new XmlSerializer(typeof(XmlSaladCollection));

            using (TextReader reader = new StreamReader(filePath, Encoding.UTF8))
            {
                XmlSaladCollection collection = (XmlSaladCollection)serializer.Deserialize(reader);

                List<Salad> salads = new List<Salad>();
                foreach (var xmlSalad in collection.Salads)
                {
                    salads.Add(xmlSalad.ToSalad());
                }

                Console.WriteLine($"Успешно загружено {salads.Count} салатов из XML файла: {filePath}");

                return salads;
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Ошибка при импорте салатов из XML: {ex.Message}");
        }
    }
}


[XmlRoot("Salad")]
public class XmlSalad
{
    [XmlElement("Name")]
    public string Name { get; set; }

    [XmlElement("Description")]
    public string Description { get; set; }

    [XmlElement("CreatedDate")]
    public DateTime CreatedDate { get; set; }

    [XmlArray("Ingredients")]
    [XmlArrayItem("Vegetable", Type = typeof(XmlVegetable))]
    [XmlArrayItem("Protein", Type = typeof(XmlProtein))]
    [XmlArrayItem("Dressing", Type = typeof(XmlDressing))]
    [XmlArrayItem("Base", Type = typeof(XmlBase))]
    [XmlArrayItem("Crunchy", Type = typeof(XmlCrunchy))]
    public List<XmlIngredient> Ingredients { get; set; }

    public XmlSalad()
    {
        Ingredients = new List<XmlIngredient>();
    }

    public XmlSalad(Salad salad)
    {
        Name = salad.Name;
        Description = salad.Description;
        CreatedDate = salad.CreatedDate;
        Ingredients = new List<XmlIngredient>();

        foreach (var ingredient in salad.GetIngredients())
        {
            if (ingredient is Vegetable vegetable)
            {
                Ingredients.Add(new XmlVegetable(vegetable));
            }
            else if (ingredient is Protein protein)
            {
                Ingredients.Add(new XmlProtein(protein));
            }
            else if (ingredient is Dressing dressing)
            {
                Ingredients.Add(new XmlDressing(dressing));
            }
            else if (ingredient is Base baseIngredient)
            {
                Ingredients.Add(new XmlBase(baseIngredient));
            }
            else if (ingredient is Crunchy crunchy)
            {
                Ingredients.Add(new XmlCrunchy(crunchy));
            }
        }
    }

    public Salad ToSalad()
    {
        Chef chef = new Chef();
        Salad salad = chef.CreateSalad(Name, Description);

        var saladField = salad.GetType().GetField("CreatedDate",
            System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic |
            System.Reflection.BindingFlags.Public);
        if (saladField != null)
        {
            saladField.SetValue(salad, CreatedDate);
        }
        else
        {
            var property = salad.GetType().GetProperty("CreatedDate");
            if (property != null && property.CanWrite)
            {
                property.SetValue(salad, CreatedDate);
            }
        }

        foreach (var xmlIngredient in Ingredients)
        {
            Ingredient ingredient = xmlIngredient.ToIngredient(chef);
            if (ingredient != null)
            {
                salad.AddIngredient(ingredient);
            }
        }

        return salad;
    }
}

[XmlRoot("Salads")]
public class XmlSaladCollection
{
    [XmlElement("Salad")]
    public List<XmlSalad> Salads { get; set; }

    public XmlSaladCollection()
    {
        Salads = new List<XmlSalad>();
    }
}

[XmlInclude(typeof(XmlVegetable))]
[XmlInclude(typeof(XmlProtein))]
[XmlInclude(typeof(XmlDressing))]
[XmlInclude(typeof(XmlBase))]
[XmlInclude(typeof(XmlCrunchy))]
public abstract class XmlIngredient
{
    [XmlElement("Type")]
    public string Type { get; set; }

    [XmlElement("Name")]
    public string Name { get; set; }

    [XmlElement("Weight")]
    public double Weight { get; set; }

    [XmlElement("Calories")]
    public double Calories { get; set; }

    [XmlElement("Price")]
    public double Price { get; set; }

    public abstract Ingredient ToIngredient(Chef chef);
}

public class XmlVegetable : XmlIngredient
{
    [XmlElement("VegetableType")]
    public string VegetableType { get; set; }

    [XmlElement("Color")]
    public string Color { get; set; }

    [XmlElement("FiberContent")]
    public double FiberContent { get; set; }

    public XmlVegetable() { }

    public XmlVegetable(Vegetable vegetable)
    {
        Type = "Vegetable";
        Name = vegetable.Name;
        Weight = vegetable.Weight;
        Calories = vegetable.Calories;
        Price = vegetable.Price;
        VegetableType = vegetable.VegetableType;
        Color = vegetable.Color;
        FiberContent = vegetable.FiberContent;
    }

    public override Ingredient ToIngredient(Chef chef)
    {
        return chef.CreateVegetable(VegetableType, Name, Color, Weight, Calories, Price, FiberContent);
    }
}

public class XmlProtein : XmlIngredient
{
    [XmlElement("ProteinType")]
    public string ProteinType { get; set; }

    [XmlElement("IsVegetarian")]
    public bool IsVegetarian { get; set; }

    [XmlElement("ProteinContent")]
    public double ProteinContent { get; set; }

    public XmlProtein() { }

    public XmlProtein(Protein protein)
    {
        Type = "Protein";
        Name = protein.Name;
        Weight = protein.Weight;
        Calories = protein.Calories;
        Price = protein.Price;
        ProteinType = protein.ProteinType;
        IsVegetarian = protein.IsVegetarian;
        ProteinContent = protein.ProteinContent;
    }

    public override Ingredient ToIngredient(Chef chef)
    {
        return chef.CreateProtein(ProteinType, Name, IsVegetarian, Weight, Calories, Price, ProteinContent);
    }
}

public class XmlDressing : XmlIngredient
{
    [XmlElement("DressingType")]
    public string DressingType { get; set; }

    [XmlElement("IsCreamy")]
    public bool IsCreamy { get; set; }

    [XmlElement("FatContent")]
    public double FatContent { get; set; }

    public XmlDressing() { }

    public XmlDressing(Dressing dressing)
    {
        Type = "Dressing";
        Name = dressing.Name;
        Weight = dressing.Weight;
        Calories = dressing.Calories;
        Price = dressing.Price;
        DressingType = dressing.DressingType;
        IsCreamy = dressing.IsCreamy;
        FatContent = dressing.FatContent;
    }

    public override Ingredient ToIngredient(Chef chef)
    {
        return chef.CreateDressing(DressingType, Name, IsCreamy, Weight, Calories, Price, FatContent);
    }
}

public class XmlBase : XmlIngredient
{
    [XmlElement("BaseType")]
    public string BaseType { get; set; }

    public XmlBase() { }

    public XmlBase(Base baseIngredient)
    {
        Type = "Base";
        Name = baseIngredient.Name;
        Weight = baseIngredient.Weight;
        Calories = baseIngredient.Calories;
        Price = baseIngredient.Price;
        BaseType = baseIngredient.BaseType;
    }

    public override Ingredient ToIngredient(Chef chef)
    {
        return chef.CreateBase(BaseType, Name, Weight, Calories, Price, true);
    }
}

public class XmlCrunchy : XmlIngredient
{
    [XmlElement("CrunchyType")]
    public string CrunchyType { get; set; }

    [XmlElement("CrunchinessLevel")]
    public double CrunchinessLevel { get; set; }

    [XmlElement("Texture")]
    public string Texture { get; set; }

    public XmlCrunchy() { }

    public XmlCrunchy(Crunchy crunchy)
    {
        Type = "Crunchy";
        Name = crunchy.Name;
        Weight = crunchy.Weight;
        Calories = crunchy.Calories;
        Price = crunchy.Price;
        CrunchyType = crunchy.CrunchyType;
        CrunchinessLevel = crunchy.CrunchinessLevel;
        Texture = crunchy.Texture;
    }

    public override Ingredient ToIngredient(Chef chef)
    {
        return chef.CreateCrunchy(CrunchyType, Name, CrunchinessLevel, Weight, Calories, Price, Texture);
    }
}