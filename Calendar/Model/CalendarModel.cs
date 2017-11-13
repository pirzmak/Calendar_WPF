using Calendar.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace Calendar.Model
{
    public class CalendarModel : NotifyBase, ICalendar
    {
        private EventDAO eventDAO;
        public List<Day> AllDays { get; set; }
        public List<Day> Days { get; set; }

        public CalendarModel()
        {
            eventDAO = new EventDAO();
            AllDays = eventDAO.getEvents();
            AllDays.ForEach(d => d.EventsList = d.EventsList.OrderBy(e => e.StartDate).ToList());
        }

        public List<Day> LoadEvents(DateTime from, DateTime to)
        {
            List<Day> filteredList = AllDays.Where(d => d.Date.Date.CompareTo(from.Date) >= 0 && d.Date.Date.CompareTo(to.Date) <= 0).ToList();
            Days = new List<Day>(filteredList);              
            
            return Days;
        }        
        
        public void DeleteEvent(Event e)
        {
            foreach (Day d in AllDays)
                if (d.Date.Date.Equals(e.StartDate.Date)) d.DeleteEvent(e);

            eventDAO.deleteEvent(e);
        }

        public void EditEvent(Event e)
        {
            foreach (Day d in AllDays)
                if (d.Date.Date.Equals(e.StartDate.Date)) d.EditEvent(e);

            eventDAO.ChangeEvent(e);
        }

        public void AddEvent(Event e)
        {
            foreach (Day d in AllDays)
                if (d.Date.Date.Equals(e.StartDate.Date)) { d.AddEvent(e); eventDAO.saveEvent(e); return; }

            Day newDay = new Day(e.StartDate);
            newDay.AddEvent(e);
            
            AllDays.Add(newDay);

            eventDAO.saveEvent(e);
        }

    }
}