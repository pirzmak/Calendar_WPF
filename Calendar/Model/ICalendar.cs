using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Model
{
    public interface ICalendar
    {
        List<Day> AllDays { get; set; }
        BindingList<Day> Days { get; set; }
        BindingList<Day> LoadEvents(DateTime from, DateTime to);
        void DeleteEvent(Event e);
        void AddEvent(Event e);
        void DaysChanged(object sender, ListChangedEventArgs e);
    }
}
