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
            List<Appointment> events = new List<Appointment>();
            Appointment eventMock = new Appointment{Title = "Old", StartTime = DateTime.ParseExact("11-01-2000 10:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                EndTime = DateTime.ParseExact("11-01-2000 12:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture)};
            
            d.AppointmentsList.Add(eventMock);

            Assert.IsTrue(d.AppointmentsList.Count == 1);

            d.DeleteEvent(eventMock);

            Assert.IsTrue(d.AppointmentsList.Count == 0);
        }
        [TestMethod()]
        public void AddEventTest()
        {
            List<Appointment> events = new List<Appointment>();
            Appointment eventMock = new Appointment
            {
                Title = "Old",
                StartTime = DateTime.ParseExact("11-01-2000 10:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                EndTime = DateTime.ParseExact("11-01-2000 12:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture)
            };

            d.AppointmentsList.Add(eventMock);

            Assert.IsTrue(d.AppointmentsList.Count == 1);

            Appointment eventMock2 = new Appointment
            {
                Title = "Old",
                StartTime = DateTime.ParseExact("12-01-2000 10:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                EndTime = DateTime.ParseExact("12-01-2000 12:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture)
            };

            d.AppointmentsList.Add(eventMock2);

            Assert.IsTrue(d.AppointmentsList.Count == 2);
        }
    }
}