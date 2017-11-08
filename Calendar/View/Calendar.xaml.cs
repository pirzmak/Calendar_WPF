using Calendar.Model;
using Calendar.ModelView;
using Calendar.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calendar
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            mainStyle = blueStyle;
            SetResourcesColors(mainStyle);

            fontStyle = new FontFamily("Arial");
            Resources["fontStyle"] = fontStyle;

            InitializeComponent();   
        }
        
        private Dictionary<string, SolidColorBrush> mainStyle;
        private FontFamily fontStyle;

        private static Dictionary<string, SolidColorBrush> redStyle = new Dictionary<string, SolidColorBrush>
        {
            { "mainColor", GetColor("#bf0000") },
            { "secondColor", GetColor("#800000") },
            { "shadowColor", GetColor("#990000") },
            { "eventColor", GetColor("#e2a60d") },
            { "fontColor", GetColor("#e2a60d") },
        };

        private static Dictionary<string, SolidColorBrush> blueStyle = new Dictionary<string, SolidColorBrush>
        {
            { "mainColor", GetColor("#46a2c5") },
            { "secondColor", GetColor("#2b94a3") },
            { "shadowColor", GetColor("#4bbcd4") },
            { "eventColor", GetColor("#000000") },
            { "fontColor", GetColor("#ffffff") },
        };

        private static Dictionary<string, SolidColorBrush> greenStyle = new Dictionary<string, SolidColorBrush>
        {
            { "mainColor", GetColor("#328a2e") },
            { "secondColor", GetColor("#8dcf8a") },
            { "shadowColor", GetColor("#994500") },
            { "eventColor", GetColor("#156711") },
            { "fontColor", GetColor("#000000") },
        };

        public void MenuItem_Enter(object sender, RoutedEventArgs e)
        {
            switch ((String)((sender as MenuItem).Tag))
            {
                case "Red":
                    SetResourcesColors(redStyle);
                    break;
                case "Blue":
                    SetResourcesColors(blueStyle);
                    break;
                case "Green":
                    SetResourcesColors(greenStyle);
                    break;
                case "Arial":
                    Resources["fontStyle"] = new FontFamily("Arial");
                    break;
                case "Times":
                    Resources["fontStyle"] = new FontFamily("Times New Roman");
                    break;
                case "Comic":
                    Resources["fontStyle"] = new FontFamily("Comic Sans MS");
                    break;
                default:
                    SetResourcesColors(mainStyle);
                    break;
            }
        }

        public void MenuItem_Leave(object sender, RoutedEventArgs e)
        {
            SetResourcesColors(mainStyle);
        }

        public void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            switch ((String)((sender as MenuItem).Tag))
            {
                case "Red":
                    SetColors(redStyle);
                    break;
                case "Blue":
                    SetColors(blueStyle);
                    break;
                case "Green":
                    SetColors(greenStyle);
                    break;
                default:
                    break;
            }
            SetResourcesColors(mainStyle);
        }

        private void SetColors(Dictionary<string, SolidColorBrush> style)
        {
            mainStyle = style;
        }

        private void SetResourcesColors(Dictionary<string, SolidColorBrush> style)
        {
            Resources["mainColor"] = style["mainColor"];
            Resources["secondColor"] = style["secondColor"];
            Resources["shadowColor"] = style["shadowColor"];
            Resources["eventColor"] = style["eventColor"];
            Resources["fontColor"] = style["fontColor"];
        }

        private static SolidColorBrush GetColor(String color)
        {
            return (SolidColorBrush)(new BrushConverter().ConvertFrom(color));
        }

        private void ShowEventSettingInfo(Event e, bool newEvent)
        {
            MainWindowViewModel o1 = this.vm1;
            EventSettings wnd = new EventSettings();
            EventSettingsWindowViewModel s = wnd.vm1;
            if (newEvent)
                s.LoadNewEventInfo(e, o1.CalendarModel);
            else 
                s.LoadEventInfo(e, o1.CalendarModel);
            bool? res = wnd.ShowDialog();
        }

        protected void HandleDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ShowEventSettingInfo(((ListViewItem)sender).Content as Event, false);
        }

        protected void HandleRectClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                DateTime date = (((Grid)sender).DataContext as Day).Date;
                Event newEvent = new Event("", date, date, "");
                ShowEventSettingInfo(newEvent, true);
            }
        }       
    }   
}
