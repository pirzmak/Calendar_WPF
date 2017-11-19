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
    public class EventSettingsWindowViewModel : NotifyBase
    {       

        public Appointment MyAppointment { get; set; }
        public ICalendar CalendarModel { get; set; }
        private bool _oldEvent;

        private String _newTitle;
        private String _newMessage;
        private int _newFromH;
        private int _newFromM;
        private int _newToH;
        private int _newToM;

        private bool _error;
        private string _errorText;

        private bool _warning;
        private string _warningText;

        public bool Error
        {
            get { return _error; }
            set { SetProperty(ref _error, value); }
        }

        public String ErrorText
        {
            get { return _errorText; }
            set { SetProperty(ref _errorText, value); }
        }

        public bool Warning
        {
            get { return _warning; }
            set { SetProperty(ref _warning, value); }
        }

        public String WarningText
        {
            get { return _warningText; }
            set { SetProperty(ref _warningText, value); }
        }

        public bool OldEvent
        {
            get { return _oldEvent; }
            set { SetProperty(ref _oldEvent, value); }
        }

        public String NewTitle
        {
            get { return _newTitle; }
            set
            {
                SetProperty(ref _newTitle, value);
                SaveCommand.RaiseCanExecuteChanged();
                Warning = false;
                if (NewTitle.Length == 0)
                {
                    Warning = true;
                    WarningText = "Title can't be empty";
                }
            }
        }

        public String NewMessage
        {
            get { return _newMessage; }
            set { SetProperty(ref _newMessage, value); }
        }

        public int NewFromH
        {
            get { return _newFromH; }
            set
            {
                SetProperty(ref _newFromH, value);
                SaveCommand.RaiseCanExecuteChanged();
                Warning = false;
                if (NewFromH * 60 + NewFromM < NewToH * 60 + NewToM)
                {
                    Warning = true;
                    WarningText = "Date from need to be before to";
                }
            }
        }

        public int NewFromM
        {
            get { return _newFromM; }
            set
            {
                SetProperty(ref _newFromM, value);
                SaveCommand.RaiseCanExecuteChanged();
                Warning = false;
                if (NewFromH * 60 + NewFromM < NewToH * 60 + NewToM)
                {
                    Warning = true;
                    WarningText = "Date from need to be before to";
                }
            }
        }

        public int NewToH
        {
            get { return _newToH; }
            set
            {
                SetProperty(ref _newToH, value);
                SaveCommand.RaiseCanExecuteChanged();
                Warning = false;
                if (NewFromH * 60 + NewFromM < NewToH * 60 + NewToM)
                {
                    Warning = true;
                    WarningText = "Date from need to be before to";
                }
            }
        }

        public int NewToM
        {
            get { return _newToM; }
            set
            {
                SetProperty(ref _newToM, value);
                SaveCommand.RaiseCanExecuteChanged();
                Warning = false;
                if (NewFromH * 60 + NewFromM < NewToH * 60 + NewToM)
                {
                    Warning = true;
                    WarningText = "Date from need to be before to";
                }
                
            }
        }

        public void LoadEventInfo(Appointment e, ICalendar calendarModel)
        {
            //NewMessage = e.Message;
            NewFromH = e.StartTime.Hour;
            NewFromM = e.StartTime.Minute;
            NewToH = e.EndTime.Hour;
            NewToM = e.EndTime.Minute;
            NewTitle = e.Title;
            MyAppointment = e;
            OldEvent = true;
            Error = false;
            CalendarModel = calendarModel;
        }

        public void LoadNewEventInfo(Appointment e, ICalendar calendarModel)
        {            
            NewMessage = "";
            NewFromH = 12;
            NewFromM = 0;
            NewToH = 12;
            NewToM = 0;
            NewTitle = "";
            MyAppointment = e;
            OldEvent = false;
            Error = false;
            CalendarModel = calendarModel;
        }

        private void SaveButtonClick(object sender)
        {
            if (this.isProperAppointment())
            {
                MyAppointment.Title = NewTitle;
                //MyAppointment.Message = NewMessage;
                MyAppointment.StartTime = new DateTime(MyAppointment.StartTime.Year, MyAppointment.StartTime.Month, MyAppointment.StartTime.Day, NewFromH, NewFromM, 0);
                MyAppointment.EndTime = new DateTime(MyAppointment.StartTime.Year, MyAppointment.StartTime.Month, MyAppointment.StartTime.Day, NewToH, NewToM, 0);

                try
                {
                    if (!OldEvent)
                        CalendarModel.AddAppointment(MyAppointment);
                    else
                        CalendarModel.UpdateAppointment(MyAppointment);

                    ((Window)sender).Close();
                }
                catch(Exception e)
                {
                    Error = true;
                    ErrorText = "Unfortunely appointment did not save becouse of " + e.Message + ". Please refresh";
                }
            }
        }

        private void DeleteButtonClick(object sender)
        {
            try
            {
                CalendarModel.DeleteAppointment(MyAppointment);
                ((Window)sender).Close();
            }
            catch (Exception e)
            {
                Error = true;
                ErrorText = "Unfortunely appointment did not save becouse of " + e.Message + ". Please refresh";
            }
        }

        public bool isProperAppointment()
        {
            return NewTitle != "" && NewFromH * 60 + NewFromM <= NewToH * 60 + NewToM;
        }

        private RelayCommand _saveCommand;

        public RelayCommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(
                        param => this.SaveButtonClick(param),
                        () => this.isProperAppointment()
                    );
                }
                return _saveCommand;
            }
        }

        private RelayCommand _deleteCommand;

        public RelayCommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                {
                    _deleteCommand = new RelayCommand(
                        param => this.DeleteButtonClick(param),
                        () => true
                    );
                }
                return _deleteCommand;
            }
        }

    }

}