using Calendar.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Model
{
    public class Event : NotifyBase
    {
        public DateTime _startDate;
        public DateTime _endDate;
        public String _title;
        public String _message;
        public Guid Id { get;}

        public DateTime StartDate
        {
            get { return _startDate; }
            set { SetProperty(ref _startDate, value); }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set { SetProperty(ref _endDate, value); }
        }

        public String Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public String Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public Event(String title, DateTime start, DateTime end, String message)
        {
            this.Title = title;
            this.StartDate = start;
            this.EndDate = end;
            this.Message = message;
            this.Id = Guid.NewGuid();
        }

        public Event(String title)
        {
            this.Title = title;
            this.StartDate = DateTime.Now;
            this.EndDate = DateTime.Now;
            this.Message = "";
            this.Id = Guid.NewGuid();
        }

        public Event()
        {
            this.Title = "";
            this.StartDate = DateTime.Now;
            this.EndDate = DateTime.Now;
            this.Message = "";
            this.Id = Guid.NewGuid();
        }

        public override string ToString()
        {
            return "ID: " + this.Id + " Date: " + this.StartDate.Date + " Title: " + this.Title + " " + this.StartDate.ToString("HH:mm") + "-" + this.EndDate.ToString("HH:mm") + " Message:" + this.Message;
        }
    }
}
