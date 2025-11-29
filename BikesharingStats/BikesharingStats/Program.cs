using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace BikeshareStats
{
    public class Program
    {
        private static readonly Dictionary<string, string> CityFiles = new()
        {
            ["chicago"] = "chicago.csv",
            ["new york city"] = "new_york_city.csv",
            ["washington"] = "washington.csv"
        };

        private static readonly string[] MonthNames =
        {
            "january", "february", "march",
            "april", "may", "june",
            "july", "august", "september",
            "october", "november", "december"
        };

        private static void Main()
        {
            Console.WriteLine("US bikeshare data!");
            
            var filters = new Filter();
            var repository = new TripRepository(CityFiles, MonthNames);
            var statsService = new TripStatisticsService();
            var viewer = new TripViewer();

            while (true)
            {
                //ask user for filters
                var (city, month, day) = filters.GetFilters();

                //load data as List<Trip>
                List<Trip> trips = repository.LoadTrips(city, month, day);

                if (trips.Count == 0)
                {
                    Console.WriteLine("No trips match your filters.");
                }
                else
                {
                    //print statistics
                    statsService.PrintTimeStats(trips);
                    statsService.PrintStationStats(trips);
                    statsService.PrintTripDurationStats(trips);
                    statsService.PrintUserStats(trips);
                    
                    viewer.ShowHead(trips);
                }
                if (!Helpers.ReadYesNo(
                        "\nWould you like to restart? Enter yes or no: "))
                {
                    break;
                }

                Console.WriteLine(new string('-', 40));
            }
        }
    }
}