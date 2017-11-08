using Calendar.Model;
using Calendar.Utils;
using System;
using System.Windows.Input;
using System.Diagnostics;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows;

namespace Calendar.ModelView
{
    class EventSettingsWindowViewModel : NotifyBase
    {       

        public Event MyEvent { get; set; }
        private CalendarModel calendarModel;
        private bool _oldEvent;

        private String _newTitle;
        private String _newMessage;
        private int _newFromH;
        private int _newFromM;
        private int _newToH;
        private int _newToM;

        public bool OldEvent
        {
            get { return _oldEvent; }
            set { SetProperty(ref _oldEvent, value); }
        }

        public String NewTitle
        {
            get { return _newTitle; }
            set { SetProperty(ref _newTitle, value); }
        }

        public String NewMessage
        {
            get { return _newMessage; }
            set { SetProperty(ref _newMessage, value); }
        }

        public int NewFromH
        {
            get { return _newFromH; }
            set { SetProperty(ref _newFromH, value); }
        }

        public int NewFromM
        {
            get { return _newFromM; }
            set { SetProperty(ref _newFromM, value); }
        }

        public int NewToH
        {
            get { return _newToH; }
            set { SetProperty(ref _newToH, value); }
        }

        public int NewToM
        {
            get { return _newToM; }
            set { SetProperty(ref _newToM, value); }
        }

        public void loadEventInfo(Event e, CalendarModel calendarModel)
        {
            NewTitle = e.Title;
            NewMessage = e.Message;
            NewFromH = e.StartDate.Hour;
            NewFromM = e.StartDate.Minute;
            NewToH = e.EndDate.Hour;
            NewToM = e.EndDate.Minute;
            MyEvent = e;
            OldEvent = true;
            this.calendarModel = calendarModel;
        }

        public void loadNewEventInfo(Event e, CalendarModel calendarModel)
        {
            NewTitle = "";
            NewMessage = "";
            NewFromH = 12;
            NewFromM = 0;
            NewToH = 12;
            NewToM = 0;
            MyEvent = e;
            OldEvent = false;
            this.calendarModel = calendarModel;
        }

        private void saveButtonClick(object sender)
        {
            if (NewTitle != "")
            {
                MyEvent.Title = NewTitle;
                MyEvent.Message = NewMessage;
                MyEvent.StartDate = new DateTime(MyEvent.StartDate.Year, MyEvent.StartDate.Month, MyEvent.StartDate.Day, NewFromH, NewFromM, 0);
                MyEvent.EndDate = new DateTime(MyEvent.StartDate.Year, MyEvent.StartDate.Month, MyEvent.StartDate.Day, NewToH, NewToM, 0);

                if(!OldEvent)
                    calendarModel.addEvent(MyEvent);

                ((Window)sender).Close();
            }
        }

        private void deleteButtonClick(object sender)
        {
            calendarModel.deleteEvent(MyEvent);
            ((Window)sender).Close();
        }

        private ICommand _saveCommand;

        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(
                        param => this.saveButtonClick(param)
                    );
                }
                return _saveCommand;
            }
        }

        private ICommand _deleteCommand;

        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                {
                    _deleteCommand = new RelayCommand(
                        param => this.deleteButtonClick(param)
                    );
                }
                return _deleteCommand;
            }
        }

    }

}