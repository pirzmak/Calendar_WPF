using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calendar.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calendar.Model;
using Rhino.Mocks;
using Calendar.View;
using System.Globalization;

namespace Calendar.ModelView.Tests
{
    [TestClass()]
    public class EventSettingsWindowViewModelTests
    {
        private ICalendar calendarMock;
        private EventSettingsWindowViewModel vm;
        [TestInitialize()]
        public void Initialize()
        {
            vm = new EventSettingsWindowViewModel();
            calendarMock = MockRepository.GenerateMock<ICalendar>();
            vm.CalendarModel = calendarMock;
        }
        [TestMethod()]
        public void SaveChangeEventInfoTest()
        {
            List<Appointment> events = new List<Appointment>();
            Appointment eventMock = new Appointment{Title="New", StartTime = DateTime.ParseExact("11-01-2000 10:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                EndTime = DateTime.ParseExact("11-01-2000 12:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture)};
            var day = new Day(DateTime.ParseExact("11-01-2000", "dd-MM-yyyy", CultureInfo.InvariantCulture));
            day.AppointmentsList.Add(eventMock);

            calendarMock.Expect(dao => dao.AllDays.Add(day));

            vm.MyAppointment = eventMock;
            vm.OldEvent = true;

            vm.NewTitle = "Newer";

            vm.SaveCommand.Execute(new EventSettings());

            calendarMock.Expect(dao => dao.AllDays[0].AppointmentsList[0].Title).Equals("Newer");
            

            Assert.AreEqual("Newer", vm.CalendarModel.AllDays[0].AppointmentsList[0].Title);
        }
        [TestMethod()]
        public void SaveNewEventInfoTest()
        {
            List<Appointment> events = new List<Appointment>();
            Appointment eventMock = new Appointment
            {
                Title = "New",
                StartTime = DateTime.ParseExact("11-01-2000 10:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                EndTime = DateTime.ParseExact("11-01-2000 12:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture)
            };

            var day = new Day(DateTime.ParseExact("11-01-2000", "dd-MM-yyyy", CultureInfo.InvariantCulture));
            day.AppointmentsList.Add(eventMock);

            calendarMock.Expect(dao => dao.AllDays.Add(day));

            vm.NewFromH = 10;
            vm.NewFromM = 0;
            vm.NewToH = 12;
            vm.NewToM = 30;
            vm.NewTitle = "New";

            vm.MyAppointment = eventMock;
            vm.OldEvent = false;            

            vm.SaveCommand.Execute(new EventSettings());

            calendarMock.Expect(dao => dao.AllDays.Count).Equals(2);

            Assert.IsTrue(vm.CalendarModel.AllDays[0].AppointmentsList[0].Title == "New" ||
                vm.CalendarModel.AllDays[0].AppointmentsList[0].Title == "Old");
        }
        [TestMethod()]
        public void SaveEmptyEventInfoTest()
        {
            List<Appointment> events = new List<Appointment>();
            Appointment eventMock = new Appointment
            {
                Title = "New",
                StartTime = DateTime.ParseExact("11-01-2000 10:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                EndTime = DateTime.ParseExact("11-01-2000 12:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture)
            };

            var day = new Day(DateTime.ParseExact("11-01-2000", "dd-MM-yyyy", CultureInfo.InvariantCulture));
            day.AppointmentsList.Add(eventMock);

            calendarMock.Expect(dao => dao.AllDays.Add(day));

            vm.NewFromH = 10;
            vm.NewFromM = 0;
            vm.NewToH = 12;
            vm.NewToM = 30;
            vm.NewTitle = "";

            vm.MyAppointment = eventMock;
            vm.OldEvent = false;

            vm.SaveCommand.Execute(new EventSettings());

            calendarMock.Expect(dao => dao.AllDays.Count).Equals(1);

            Assert.IsFalse(vm.CalendarModel.AllDays.Count == 2);
        }
        [TestMethod()]
        public void DeleteEventTest()
        {
            List<Appointment> events = new List<Appointment>();
            Appointment eventMock = new Appointment
            {
                Title = "New",
                StartTime = DateTime.ParseExact("11-01-2000 10:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                EndTime = DateTime.ParseExact("11-01-2000 12:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture)
            };

            var day = new Day(DateTime.ParseExact("11-01-2000", "dd-MM-yyyy", CultureInfo.InvariantCulture));
            day.AppointmentsList.Add(eventMock);

            calendarMock.Expect(dao => dao.AllDays.Add(day));

            vm.MyAppointment = eventMock;
            vm.OldEvent = true;
            
            vm.DeleteCommand.Execute(new EventSettings());

            calendarMock.Expect(dao => dao.AllDays[0].DeleteEvent(eventMock));

            calendarMock.Expect(dao => dao.AllDays[0].AppointmentsList.Count).Equals(0);


            Assert.AreEqual(0, vm.CalendarModel.AllDays[0].AppointmentsList.Count);
        }
    }
}