using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Model
{
    public class Storage
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public List<Person> getPersons()
        {
            using (var db = new StorageContext())
                return db.Persons.ToList();
        }

        public Person getPerson(string login)
        {
            try
            {
                using (var db = new StorageContext())
                    return db.Persons.Where(p => p.UserID.Equals(login)).First();
            }
            catch (ArgumentNullException ANE)
            {
                log.Error("User " + login + "can't be load:" + ANE);
                throw ANE;
            }
        }

        public List<Appointment> getAppointments()
        {
            using (var db = new StorageContext())
            {
                return db.Appointments.ToList();
            }
                
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
                try
                {
                    db.SaveChanges();
                }
                catch (Exception E)
                {
                    log.Error("Message did not create becouse of: " + E);
                    throw E;
                }
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
