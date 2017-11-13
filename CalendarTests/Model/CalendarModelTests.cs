using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calendar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Mocks;
using System.ComponentModel;
using System.Globalization;

namespace Calendar.Model.Tests
{
    [TestClass()]
    public class CalendarModelTests
    {
        private CalendarModel calendar;
        [TestInitialize()]
        public void Initialize()
        {
            calendar = new CalendarModel();
        }
        [TestMethod()]
        public void LoadEventsTest_AllInWindow()
        {
            Day day1 = new Day(DateTime.ParseExact("11-01-2000", "dd-MM-yyyy", CultureInfo.InvariantCulture));
            Day day2 = new Day(DateTime.ParseExact("12-01-2000", "dd-MM-yyyy", CultureInfo.InvariantCulture));

            day1.AddEvent(new Event("s"));
            day1.AddEvent(new Event("b"));

            day2.AddEvent(new Event("b"));
            
            calendar.AllDays = new List<Day>(new Day[] {day1, day2});

            List<Day> testList = calendar.LoadEvents(DateTime.ParseExact("11-01-2000", "dd-MM-yyyy", CultureInfo.InvariantCulture),
                DateTime.ParseExact("07-02-2000", "dd-MM-yyyy", CultureInfo.InvariantCulture));

            Assert.AreEqual(testList.Count, calendar.AllDays.Count, "Alldays in loaded");

            foreach(Day d in calendar.AllDays)
            {
                Assert.IsTrue(testList.Contains(d));
            }
        }
        [TestMethod()]
        public void LoadEventsTest_NotAllInWindow()
        {
            Day day1 = new Day(DateTime.ParseExact("11-01-2000", "dd-MM-yyyy", CultureInfo.InvariantCulture));
            Day day2 = new Day(DateTime.ParseExact("12-03-2000", "dd-MM-yyyy", CultureInfo.InvariantCulture));

            day1.AddEvent(new Event("s"));
            day1.AddEvent(new Event("b"));

            day2.AddEvent(new Event("b"));

            calendar.AllDays = new List<Day>(new Day[] { day1, day2 });

            List<Day> testList = calendar.LoadEvents(DateTime.ParseExact("11-01-2000", "dd-MM-yyyy", CultureInfo.InvariantCulture), 
                DateTime.ParseExact("07-02-2000", "dd-MM-yyyy", CultureInfo.InvariantCulture));

            Assert.AreEqual(testList.Count, 1, "Alldays in loaded");
            
            Assert.IsTrue(testList.Contains(day1));
            Assert.IsFalse(testList.Contains(day2));
        }

    }
}