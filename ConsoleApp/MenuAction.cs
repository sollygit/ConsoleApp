using ConsoleApp.Common;
using ConsoleApp.Models;
using ConsoleApp.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    static class MenuAction
    {
        public static void ProductPropertyChanged()
        {
            var Product = new Product();
            Product.PropertyChange += new Product.PropertyChangeHandler(PropertyHasChanged);
            Product.Name = "First Name";
            Product.Name = "Second Name";
        }

        public static void PropertyHasChanged(object sender, PropertyChangeEventArgs data)
        {
            if (data.PropertyName == "Name")
            {
                Console.WriteLine("New value: '" + data.NewValue + "' was: '" + data.OldValue + "'");
            }
        }

        public static void LongestWord()
        {
            string words;
            Console.Write("Please enter a string of words:");
            while (String.IsNullOrEmpty(words = Console.ReadLine().Trim()))
            {
                Console.WriteLine("Your input cannot be empty or whitespace, please try again:");
            }

            var count = Helper.GetLongestWord(words);
            Console.WriteLine($"The longest word in {words} has {count} chars");
        }

        public static void IsPalindromeTest()
        {
            Console.Write("Please enter your word:");
            Helper.IsPalindrome(Console.ReadLine());
        }

        public static void IntegerToRoman()
        {
            Console.Write("Please enter your number:");
            var number = Convert.ToInt32(Console.ReadLine());
            Helper.IntegerToRoman(number);
        }

        public static void Reverse_Words()
        {
            string words;
            Console.Write("Please enter a string of words:");
            while (String.IsNullOrEmpty(words = Console.ReadLine().Trim()))
            {
                Console.WriteLine("Your input cannot be empty or whitespace, please try again:");
            }

            var reversed = Helper.ReverseWords(words);
            Console.WriteLine($"The reverse of \"{words}\" is \"{reversed}\"");
        }

        public static void Fibonatchi()
        {
            // Example: 1, 1, 2, 3, 5, 8, 13, 21, 34 
            Console.Write("Please enter a position in a Fibonatchi series numbers:");
            var position = Convert.ToInt32(Console.ReadLine());
            var number = Helper.Fibonatchi(position - 1);
            Console.WriteLine($"The number is {number}");
        }

        public static void CsvToJson()
        {
            try
            {
                using var reader = new StreamReader($"{Directory.GetCurrentDirectory()}\\{ConfigurationManager.AppSettings["Shared_Path"]}");
                var text = reader.ReadToEnd();
                var json = text.CsvToJson();

                Console.WriteLine(JToken.Parse(json).ToString(Formatting.Indented));
                Console.WriteLine();

                var todoItems = json.FromJson<IEnumerable<TodoItem>>();

                foreach (var item in todoItems)
                {
                    Console.WriteLine($"{item.Name}, {item.IsComplete}, {item.OwnerId}");
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void FizzBuzz()
        {
            try
            {
                string counter;
                Console.Write("Please enter a FizzBuzz counter:");
                while (String.IsNullOrEmpty(counter = Console.ReadLine().Trim()))
                {
                    Console.WriteLine("Your input cannot be empty or whitespace, please try again:");
                }

                new FizzBuzz().Start(Convert.ToInt32(counter), OutputType.Console);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async static void Forcasts_AsyncEnumerable()
        {
            await foreach (var forcast in forecasts())
            {
                Console.WriteLine($"{forcast}");
            }

            // Declare a local function.
            static async IAsyncEnumerable<WeatherForecast> forecasts()
            {
                var rng = new Random();

                for (int i = 0; i < 10; i++)
                {
                    await Task.Delay(1000); // Simulate waiting for data to come through. 

                    yield return new WeatherForecast
                    {
                        Date = Helper.GetRandomDate(DateTime.Now, DateTime.Now.AddYears(1)),
                        TemperatureC = rng.Next(-20, 55),
                        Summary = Constants.SUMMARIES[rng.Next(Constants.SUMMARIES.Length)]
                    };
                }
            }
        }

        public static void JSON_Deserialize()
        {
            Helper.DeserializeTechnology(Constants.JSON_TECHNOLOGIES);
        }

        public static void Lotto()
        {
            Lottery.Test();
        }

        public static void CustomSort()
        {
            // Output: 1, 3, 7, 2, 2, 8, 8, 5, 5, 5 
            Helper.CustomSort(new int[] { 8, 2, 2, 7, 5, 1, 8, 5, 3, 5 });
        }

        public static void Linked_List()
        {
            LinkedList.Test();
        }

        public static void Youtube_SearchQuery()
        {
            Console.WriteLine("Please enter your Youtube search query:");

            string query;

            while (String.IsNullOrEmpty(query = Console.ReadLine().Trim()))
            {
                Console.WriteLine("Your input cannot be empty or whitespace, please try again:");
            }

            var key = ConfigurationManager.AppSettings["YoutubeApiKey"];
            GoogleService.YouTubeSearch(key, query, 10).Wait();
        }

        public static void Facebook_Profile()
        {
            var accessToken = ConfigurationManager.AppSettings["FacebookAccessToken"];
            var facebookClient = new FacebookClient(ConfigurationManager.AppSettings["FacebookApiUri"]);
            var facebookService = new FacebookService(facebookClient);

            try
            {
                var account = facebookService.GetAccount(accessToken).GetAwaiter().GetResult();
                Console.WriteLine(JToken.Parse(account).ToString(Formatting.Indented));
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void Facebook_SearchQuery()
        {
            Console.WriteLine("Please enter your search query:");

            var accessToken = ConfigurationManager.AppSettings["FacebookAccessToken"];
            string query;

            while (String.IsNullOrEmpty(query = Console.ReadLine().Trim()))
            {
                Console.WriteLine("Your input cannot be empty or whitespace, please try again:");
            }

            var facebookClient = new FacebookClient(ConfigurationManager.AppSettings["FacebookApiUri"]);
            var facebookService = new FacebookService(facebookClient);

            try
            {
                var result = facebookService.Search(accessToken, query).GetAwaiter().GetResult();

                if (result == "{\"data\":[]}")
                {
                    Console.WriteLine("No results found!");
                }
                else
                {
                    Console.WriteLine(JToken.Parse(result).ToString(Formatting.Indented));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void Async_Stream()
        {
            var cts = new CancellationTokenSource();

            Console.CancelKeyPress += (s, e) =>
            {
                cts.Cancel();
            };

            AsyncUtil.GetSequenceAsync(cts.Token).Wait();
        }
    }
}
