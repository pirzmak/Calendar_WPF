using Calendar.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Calendar.Model
{
    public class Day : NotifyBase
    {
        private DateTime _date;
        private List<Event> _eventsList;        

        public Day(DateTime date)
        {
            this.Date = date;
            EventsList = new List<Event>();
            EventsList = new List<Event>(EventsList.OrderBy(e => e.StartDate.TimeOfDay).ToList());            
        }        

        public DateTime Date
        {
            get { return _date; }
            set { SetProperty(ref _date, value); }
        }      

        public List<Event> EventsList
        {
            get { return _eventsList; }
            set { SetProperty(ref _eventsList, value); }
        }

        public String DateWeekNumber
        {
            get
            {
                CultureInfo ciCurr = CultureInfo.CurrentCulture;
                int weekNum = ciCurr.Calendar.GetWeekOfYear(Date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                return "Week " + weekNum.ToString();
            }
        }
     

        public void AddEvent(Event e)
        {
            EventsList.Add(e);
            EventsList = new List<Event>(EventsList.OrderBy(ev => ev.StartDate.TimeOfDay).ToList());
        }

        public void EditEvent(Event e)
        {
            foreach (Event ev in EventsList)
                if (ev.Id.Equals(e.Id))
                    ev.copy(e);
            EventsList = new List<Event>(EventsList.OrderBy(ev => ev.StartDate.TimeOfDay).ToList());
        }

        public void DeleteEvent(Event e)
        {
            EventsList.Remove(e);
            EventsList = new List<Event>(EventsList.OrderBy(ev => ev.StartDate.TimeOfDay).ToList());
        }
        
    }
}
