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
        private Storage storage;
        public List<Day> AllDays { get; set; }
        public ObservableCollection<Day> Days { get; set; }

        public CalendarModel()
        {
            storage = new Storage();
            List<Appointment> appointments = storage.getAppointments();
            AllDays = getDays(appointments);
            AllDays.ForEach(d => d.AppointmentsList = d.AppointmentsList.OrderBy(e => e.StartTime).ToList());
        }

        private List<Day> getDays(List<Appointment> appointments)
        {
            List<Day> days = new List<Day>();

            foreach (Appointment e in appointments)
            {
                if (days.Exists(d => e.AppointmentDate.Date.Equals(d.Date)))
                    days.Find(d => e.AppointmentDate.Date.Equals(d.Date)).AppointmentsList.Add(e);
                else
                {
                    Day newDay = new Day(e.StartTime.Date);
                    newDay.AddEvent(e);
                    days.Add(newDay);
                }
            }

            return days;
        }

        public ObservableCollection<Day> LoadEvents(DateTime from, DateTime to)
        {
            List<Day> filteredList = AllDays.Where(d => d.Date.Date.CompareTo(from.Date) >= 0 && d.Date.Date.CompareTo(to.Date) <= 0).ToList();
            Days = new ObservableCollection<Day>(filteredList);              
            
            return Days;
        }        
        
        public void DeleteEvent(Appointment e)
        {
            foreach (Day d in AllDays)
                if (d.Date.Date.Equals(e.AppointmentDate.Date)) d.DeleteEvent(e);

            storage.deleteAppointment(e);
        }

        public void EditEvent(Appointment e)
        {
            foreach (Day d in AllDays)
                if (d.Date.Date.Equals(e.AppointmentDate.Date)) d.EditEvent(e);

            storage.updateAppointment(e);
        }

        public void AddEvent(Appointment e)
        {
            foreach (Day d in AllDays)
                if (d.Date.Date.Equals(e.AppointmentDate.Date)) { d.AddEvent(e); storage.createAppointment(e.Title, e.StartTime, e.EndTime); return; }

            Day newDay = new Day(e.AppointmentDate);
            newDay.AddEvent(e);

            AllDays.Add(newDay);
            Days.Add(newDay);

            storage.createAppointment(e.Title, e.StartTime, e.EndTime);
        }

    }
}