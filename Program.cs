using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using MoviesChallenge.Models;
using MoviesChallenge.Models.Config;
using MoviesChallenge.Models.Enums;

namespace MoviesChallenge
{
    class Program
    {
        const string APP_SETTINGS_FILENAME = "appsettings.json";
        const string ST_CONFIG = "SetupTimes";
        const string HOO_CONFIG = "HoursOfOperation";
        const string IFH_CONFIG = "InputFileHeaders";
        SetupTimes SetupTimes;
        WeeklyHoursOfOperation WeeklyHoursOfOperation;
        InputMetaData InputMetaData = new InputMetaData();
        List<MovieDataItem> MovieDataItems = new List<MovieDataItem>();

        static void Main(string[] args)
        {
            Program program = new Program();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(APP_SETTINGS_FILENAME, optional: false, reloadOnChange: false);
            IConfiguration configuration = builder.Build();

            /* Retrieve config data */
            program.WeeklyHoursOfOperation = new WeeklyHoursOfOperation(configuration.GetSection(HOO_CONFIG).Get<Dictionary<string, DayHoursOfOperation>>());
            program.SetupTimes = configuration.GetSection(ST_CONFIG).Get<SetupTimes>();
            program.WeeklyHoursOfOperation.ConfigureForSetupTime(Int64.Parse(program.SetupTimes.StartOfDaySetup));

            /* Read input file. */
            using (var inputStream = File.OpenText(args[0]))
            {
                /* Read input column headers from config, read header indicies from top line of input file */
                program.InputMetaData.MapConfigAndInputMetaData(configuration.GetSection(IFH_CONFIG).Get<Dictionary<string, string>>(), inputStream.ReadLine());

                /* Create data object for each movie and calculate showtimes for each day of the week */
                string line;
                while ((line = inputStream.ReadLine()) != null)
                {
                    MovieDataItem dataItem = new MovieDataItem(line, program.InputMetaData);
                    dataItem.CalculateShowtimes(Int16.Parse(program.SetupTimes.PostShowingCleanup), program.WeeklyHoursOfOperation);
                    program.MovieDataItems.Add(dataItem);
                }
            }

            /* Print showtime info for current day */
            DayOfWeek day = DateTime.Today.DayOfWeek;
            Console.WriteLine(day.ToString() + " " + DateTime.Today.ToString("d") + "\n");
            program.MovieDataItems.ForEach(movie =>
            {
                Console.WriteLine(movie.ToString(day));
            });
        }
    }
}
