namespace BikeshareStats;

public class TripViewer
{
    public void ShowHead(List<Trip> trips)
    {
        if (!Helpers.ReadYesNo("Do you want to see the first 5 rows? (yes/no): "))
            return;

        int current = 0;
        while (current < trips.Count)
        {
            int size = Math.Min(5, trips.Count - current);
            foreach (var trip in trips.Skip(current).Take(size))
            {
                Console.WriteLine("{");
                Console.WriteLine($"  'Start Time'   : '{trip.StartTime}'");
                Console.WriteLine($"  'End Time'     : '{trip.EndTime}'");
                Console.WriteLine($"  'Trip Duration': {trip.TripDurationSeconds}");
                Console.WriteLine($"  'Start Station': '{trip.StartStation}'");
                Console.WriteLine($"  'End Station'  : '{trip.EndStation}'");
                Console.WriteLine($"  'User Type'    : '{trip.UserType}'");
                Console.WriteLine($"  'Gender'       : '{trip.Gender}'");
                Console.WriteLine($"  'Birth Year'   : '{trip.BirthYear}'");
                Console.WriteLine("}");
            }

            current += size;
            if (current >= trips.Count) break;

            if (!Helpers.ReadYesNo(
                    $"...Do you want to see next {Math.Min(5, trips.Count - current)} rows? (yes/no): "))
                break;

            Console.WriteLine(new string('.', 10));
        }
    }
}