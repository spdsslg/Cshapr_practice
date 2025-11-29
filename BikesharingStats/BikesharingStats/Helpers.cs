using System.Text.RegularExpressions;

namespace BikeshareStats;

public static class Helpers
{
   
    public static bool ReadYesNo(string? prompt = null)
    {
        if (!string.IsNullOrEmpty(prompt))
        {
            Console.Write(prompt);
        }

        var input = Console.ReadLine() ?? string.Empty;
        return Regex.IsMatch(input.Trim(), @"^y(es)?$", RegexOptions.IgnoreCase);
    }
}