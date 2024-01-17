﻿using System;

namespace ConsoleApp.Models
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public string Summary { get; set; }
        public int TemperatureF
        {
            get
            {
                return 32 + (int)(TemperatureC / 0.5556);
            }
        }

        public override string ToString()
        {
            return $"{Date.ToString("g")} {Summary.PadRight(10)} {TemperatureC}°C";
        }
    }
}
