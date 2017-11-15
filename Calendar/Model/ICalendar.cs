using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Model
{
    public interface ICalendar
    {
        List<Day> AllDays { get; set; }
        ObservableCollection<Day> Days { get; set; }
        ObservableCollection<Day> LoadEvents(DateTime from, DateTime to);
        void EditEvent(Event e);
        void DeleteEvent(Event e);
        void AddEvent(Event e);
    }
}
