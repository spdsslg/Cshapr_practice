using System.Globalization;
using System.Linq;

namespace BikeshareStats;

public class TripRepository
{
    private readonly Dictionary<string, string> _cityFiles;
    private readonly string[] _monthNames;
    
    public TripRepository(Dictionary<string, string> cityFiles, string[] monthNames)
    {
        _cityFiles = cityFiles;
        _monthNames = monthNames;
    }
    
    public List<Trip> LoadTrips(string city, string month, string day)
    {
        var fileName = _cityFiles[city];
        var path = Path.Combine(AppContext.BaseDirectory,"..", "..", "..", "Assets", fileName);

        if (!File.Exists(path))
        {
            Console.WriteLine($"File '{path}' not found.");
            return new List<Trip>();
        }

        var lines = File.ReadLines(path).ToList();
        if (lines.Count <= 1) return new List<Trip>();
        
        var header = lines[0].Split(',')
            .Select(h => h.Trim())
            .ToArray();
        
        int idxStartTime = Array.IndexOf(header, "Start Time");
        int idxEndTime = Array.IndexOf(header, "End Time");
        int idxDuration = Array.IndexOf(header, "Trip Duration");
        int idxStartSta = Array.IndexOf(header, "Start Station");
        int idxEndSta = Array.IndexOf(header, "End Station");
        int idxUserType = Array.IndexOf(header, "User Type");
        int idxGender = Array.IndexOf(header, "Gender");
        int idxBirthYear = Array.IndexOf(header, "Birth Year");
        
        var trips =
            lines
                .Skip(1)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line => ParseTrip(line, idxStartTime, idxEndTime, idxDuration,
                    idxStartSta, idxEndSta, idxUserType,
                    idxGender, idxBirthYear))
                .Where(t => t != null)!
                .ToList()!;

        //filter by month
        if (month != "all")
        {
            int monthNumber = Array.IndexOf(_monthNames, month) + 1;
            trips = trips.Where(t => t.StartTime.Month == monthNumber).ToList();
        }

        //filter by day of week
        if (day != "all")
        {
            var dowWanted = ParseDayOfWeek(day);
            trips = trips.Where(t => t.StartTime.DayOfWeek == dowWanted).ToList();
        }

        return trips;
    }
    

    private Trip? ParseTrip(
        string line,
        int idxStartTime, int idxEndTime, int idxDuration,
        int idxStartSta, int idxEndSta, int idxUserType,
        int idxGender, int idxBirthYear)
    {
        var cols = line.Split(',');

        try
        {
            var start= DateTime.Parse(cols[idxStartTime], CultureInfo.InvariantCulture);
            var end= DateTime.Parse(cols[idxEndTime], CultureInfo.InvariantCulture);
            int duration = int.Parse(cols[idxDuration], CultureInfo.InvariantCulture);

            string startSta = cols[idxStartSta];
            string endSta = cols[idxEndSta];
            string userType = cols[idxUserType];

            string? gender = idxGender >= 0 && idxGender < cols.Length && cols[idxGender] != ""
                ? cols[idxGender]
                : null;

            int? birthYear = null;
            if (idxBirthYear >= 0 && idxBirthYear < cols.Length && cols[idxBirthYear] != "")
            {
                if (int.TryParse(cols[idxBirthYear], out var by))
                    birthYear = by;
            }

            return new Trip
            {
                StartTime = start,
                EndTime = end,
                TripDurationSeconds = duration,
                StartStation = startSta,
                EndStation = endSta,
                UserType = userType,
                Gender = gender,
                BirthYear = birthYear
            };
        }
        catch
        {
            return null;
        }
    }

    private DayOfWeek ParseDayOfWeek(string day) =>
        day.ToLowerInvariant() switch
        {
            "monday" => DayOfWeek.Monday,
            "tuesday" => DayOfWeek.Tuesday,
            "wednesday" => DayOfWeek.Wednesday,
            "thursday" => DayOfWeek.Thursday,
            "friday" => DayOfWeek.Friday,
            "saturday" => DayOfWeek.Saturday,
            "sunday" => DayOfWeek.Sunday,
            _  => throw new ArgumentOutOfRangeException(nameof(day))
        };
}
