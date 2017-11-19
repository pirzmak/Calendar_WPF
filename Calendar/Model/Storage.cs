using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Model
{
    public class Storage
    {
        public List<Person> getPersons()
        {
            using (var db = new StorageContext())
                return db.Persons.ToList();
        }

        public Person getPerson(string login)
        {
            using (var db = new StorageContext())
                return db.Persons.Where(p => p.UserID == login).First();
        }

        public List<Appointment> getAppointments()
        {
            using (var db = new StorageContext())
                return db.Appointments.ToList();
        }

        public void createAppointment(string title, DateTime startDate, DateTime endDate)
        {
            using (var db = new StorageContext())
            {
                var Appointment = new Appointment
                {
                    Title = title,
                    AppointmentDate = startDate.Date,
                    StartTime = startDate,
                    EndTime = endDate,
                    Attendances = new List<Attendance>()
                };
                db.Appointments.Add(Appointment);
                db.SaveChanges();
            }
        }

        public void updateAppointment(Appointment st)
        {
            using (var db = new StorageContext())
            {
                var original = db.Appointments.Find(st.AppointmentId);
                if (original != null)
                {
                    original.Title = st.Title;
                    original.AppointmentDate = st.AppointmentDate;
                    original.StartTime = st.StartTime;
                    original.EndTime = st.EndTime;
                    db.SaveChanges();
                }
            }
        }

        public void deleteAppointment(Appointment st)
        {
            using (var db = new StorageContext())
            {
                var original = db.Appointments.Find(st.AppointmentId);
                if (original != null)
                {
                    db.Appointments.Remove(original);
                    db.SaveChanges();
                }
            }
        }
    }
}
