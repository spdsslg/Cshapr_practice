namespace Lab04_practice;

[Info("Oscar Wilde")]
public class Books
{
    public Dictionary<string, bool> Names { get; }
    public string Genre { get; }

    public Books()
    {
        Names = new() {["Portrait of Dorian Gray"]=false, ["De Profundis"]=true};
        Genre = "various";
    }
}

[Info("Iron Maiden")]
public class Songs
{
    public Dictionary<string, bool> Names { get; }
    public string Genre { get; }

    public Songs()
    {
        Names = new(){["Fear of the dark"]=true, ["The trooper"]=false};
        Genre = "Metal";
    }
}

public class Operations
{
    [MathOperation("+")]
    public int Add(int a, int b) => a + b;
    
    [MathOperation("-")]
    public int Subtract(int a, int b) => a - b;
    
    [MathOperation("*")]
    public int Multiply(int a, int b) => a * b;
}


