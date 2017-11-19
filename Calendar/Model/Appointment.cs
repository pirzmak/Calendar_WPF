using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Model
{
    public class Appointment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid AppointmentId { get; set; }
        public string Title { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public virtual List<Attendance> Attendances { get; set; }
        public void copy(Appointment e)
        {
            this.Title = e.Title;
            this.AppointmentDate = e.AppointmentDate;
            this.StartTime = e.StartTime;
            this.EndTime = e.EndTime;
        }
    }
}