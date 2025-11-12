namespace PracticeToLab3;

public class TextLog
{
    public event EventHandler? FileChanged;
    public event EventHandler? FileCreated;
    public string FilePath{get;}
    
    protected TextLog(string path) => FilePath = path;

    public static TextLog InitTextLog()
    {
        Console.WriteLine("Enter the path to the file:");
        var path = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException("Path cannot be empty", nameof(path));
        }
        return new TextLog(path);
    }

    public static string GetInput()
    {
        Console.WriteLine("Enter text you want to add to the file or press Enter to exit:");
        string input = "";
        string line;
        while (!string.IsNullOrEmpty(line = Console.ReadLine()))
        {
            input += line+"\n";
        }
        return input;
    }
    
    public void AppendToFile(string content)
    {
        var dir = Path.GetDirectoryName(FilePath);
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        
        if (!File.Exists(FilePath))
        {
            File.Create(FilePath).Close();
            OnFileCreated();
        }
        File.AppendAllText(FilePath, content);
        OnFileChanged();
    }
    protected void OnFileChanged()=>FileChanged?.Invoke(this,EventArgs.Empty);
    protected void OnFileCreated()=>FileCreated?.Invoke(this, EventArgs.Empty);
}