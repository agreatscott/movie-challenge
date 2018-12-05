using System;

namespace MoviesChallenge.Models.Config
{
    public class DayHoursOfOperation
    {
        public TimeSpan Open { get; set; }
        public TimeSpan Close { get; set; }
    }
}