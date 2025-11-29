using System.Text.RegularExpressions;

namespace BikeshareStats;

public class Filter
{
    private readonly Dictionary<string, string> _cityPatterns = new()
    {
        [@"new york( city)?"]   = "new york city",
        [@"chicago"]            = "chicago",
        [@"washington( city)?"] = "washington"
    };

    private readonly Dictionary<string, string> _monthPatterns = new()
    {
        [@"all( months)?"] = "all",
        [@"jan(uary)?|0?1"] = "january",
        [@"feb(ruary)?|0?2"] = "february",
        [@"mar(ch)?|0?3"] = "march",
        [@"apr(il)?|0?4"] = "april",
        [@"may|0?5"] = "may",
        [@"jun(e)?|0?6"] = "june"
    };

    private readonly Dictionary<string, string> _dayPatterns = new()
    {
        [@"all( days)?"] = "all",
        [@"mon(day)?|0?1"] = "monday",
        [@"tue(sday)?|0?2"] = "tuesday",
        [@"wed(nesday)?|0?3"] = "wednesday",
        [@"thu(rsday)?|0?4"] = "thursday",
        [@"fri(day)?|0?5"] = "friday",
        [@"sat(urday)?|0?6"] = "saturday",
        [@"sun(day)?|0?7"] = "sunday",
    };

    public (string city, string month, string day) GetFilters()
    {
        string city = AskCity();
        string month = AskMonth();
        string day = AskDayOfWeek();

        Console.WriteLine(new string('-', 40));
        return (city, month, day);
    }

    private string AskCity()
    {
        while (true)
        {
            Console.Write("Enter city (chicago, new york city, washington): ");
            var input = (Console.ReadLine() ?? "").Trim().ToLowerInvariant();

            foreach (var kv in _cityPatterns)
            {
                if (Regex.IsMatch(input, $"^{kv.Key}$"))
                    return kv.Value;
            }

            Console.WriteLine(
                $"Oops.. It seems that we don't provide our services in city '{input}'.");
        }
    }

    private string AskMonth()
    {
        while (true)
        {
            Console.Write("Enter month (all, january, february, ...): ");
            var input = (Console.ReadLine() ?? "").Trim().ToLowerInvariant();

            foreach (var kv in _monthPatterns)
            {
                if (Regex.IsMatch(input, $"^{kv.Key}$"))
                    return kv.Value;
            }

            Console.WriteLine($"Oops.. There is no such month as '{input}'. Try again.");
        }
    }

    private string AskDayOfWeek()
    {
        while (true)
        {
            Console.Write("Enter day of week (all, monday, tuesday, ...): ");
            var input = (Console.ReadLine() ?? "").Trim().ToLowerInvariant();

            foreach (var kv in _dayPatterns)
            {
                if (Regex.IsMatch(input, $"^{kv.Key}$"))
                    return kv.Value;
            }

            Console.WriteLine($"Oops.. There is no such day as '{input}'. Try again.");
        }
    }
}
