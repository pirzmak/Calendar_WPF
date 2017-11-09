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
            List<Event> events = new List<Event>();
            Event eventMock = new Event("New", DateTime.Parse("11-01-2000 10:00"), DateTime.Parse("11-01-2000 12:00"), "");
            var day = new Day(DateTime.Parse("11-01-2000"));
            day.EventsList.Add(eventMock);

            calendarMock.Expect(dao => dao.AllDays.Add(day));

            vm.MyEvent = eventMock;
            vm.OldEvent = true;

            vm.NewTitle = "Newer";

            vm.SaveCommand.Execute(new EventSettings());

            calendarMock.Expect(dao => dao.AllDays[0].EventsList[0].Title).Equals("Newer");
            

            Assert.AreEqual("Newer", vm.CalendarModel.AllDays[0].EventsList[0].Title);
        }
        [TestMethod()]
        public void SaveNewEventInfoTest()
        {
            List<Event> events = new List<Event>();
            Event eventMock = new Event("Old", DateTime.Parse("11-01-2000 10:00"), DateTime.Parse("11-01-2000 12:00"), "");
            var day = new Day(DateTime.Parse("11-01-2000"));
            day.EventsList.Add(eventMock);

            calendarMock.Expect(dao => dao.AllDays.Add(day));

            vm.NewFromH = 10;
            vm.NewFromM = 0;
            vm.NewToH = 12;
            vm.NewToM = 30;
            vm.NewTitle = "New";

            vm.MyEvent = eventMock;
            vm.OldEvent = false;            

            vm.SaveCommand.Execute(new EventSettings());

            calendarMock.Expect(dao => dao.AllDays.Count).Equals(2);

            Assert.IsTrue(vm.CalendarModel.AllDays[0].EventsList[0].Title == "New" ||
                vm.CalendarModel.AllDays[0].EventsList[0].Title == "Old");
        }
        [TestMethod()]
        public void SaveEmptyEventInfoTest()
        {
            List<Event> events = new List<Event>();
            Event eventMock = new Event("Old", DateTime.Parse("11-01-2000 10:00"), DateTime.Parse("11-01-2000 12:00"), "");
            var day = new Day(DateTime.Parse("11-01-2000"));
            day.EventsList.Add(eventMock);

            calendarMock.Expect(dao => dao.AllDays.Add(day));

            vm.NewFromH = 10;
            vm.NewFromM = 0;
            vm.NewToH = 12;
            vm.NewToM = 30;
            vm.NewTitle = "";

            vm.MyEvent = eventMock;
            vm.OldEvent = false;

            vm.SaveCommand.Execute(new EventSettings());

            calendarMock.Expect(dao => dao.AllDays.Count).Equals(1);

            Assert.IsFalse(vm.CalendarModel.AllDays.Count == 2);
        }
        [TestMethod()]
        public void DeleteEventTest()
        {
            List<Event> events = new List<Event>();
            Event eventMock = new Event("New", DateTime.Parse("11-01-2000 10:00"), DateTime.Parse("11-01-2000 12:00"), "");
            var day = new Day(DateTime.Parse("11-01-2000"));
            day.EventsList.Add(eventMock);

            calendarMock.Expect(dao => dao.AllDays.Add(day));

            vm.MyEvent = eventMock;
            vm.OldEvent = true;
            
            vm.DeleteCommand.Execute(new EventSettings());

            calendarMock.Expect(dao => dao.AllDays[0].DeleteEvent(eventMock));

            calendarMock.Expect(dao => dao.AllDays[0].EventsList.Count).Equals(0);


            Assert.AreEqual(0, vm.CalendarModel.AllDays[0].EventsList.Count);
        }
    }
}