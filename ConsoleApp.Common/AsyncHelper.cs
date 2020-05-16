using ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp.Common
{
    public static class AsyncHelper
    {
        public static async Task GetWeatherForecastAsync(CancellationToken token)
        {
            token.Register(new Action(() => Console.WriteLine("Operation Canceled!")));

            await foreach (var forcast in forecasts())
            {
                Console.WriteLine($"{forcast}");
            }

            static async IAsyncEnumerable<WeatherForecast> forecasts()
            {
                var rnd = new Random();
                for (int i = 0; i < 10; i++)
                {
                    await Task.Delay(1000); // Simulate waiting for data to come through. 

                    yield return new WeatherForecast
                    {
                        Date = Helper.GetRandomDate(DateTime.Now, DateTime.Now.AddYears(1)),
                        TemperatureC = rnd.Next(-20, 55),
                        Summary = Constants.SUMMARIES[rnd.Next(Constants.SUMMARIES.Length)]
                    };
                }
            }
        }
    }
}
