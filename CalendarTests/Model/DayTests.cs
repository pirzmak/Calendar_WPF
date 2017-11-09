using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calendar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Model.Tests
{
    [TestClass()]
    public class DayTests
    {
        private Day d;
        private DateTime date;
        [TestInitialize()]
        public void Initialize()
        {
            date = DateTime.Parse("01-01-2000");
            d = new Day(date);
        }
        [TestMethod()]
        public void DeleteEventTest()
        {
            List<Event> events = new List<Event>();
            Event eventMock = new Event("Old", DateTime.Parse("11-01-2000 10:00"), DateTime.Parse("11-01-2000 12:00"), "");
            
            d.EventsList.Add(eventMock);

            Assert.IsTrue(d.EventsList.Count == 1);

            d.DeleteEvent(eventMock);

            Assert.IsTrue(d.EventsList.Count == 0);
        }
        [TestMethod()]
        public void AddEventTest()
        {
            List<Event> events = new List<Event>();
            Event eventMock = new Event("Old", DateTime.Parse("11-01-2000 10:00"), DateTime.Parse("11-01-2000 12:00"), "");

            d.EventsList.Add(eventMock);

            Assert.IsTrue(d.EventsList.Count == 1);

            Event eventMock2 = new Event("Old", DateTime.Parse("12-01-2000 10:00"), DateTime.Parse("12-01-2000 12:00"), "");

            d.EventsList.Add(eventMock2);

            Assert.IsTrue(d.EventsList.Count == 2);
        }
    }
}