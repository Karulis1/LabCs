using System.Xml.Serialization;

[XmlInclude(typeof(Word))]
[XmlInclude(typeof(Sentence))]
public abstract class Token
{
    [XmlText]
    private string value;

    protected Token() { }

    public string Value { get
        { 
         return value; 
        }

        set
        {
            if (value.Length < 2) { 
            this.value = value;
        }
            else this.value = value;
        } }

    protected Token(string value)
    {
        Value = value;
    }
}