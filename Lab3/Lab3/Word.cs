using System.Xml.Serialization;

[XmlRoot("Word")]
public class Word : Token
{

    public Word(string value) : base(value)
    {
    }
    public Word() : base()
    {
    }
}