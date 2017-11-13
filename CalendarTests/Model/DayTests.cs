using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calendar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

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
            date = DateTime.ParseExact("01-01-2000", "dd-MM-yyyy", CultureInfo.InvariantCulture);
            d = new Day(date);
        }
        [TestMethod()]
        public void DeleteEventTest()
        {
            List<Event> events = new List<Event>();
            Event eventMock = new Event("Old", DateTime.ParseExact("11-01-2000 10:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture), 
                DateTime.ParseExact("11-01-2000 12:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture), "");
            
            d.EventsList.Add(eventMock);

            Assert.IsTrue(d.EventsList.Count == 1);

            d.DeleteEvent(eventMock);

            Assert.IsTrue(d.EventsList.Count == 0);
        }
        [TestMethod()]
        public void AddEventTest()
        {
            List<Event> events = new List<Event>();
            Event eventMock = new Event("Old", DateTime.ParseExact("11-01-2000 10:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture), 
                DateTime.ParseExact("11-01-2000 12:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture), "");

            d.EventsList.Add(eventMock);

            Assert.IsTrue(d.EventsList.Count == 1);

            Event eventMock2 = new Event("Old", DateTime.ParseExact("12-01-2000 10:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture), 
                DateTime.ParseExact("12-01-2000 12:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture), "");

            d.EventsList.Add(eventMock2);

            Assert.IsTrue(d.EventsList.Count == 2);
        }
    }
}