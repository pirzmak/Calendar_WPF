using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Calendar.Model
{
    public class EventDAO
    {
        private String filePath = "events.bor";
        private List<Event> events;

        private void WriteToXmlFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
        {
            TextWriter writer = null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                writer = new StreamWriter(filePath, append);
                serializer.Serialize(writer, objectToWrite);
            }
            catch (IOException)
            {
                var serializer = new XmlSerializer(typeof(T));
                writer = new StreamWriter(filePath, true);
                serializer.Serialize(writer, objectToWrite);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        private T ReadFromXmlFile<T>(string filePath) where T : new()
        {
            TextReader reader = null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                reader = new StreamReader(filePath);
                return (T)serializer.Deserialize(reader);
            }
            catch (IOException)  
            {
                reader = null;
                return new T();
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        public List<Day> getEvents()
        {
            events = ReadFromXmlFile<List<Event>>(filePath);

            List<Day> days = new List<Day>();

            foreach(Event e in events)
            {
                if (days.Exists(d => e.StartDate.Date.Equals(d.Date)))
                    days.Find(d => e.StartDate.Date.Equals(d.Date)).EventsList.Add(e);
                else
                {
                    Day newDay = new Day(e.StartDate.Date);
                    newDay.AddEvent(e);
                    days.Add(newDay);
                }
            }

            return days;
        }

        public void saveEvent(Event e)
        {
            events.Add(e);

            WriteToXmlFile<List<Event>>(filePath, events);
        }

        public void deleteEvent(Event e)
        {
            events.Remove(e);

            WriteToXmlFile<List<Event>>(filePath, events);
        }
    }
}
