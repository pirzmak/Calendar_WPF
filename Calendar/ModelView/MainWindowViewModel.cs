﻿using Calendar.Model;
using Calendar.Utils;
using System;
using System.Linq;
using System.Windows.Input;
using System.Collections.Specialized;

namespace Calendar.ModelView
{
    public class MainWindowViewModel : NotifyBase
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        const int NUMDAYSINWEEK = 7;

        public DateTime ActualDay { get; set; }

        public ICalendar CalendarModel { get; set; }

        private Day[][] _days;

        public MainWindowViewModel()
        {
            ActualDay = GetFirstDayOfWeek(DateTime.Now);

            CalendarModel = new CalendarModel();
            CalendarModel.LoadEvents(ActualDay, ActualDay.AddDays(4 * NUMDAYSINWEEK));

            CalendarModel.Days.CollectionChanged += OnPropertyChange;

            Days = LoadEvents();     
            

            log.Info("Calendar Started");
        }

        public void OnPropertyChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            Days = LoadEvents();            
        }

        public Day[][] Days
        {
            get { return _days; }
            set { SetProperty(ref _days, value); }
        }


        public void NextClick()
        {
            MoveCalendarView(7);
        }

        public void PrevClick()
        {
            MoveCalendarView(-7);
        }

        private void MoveCalendarView(int days)
        {
            ActualDay = ActualDay.AddDays(days);
            Days = LoadEvents();
        }

        private Day[][] LoadEvents()
        {
            Day[][] newDL = new Day[4][];

            DateTime day = ActualDay;

            for (int x = 0; x < newDL.Length; x += 1)
            {
                newDL[x] = new Day[NUMDAYSINWEEK];
                for (int y = 0; y < NUMDAYSINWEEK; y += 1)
                {                    
                    if (CalendarModel.Days.Any(d => d.Date.Date.Equals(day.Date)))
                        newDL[x][y] = CalendarModel.Days.Single(d => d.Date.Date.Equals(day.Date));
                    else
                        newDL[x][y] = new Day(day);

                    day = day.AddDays(1);
                }
            }

            return newDL;
        }              

        private DateTime GetFirstDayOfWeek(DateTime date)
        {
            return date.AddDays(-((int)date.DayOfWeek - 1));
        }

        public ICommand NextClickCommand { get { return new RelayCommand(o => NextClick(), () => true); } }
        public ICommand PrevClickCommand { get { return new RelayCommand(o => PrevClick(), () => true); } }

    }
}