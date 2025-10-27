using System.Xml.Serialization;

[XmlRoot("Punctuation")]
public class Punctuation : Token
{
    public Punctuation(string value) : base(value)
    {
    }
    public Punctuation() : base()
    {
    }
}