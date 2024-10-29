using System;

namespace ConsoleApp.Models
{
    public class WeatherForecast
    {
        string summary;

        public delegate void PropertyChangeHandler(object sender, PropertyChangeEventArgs data);
        public event PropertyChangeHandler PropertyChange;

        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF
        {
            get
            {
                return 32 + (int)(TemperatureC / 0.5556);
            }
        }
        public string Summary 
        {
            get
            {
                return summary;
            }
            set
            {
                string oldSummary = summary;
                summary = value;

                OnPropertyChange(this, new PropertyChangeEventArgs("Summary", oldSummary, value));
            }
        }

        public override string ToString()
        {
            return $"{Date:g} {Summary.PadRight(10)} {TemperatureC}°C";
        }

        // The method which fires the Event
        public void OnPropertyChange(object sender, PropertyChangeEventArgs data)
        {
            // Ensure to have any Subscribers and invoke the Event
            PropertyChange?.Invoke(this, data);
        }
    }
}
