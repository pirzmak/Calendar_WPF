using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calendar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Mocks;
using System.ComponentModel;

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
            Day day1 = new Day(DateTime.Parse("11-01-2000"));
            Day day2 = new Day(DateTime.Parse("12-01-2000"));

            day1.AddEvent(new Event("s"));
            day1.AddEvent(new Event("b"));

            day2.AddEvent(new Event("b"));
            
            calendar.AllDays = new List<Day>(new Day[] {day1, day2});

            BindingList<Day> testList = calendar.LoadEvents(DateTime.Parse("11-01-2000"), DateTime.Parse("07-02-2000"));

            Assert.AreEqual(testList.Count, calendar.AllDays.Count, "Alldays in loaded");

            foreach(Day d in calendar.AllDays)
            {
                Assert.IsTrue(testList.Contains(d));
            }
        }
        [TestMethod()]
        public void LoadEventsTest_NotAllInWindow()
        {
            Day day1 = new Day(DateTime.Parse("11-01-2000"));
            Day day2 = new Day(DateTime.Parse("12-03-2000"));

            day1.AddEvent(new Event("s"));
            day1.AddEvent(new Event("b"));

            day2.AddEvent(new Event("b"));

            calendar.AllDays = new List<Day>(new Day[] { day1, day2 });

            BindingList<Day> testList = calendar.LoadEvents(DateTime.Parse("11-01-2000"), DateTime.Parse("07-02-2000"));

            Assert.AreEqual(testList.Count, 1, "Alldays in loaded");
            
            Assert.IsTrue(testList.Contains(day1));
            Assert.IsFalse(testList.Contains(day2));
        }

    }
}