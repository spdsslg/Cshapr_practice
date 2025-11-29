namespace MathPlugin;

[AttributeUsage(AttributeTargets.Class)]
public class PluginAttribute : Attribute
{
    public string Name { get; }
    public PluginAttribute(string name)
    {
        Name = name;
    }
}

public class Calculator
{
    public int Add(int a, int b) => a + b;
}

[Plugin("Operations")]
public class SomeOperations
{
    public void DerivativeAt(int a, int b) => Console.WriteLine($"f'({a},{b})");
    public void Derivative() => Console.WriteLine("f'(x,y)");
}