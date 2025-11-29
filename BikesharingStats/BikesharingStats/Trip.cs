namespace BikeshareStats;

public class Trip
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int TripDurationSeconds { get; set; }

    public string StartStation { get; set; } = string.Empty;
    public string EndStation   { get; set; } = string.Empty;

    public string UserType { get; set; } = string.Empty;//"Subscriber", "Customer"
    
    public string? Gender { get; set; }
    
    public int? BirthYear { get; set; }
}