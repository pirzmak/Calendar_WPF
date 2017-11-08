using Calendar.Utils;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Calendar.Model
{
    public class Day : NotifyBase
    {
        private DateTime _date;
        private BindingList<Event> _eventsList;        

        public Day(DateTime date)
        {
            this.Date = date;
            EventsList = new BindingList<Event>();
            EventsList = new BindingList<Event>(EventsList.OrderBy(e => e.StartDate.TimeOfDay).ToList());

            EventsList.ListChanged += eventsChanged;
        }

        private void eventsChanged(object sender, ListChangedEventArgs c)
        {            
            OnPropertyChanged("EventsList");
        }

        public DateTime Date
        {
            get { return _date; }
            set { SetProperty(ref _date, value); }
        }      

        public BindingList<Event> EventsList
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
     

        public void addEvent(Event e)
        {
            EventsList.Add(e);
            EventsList = new BindingList<Event>(EventsList.OrderBy(ev => ev.StartDate.TimeOfDay).ToList());
        }

        public void deleteEvent(Event e)
        {
            EventsList.Remove(e);
        }
        
    }
}
