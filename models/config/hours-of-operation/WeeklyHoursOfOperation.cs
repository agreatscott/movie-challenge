using System;
using System.Collections.Generic;
using MoviesChallenge.Models.Enums;

namespace MoviesChallenge.Models.Config
{
    class WeeklyHoursOfOperation
    {
        public IDictionary<DayOfWeek, DayHoursOfOperation> HoursMap { get; } = new Dictionary<DayOfWeek, DayHoursOfOperation>();

        public WeeklyHoursOfOperation(IDictionary<string, DayHoursOfOperation> configInput)
        {
            if (configInput.Count != 7)
            {
                throw new ArgumentException("Invalid config");
            }
            foreach (string key in configInput.Keys)
            {
                Enum.TryParse(key, out DayOfWeek weekday);
                HoursMap.Add(weekday, configInput[key]);
            }
        }

        public void ConfigureForSetupTime(double startOfDaySetup)
        {
            TimeSpan startOfDaySetupMins = TimeSpan.FromMinutes(startOfDaySetup);
            foreach (KeyValuePair<DayOfWeek, DayHoursOfOperation> entry in HoursMap)
            {
                entry.Value.Open += startOfDaySetupMins;
            }
        }
    }
}