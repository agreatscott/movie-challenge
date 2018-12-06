using System;
using System.Collections.Generic;
using MoviesChallenge.Models.Config;
using MoviesChallenge.Models.Enums;

namespace MoviesChallenge.Models
{
    public class MovieDataItem
    {
        private IDictionary<MovieDataField, string> DataMap { get; } = new Dictionary<MovieDataField, string>();
        public IDictionary<DayOfWeek, List<MovieShowtime>> ShowtimesMap { get; } = new Dictionary<DayOfWeek, List<MovieShowtime>>();

        public MovieDataItem(string inputLine, InputMetaData inputMetaData)
        {
            var lineInputArray = inputLine.Split(',');
            for (int i = 0; i < lineInputArray.Length; i++)
            {
                if (i == inputMetaData.getMetaDataValue(MovieDataField.RunTimeMinutes).FieldIndex)
                {
                    //convert time to minutes
                    DataMap[MovieDataField.RunTimeMinutes] = TimeSpan.Parse(lineInputArray[i]).TotalMinutes.ToString();
                }
                else
                {
                    //find the data item whose valueIndex matches i, assign the value to it
                    var movieDataField = inputMetaData.getMovieDataFieldFromIndexValue(i);
                    DataMap[movieDataField] = lineInputArray[i].Trim();
                }
            }
        }

        public void CalculateShowtimes(int postShowingCleanup, WeeklyHoursOfOperation weeklyHoursOfOperation)
        {
            foreach (DayOfWeek weekday in Enum.GetValues(typeof(DayOfWeek)))
            {
                /*  for a weekday, get showtimes 
                    start from the end of the day. get closing time for this day and subtract the runtime off that, then find the first 
                    modular 5 time before that. 
                */
                var showtimesList = new List<MovieShowtime>();
                TimeSpan cleanUpTime = TimeSpan.FromMinutes(postShowingCleanup);
                TimeSpan movieLength = TimeSpan.FromMinutes(Int64.Parse(DataMap[MovieDataField.RunTimeMinutes]));
                TimeSpan endTime = weeklyHoursOfOperation.HoursMap[weekday].Close;

                MovieShowtime newShowtime = ComputeReadableStartTime(endTime, movieLength);
                TimeSpan startTime = newShowtime.start;

                while (startTime > weeklyHoursOfOperation.HoursMap[weekday].Open)
                {
                    showtimesList.Add(newShowtime);

                    /* subtract cleanup time */
                    endTime = startTime - cleanUpTime;

                    /* calculate new showtime */
                    newShowtime = ComputeReadableStartTime(endTime, movieLength);
                    startTime = newShowtime.start;
                }

                ShowtimesMap[weekday] = showtimesList;
            }
        }

        static MovieShowtime ComputeReadableStartTime(TimeSpan endTime, TimeSpan movieLength)
        {
            var startTime = endTime - movieLength;
            /* round the 5 minutes before for readability, adjust end time accordingly */
            startTime = TimeSpan.FromMinutes(5 * Math.Floor(startTime.TotalMinutes / 5));
            endTime = startTime + movieLength;
            return new MovieShowtime { start = startTime, end = endTime };
        }

        public string GetValue(MovieDataField dataField)
        {
            if (dataField == MovieDataField.UnknownOrInvalidField)
            {
                throw new ArgumentException("invalid movie data field");
            }
            return this.DataMap[dataField];
        }

        public void SetValue(MovieDataField dataField, string value)
        {
            if (dataField == MovieDataField.UnknownOrInvalidField)
            {
                throw new ArgumentException("invalid movie data field");
            }
            this.DataMap[dataField] = value;
        }

        public string ToString(DayOfWeek day)
        {
            string ret = DataMap[MovieDataField.MovieTitle] + " - Rated " + DataMap[MovieDataField.Rating] + ", "
                + TimeSpan.FromMinutes(Double.Parse(DataMap[MovieDataField.RunTimeMinutes])).ToString(@"hh\:mm") + "\n";
            List<MovieShowtime> showtimes = this.ShowtimesMap[day];
            for (int i = showtimes.Count - 1; i > -1; i--)
            {
                ret += ("\t" + showtimes[i].start.ToString(@"hh\:mm") + " - " + showtimes[i].end.ToString(@"hh\:mm") + "\n");
            }
            return ret;
        }
    }
}