public abstract class Token
{
    private string value;
    public string Value { get
        { 
         return value; 
        }
        
        set
        {
            if (value.Length < 2)
                Console.WriteLine("Value < 2");
            else this.value = value;
        } }
    protected Token(string value)
    {
        Value = value;
    }
}