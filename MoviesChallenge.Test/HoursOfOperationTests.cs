using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviesChallenge.Models.Config;

namespace MoviesChallenge.Test.ModelsTests
{
    [TestClass]
    public class HoursOfOperationTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WeeklyHoursOfOperationConstructorThrowsExceptionOnInvalidConfig()
        {
            var HOOParam = new Dictionary<string, DayHoursOfOperation> {
                {"Monday", new DayHoursOfOperation() },
                {"Tuesday", new DayHoursOfOperation() }
            };
            var HOO = new WeeklyHoursOfOperation(HOOParam);
        }

        [TestMethod]
        public void WeeklyHoursOfOperationProperlyConfiguresForSetupTime()
        {
            double setupTime = 30; /* 30 minutes */
            TimeSpan open = TimeSpan.FromMinutes(300); /* 05:00 */
            TimeSpan close = TimeSpan.FromMinutes(1080); /* 18:00 */
            var HOOParam = new Dictionary<string, DayHoursOfOperation> {
                {"Monday", new DayHoursOfOperation { Open = open, Close = close }},
                {"Tuesday", new DayHoursOfOperation { Open = open, Close = close }},
                {"Wednesday", new DayHoursOfOperation() { Open = open, Close = close }},
                {"Thursday", new DayHoursOfOperation() { Open = open, Close = close }},
                {"Friday", new DayHoursOfOperation() { Open = open, Close = close }},
                {"Saturday", new DayHoursOfOperation() { Open = open, Close = close }},
                {"Sunday", new DayHoursOfOperation() { Open = open, Close = close }}
            };
            var weeklyHoursOfOperation = new WeeklyHoursOfOperation(HOOParam);

            weeklyHoursOfOperation.ConfigureForSetupTime(setupTime);

            foreach (KeyValuePair<DayOfWeek, DayHoursOfOperation> entry in weeklyHoursOfOperation.HoursMap) {
                Assert.AreEqual(TimeSpan.FromMinutes(330), entry.Value.Open);
            }
            
        }
    }
}
