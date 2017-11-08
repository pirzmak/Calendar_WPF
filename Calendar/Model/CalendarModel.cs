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
    public class CalendarModel : NotifyBase
    {
        private List<Day> allDays;
        public BindingList<Day> Days { get; set; }

        public CalendarModel()
        {
            allDays = new List<Day>();
        }

        public BindingList<Day> loadEvents(DateTime from, DateTime to)
        {
            List<Day> filteredList = allDays.Where(d => d.Date.Date.CompareTo(from.Date) >= 0 && d.Date.Date.CompareTo(to.Date) <= 0).ToList();
            Days = new BindingList<Day>(filteredList);  
            
            Days.ListChanged += daysChanged;
            
            return Days;
        }        

        private void daysChanged(object sender, ListChangedEventArgs e)
        {
            OnPropertyChanged("Days");
        }

        public void deleteEvent(Event e)
        {
            foreach (Day d in Days)
               if( d.Date.Date.Equals(e.StartDate.Date)) d.deleteEvent(e);

            foreach (Day d in allDays)
                if (d.Date.Date.Equals(e.StartDate.Date)) d.deleteEvent(e);
        }

        public void addEvent(Event e)
        {
            foreach (Day d in Days)
                if (d.Date.Date.Equals(e.StartDate.Date)){ d.addEvent(e); return; }

            foreach (Day d in allDays)
                if (d.Date.Date.Equals(e.StartDate.Date)) { d.addEvent(e); return; }

            Day newDay = new Day(e.StartDate);
            newDay.addEvent(e);

            Days.Add(newDay);
            allDays.Add(newDay);
        }

    }
}