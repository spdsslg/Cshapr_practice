namespace Lab04_practice;

[AttributeUsage(AttributeTargets.Class)]
public class InfoAttribute : Attribute
{
    public string Author { get; }
    public InfoAttribute(string author)=>Author = author;
}


public class MathOperationAttribute : Attribute
{
    public string Symbol { get; }
    public MathOperationAttribute(string symbol)
    {
        Symbol = symbol;
    }
}