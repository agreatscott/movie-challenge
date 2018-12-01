namespace MoviesChallenge.Models.Config
{
    class WeeklyHoursOfOperation
    {
        public DayHoursOfOperation Monday { get; set; }
        public DayHoursOfOperation Tuesday { get; set; }
        public DayHoursOfOperation Wednesday { get; set; }
        public DayHoursOfOperation Thursday { get; set; }
        public DayHoursOfOperation Friday { get; set; }
        public DayHoursOfOperation Saturday { get; set; }
        public DayHoursOfOperation Sunday { get; set; }
    }
}