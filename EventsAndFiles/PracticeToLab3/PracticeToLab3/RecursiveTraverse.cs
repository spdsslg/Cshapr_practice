namespace PracticeToLab3;

public class RecursiveTraverse
{
    public static string GetStartPoint()
    {
        Console.WriteLine("Enter the path to the directory:");
        var path = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException("Path cannot be empty", nameof(path));
        }
        return path;
    }
    
    public static void Print(string rootPath, int depth = 10)
    {
        Print(rootPath, depth, Console.Out);
    }
    
    public static void Print(string rootPath, int depth, TextWriter output)
    {
        if (!Directory.Exists(rootPath))
        {
            Console.WriteLine("Directory doesn't exist");
            return;
        }
        PrintTree(rootPath,0, depth, output);
    }

    private static void PrintIndentation(int n, TextWriter output)
    {
        while (n > 0)
        {
            output.Write(" ");
            n--;
        }
    }

    private static void PrintTree(string path, int depth, int maxDepth, TextWriter output)
    {
        if (depth > maxDepth)
        {
            return;
        }
        
        PrintIndentation(depth, output);
        output.Write("+");
        output.WriteLine(Path.GetFileName(path));
        try
        {
            foreach (var file in Directory.EnumerateFiles(path))
            {
                PrintIndentation(depth + 4,  output);
                output.WriteLine($"-{Path.GetFileName(file)}");
            }
        }
        catch (UnauthorizedAccessException)
        {
            PrintIndentation(depth + 1, output);
            output.WriteLine("[file access denied]");
        }

        try
        {
            foreach (var dir in Directory.EnumerateDirectories(path))
            {
                if ((File.GetAttributes(dir) & FileAttributes.ReparsePoint)!=0)
                {
                    continue;
                }
                PrintTree(dir, depth + 1, maxDepth,  output);
            }
        }
        catch (UnauthorizedAccessException)
        {
            PrintIndentation(depth + 1,  output);
            output.WriteLine("[directory access denied]");
        }
    }
}