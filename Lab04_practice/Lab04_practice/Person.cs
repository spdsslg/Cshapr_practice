namespace Lab04_practice;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    
    public void SayHello()=> Console.WriteLine($"Hello, I'm {Name} and my age is {Age}");
    public void Work(string job)=> Console.WriteLine($"Hello, I'm {Name} and my job is {job}");
    public void Work()=> Console.WriteLine($"Hello, I'm {Name} and I'm jobless");
}