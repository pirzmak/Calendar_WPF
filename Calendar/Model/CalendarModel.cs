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
        public BindingList<Day> Days { get; set; }

        public CalendarModel()
        {
            eventDAO = new EventDAO();
            AllDays = eventDAO.getEvents();
        }

        public BindingList<Day> LoadEvents(DateTime from, DateTime to)
        {
            List<Day> filteredList = AllDays.Where(d => d.Date.Date.CompareTo(from.Date) >= 0 && d.Date.Date.CompareTo(to.Date) <= 0).ToList();
            Days = new BindingList<Day>(filteredList);  
            
            Days.ListChanged += DaysChanged;
            
            return Days;
        }        

        public void DaysChanged(object sender, ListChangedEventArgs e)
        {
            OnPropertyChanged("Days");
        }

        public void DeleteEvent(Event e)
        {
            foreach (Day d in Days)
               if( d.Date.Date.Equals(e.StartDate.Date)) d.DeleteEvent(e);

            foreach (Day d in AllDays)
                if (d.Date.Date.Equals(e.StartDate.Date)) d.DeleteEvent(e);

            eventDAO.deleteEvent(e);
        }

        public void AddEvent(Event e)
        {
            foreach (Day d in Days)
                if (d.Date.Date.Equals(e.StartDate.Date)){ d.AddEvent(e); return; }

            foreach (Day d in AllDays)
                if (d.Date.Date.Equals(e.StartDate.Date)) { d.AddEvent(e); return; }

            Day newDay = new Day(e.StartDate);
            newDay.AddEvent(e);

            Days.Add(newDay);
            AllDays.Add(newDay);

            eventDAO.saveEvent(e);
        }

    }
}