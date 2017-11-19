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
        private List<Appointment> _appointmentsList;        

        public Day(DateTime date)
        {
            this.Date = date;
            AppointmentsList = new List<Appointment>();
            AppointmentsList = new List<Appointment>(AppointmentsList.OrderBy(e => e.StartTime.TimeOfDay).ToList());            
        }        

        public DateTime Date
        {
            get { return _date; }
            set { SetProperty(ref _date, value); }
        }      

        public List<Appointment> AppointmentsList
        {
            get { return _appointmentsList; }
            set { SetProperty(ref _appointmentsList, value); }
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
     

        public void AddEvent(Appointment e)
        {
            AppointmentsList.Add(e);
            AppointmentsList = new List<Appointment>(AppointmentsList.OrderBy(ev => ev.StartTime.TimeOfDay).ToList());
        }

        public void EditEvent(Appointment e)
        {
            foreach (Appointment ev in AppointmentsList)
                if (ev.AppointmentId.Equals(e.AppointmentId))
                    ev.copy(e);
            AppointmentsList = new List<Appointment>(AppointmentsList.OrderBy(ev => ev.StartTime.TimeOfDay).ToList());
        }

        public void DeleteEvent(Appointment e)
        {
            AppointmentsList.Remove(e);
            AppointmentsList = new List<Appointment>(AppointmentsList.OrderBy(ev => ev.StartTime.TimeOfDay).ToList());
        }
        
    }
}
