using System.Xml.Serialization;

[XmlInclude(typeof(Word))]
[XmlInclude(typeof(Sentence))]
public abstract class Token
{
    [XmlText]
    private string value;

    protected Token() { }

    public string Value { get; set;}

    protected Token(string value)
    {
        Value = value;
    }
}