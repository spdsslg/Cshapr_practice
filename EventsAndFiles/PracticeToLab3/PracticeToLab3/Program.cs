using System.Net.Mime;
using PracticeToLab3;

public class Program
{
    public static void Main()
    {
        //first part
        EventHandler<CounterEventArgs> fun1 = (objectThatInvoked, e) =>
        {
            if (objectThatInvoked is Counter invocationCounter)
            {
                if (e.InitValue)
                {
                    Console.WriteLine($"Object {invocationCounter.Id} value was initially greater than {invocationCounter.Threshold}\n");
                }
                else
                {
                    Console.WriteLine($"Threshold of object {invocationCounter.Id} was reached.\nThe last value before the threshold is {e.Value}\n");
                }
            }
        };
        
        var counterList = new List<Counter>()
        {
            new Counter(5, 0),
            new Counter(10, 12),
            new Counter(15, 0)
        };

        foreach (var counter in counterList)
        {
            counter.ThresholdReached += fun1;
            counter.Check();
        }
        
        for (int i = 0; i < 10; i++)
        {
            counterList[0].Increment(3);
            counterList[1].Increment(2);
            counterList[2].Increment();
        }
        
        foreach (var counter in counterList)
        {
            counter.ThresholdReached -= fun1;
        }
        
        
        //second part (text log)
        TextLog logFile;
        while (true)
        {
            try
            {
                logFile = TextLog.InitTextLog();
                break;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"{e.Message}\nIf you want to continue, press Enter. Otherwise press any other key");
                var line = Console.ReadLine();
                if (!string.IsNullOrEmpty(line)) return;
            }
        }

        EventHandler loggerChanged = (s, e) =>
        {
            if (s is TextLog textLog)
            {
                Console.WriteLine($"File with path {textLog.FilePath} was Changed");
            }
        };

        EventHandler loggerCreated = (s, e) =>
        {
            if (s is TextLog textLog)
            {
                Console.WriteLine($"File with path {textLog.FilePath} Created");
            }
        };
        logFile.FileChanged += loggerChanged;
        logFile.FileCreated += loggerCreated;

        while (true)
        {
            string content = TextLog.GetInput();
            if (string.IsNullOrEmpty(content)) break;
            logFile.AppendToFile(content);
        }
        
        logFile.FileCreated -= loggerCreated;
        logFile.FileChanged -= loggerChanged;
    }
}