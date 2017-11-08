using Calendar.Model;
using Calendar.Utils;
using System;
using System.Linq;
using System.Windows.Input;
using System.Diagnostics;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Calendar.ModelView
{
    public class MainWindowViewModel : NotifyBase
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        const int NUMDAYSINWEEK = 7;

        private DateTime actualDay;

        private CalendarModel calendarModel;

        private Day[][] _days;

        public SolidColorBrush _mainColor;

        public SolidColorBrush MainColor
        {
            get { return _mainColor; }
            set { SetProperty(ref _mainColor, value); }
        }

        public MainWindowViewModel()
        {
            actualDay = getFirstDayOfWeek(DateTime.Now);

            calendarModel = new CalendarModel();
            calendarModel.loadEvents(actualDay, actualDay.AddDays(4 * NUMDAYSINWEEK));
            
            calendarModel.PropertyChanged += onPropertyChange;

            Days = loadEvents();

            MainColor = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ffaacc"));

            log.Info("Calendar Started");
        }

        public void onPropertyChange(object sender, PropertyChangedEventArgs e)
        {
            Days = loadEvents();            
        }

        public Day[][] Days
        {
            get { return _days; }
            set { SetProperty(ref _days, value); }
        }


        public void nextClick()
        {
            moveCalendarView(7);
        }

        public void prevClick()
        {
            moveCalendarView(-7);
        }

        private void moveCalendarView(int days)
        {
            actualDay = actualDay.AddDays(days);
            Days = loadEvents();
        }

        private Day[][] loadEvents()
        {
            Day[][] newDL = new Day[4][];

            DateTime day = actualDay;

            for (int x = 0; x < newDL.Length; x += 1)
            {
                newDL[x] = new Day[NUMDAYSINWEEK];
                for (int y = 0; y < NUMDAYSINWEEK; y += 1)
                {                    
                    if (calendarModel.Days.Any(d => d.Date.Date.Equals(day.Date)))
                        newDL[x][y] = calendarModel.Days.Single(d => d.Date.Date.Equals(day.Date));
                    else
                        newDL[x][y] = new Day(day);

                    day = day.AddDays(1);
                }
            }

            return newDL;
        }

        public CalendarModel getCalendarModel()
        {
            return calendarModel;
        }

        private DateTime getFirstDayOfWeek(DateTime date)
        {
            return date.AddDays(-((int)date.DayOfWeek - 1));
        }

        public ICommand nextClickCommand { get { return new RelayCommand(o => nextClick()); } }
        public ICommand prevClickCommand { get { return new RelayCommand(o => prevClick()); } }

    }

}