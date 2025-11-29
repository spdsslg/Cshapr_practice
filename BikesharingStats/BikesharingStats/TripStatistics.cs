namespace BikeshareStats;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Globalization;

public class TripStatisticsService
{
    public void PrintTimeStats(List<Trip> trips)
    {
        Console.WriteLine("\nCalculating The Most Frequent Times of Travel...\n");
        var sw = Stopwatch.StartNew();

        int commonMonth =
            trips.GroupBy(t => t.StartTime.Month)
                 .OrderByDescending(g => g.Count())
                 .First().Key;
        Console.WriteLine(
            $"Most common month: {CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(commonMonth)}");

        var commonDay =
            trips.GroupBy(t => t.StartTime.DayOfWeek)
                 .OrderByDescending(g => g.Count())
                 .First().Key;
        Console.WriteLine($"Most common day of week: {commonDay}");

        int commonHour =
            trips.GroupBy(t => t.StartTime.Hour)
                 .OrderByDescending(g => g.Count())
                 .First().Key;
        Console.WriteLine($"Most common start hour: {commonHour}");

        sw.Stop();
        Console.WriteLine($"\nThis took {sw.Elapsed.TotalSeconds:F4} seconds.");
        Console.WriteLine(new string('-', 40));
    }

    public void PrintStationStats(List<Trip> trips)
    {
        Console.WriteLine("\nCalculating The Most Popular Stations and Trip...\n");
        var sw = Stopwatch.StartNew();

        string commonStart =
            trips.GroupBy(t => t.StartStation)
                 .OrderByDescending(g => g.Count())
                 .First().Key;
        Console.WriteLine($"Most commonly used start station: {commonStart}");

        string commonEnd =
            trips.GroupBy(t => t.EndStation)
                 .OrderByDescending(g => g.Count())
                 .First().Key;
        Console.WriteLine($"Most commonly used end station: {commonEnd}");

        var commonPair =
            trips.GroupBy(t => new { t.StartStation, t.EndStation })
                 .OrderByDescending(g => g.Count())
                 .First().Key;
        Console.WriteLine(
            $"Most frequent combination: {commonPair.StartStation} --> {commonPair.EndStation}");

        sw.Stop();
        Console.WriteLine($"\nThis took {sw.Elapsed.TotalSeconds:F4} seconds.");
        Console.WriteLine(new string('-', 40));
    }

    public void PrintTripDurationStats(List<Trip> trips)
    {
        Console.WriteLine("\nCalculating Trip Duration...\n");
        var sw = Stopwatch.StartNew();

        long totalSeconds = trips.Sum(t => (long)t.TripDurationSeconds);
        var total = TimeSpan.FromSeconds(totalSeconds);
        var mean  = TimeSpan.FromSeconds(trips.Average(t => t.TripDurationSeconds));

        Console.WriteLine($"Total travel time: {total}");
        Console.WriteLine($"Mean travel time:  {mean}");

        sw.Stop();
        Console.WriteLine($"\nThis took {sw.Elapsed.TotalSeconds:F4} seconds.");
        Console.WriteLine(new string('-', 40));
    }

    public void PrintUserStats(List<Trip> trips)
    {
        Console.WriteLine("\nCalculating User Stats...\n");
        var sw = Stopwatch.StartNew();

        Console.WriteLine("Counts of user types:");
        foreach (var g in trips.GroupBy(t => t.UserType))
        {
            Console.WriteLine($"  {g.Key}: {g.Count()}");
        }
        Console.WriteLine();

        Console.WriteLine("Counts of gender:");
        foreach (var g in trips
                     .Where(t => t.Gender != null)
                     .GroupBy(t => t.Gender))
        {
            Console.WriteLine($"  {g.Key}: {g.Count()}");
        }
        Console.WriteLine();

        var birthYears = trips.Where(t => t.BirthYear.HasValue)
                              .Select(t => t.BirthYear!.Value)
                              .ToList();

        if (birthYears.Any())
        {
            int earliest = birthYears.Min();
            int latest   = birthYears.Max();
            int common   = birthYears
                            .GroupBy(y => y)
                            .OrderByDescending(g => g.Count())
                            .First().Key;
            int mean     = (int)Math.Floor(birthYears.Average());

            Console.WriteLine($"Earliest year of birth: {earliest}");
            Console.WriteLine($"Most recent year of birth: {latest}");
            Console.WriteLine($"Most common year of birth: {common}");
            Console.WriteLine($"Mean year of birth: {mean}");
        }
        else
        {
            Console.WriteLine("No birth-year data available.");
        }

        sw.Stop();
        Console.WriteLine($"\nThis took {sw.Elapsed.TotalSeconds:F4} seconds.");
        Console.WriteLine(new string('-', 40));
    }
}

