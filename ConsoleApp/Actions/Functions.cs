using ConsoleApp.Common;
using ConsoleApp.Models;
using ConsoleApp.Services;
using EasyConsoleCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp.Actions
{
    static class Functions
    {
        public static void Reverse_Words()
        {
            string words;
            Console.Write("Please enter a string of words:");
            while (String.IsNullOrEmpty(words = Console.ReadLine().Trim()))
            {
                Console.WriteLine("Your input cannot be empty or whitespace, please try again:");
            }

            var reversed = Utility.ReverseWords(words);
            Console.WriteLine($"The reverse of \"{words}\" is \"{reversed}\"");
        }

        public static void Linked_List()
        {
            LinkedList.Test();
        }

        public static void IsPalindromeTest()
        {
            Console.Write("Please enter your word:");
            Utility.IsPalindrome(Console.ReadLine());
        }

        public static void ProductPropertyChanged()
        {
            var Product = new Product();
            Product.PropertyChange += new Product.PropertyChangeHandler(PropertyHasChanged);
            Product.ProductName = "Jack Daniels";
            Product.ProductName = "Black Label";
        }

        public static void PropertyHasChanged(object sender, PropertyChangeEventArgs data)
        {
            if (string.IsNullOrEmpty(data.OldValue)) return;

            if (data.PropertyName == "ProductName")
            {
                Console.WriteLine($"New value: '{data.NewValue}' Old value: '{data.OldValue}'");
            }
        }

        public static void Fibonacci()
        {
            // Example: 0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55 
            Console.Write("Please enter a position in a Fibonacci series:");
            var position = Convert.ToInt32(Console.ReadLine());
            var result = Utility.Fibonacci(position - 1);
            Console.WriteLine($"The result of position {position} is {result}");
        }

        public static void IntegerToRoman()
        {
            string input;
            int number;
            Console.WriteLine("Please enter your integer number:");
            while (!(!string.IsNullOrEmpty(input = Console.ReadLine().Trim()) &&
                int.TryParse(input, out number) && number > 0 && number <= 100000))
            {
                Console.WriteLine("Please enter a number between 1 to 100,000:");
            }

            Utility.IntegerToRoman(number);
        }

        public static void LongestWord()
        {
            string words;
            Console.Write("Please enter a string of words:");
            while (String.IsNullOrEmpty(words = Console.ReadLine().Trim()))
            {
                Console.WriteLine("Your input cannot be empty or whitespace, please try again:");
            }

            var count = Utility.GetLongestWord(words);
            Console.WriteLine($"The longest word in {words} has {count} chars");
        }

        public static void CustomSort()
        {
            Utility.CustomSort(new int[] { 8, 2, 2, 7, 5, 1, 8, 5, 3, 5 });
        }

        public static void RunSevenBoom()
        {
            SevenBoom.Run();
        }

        public static void FizzBuzz()
        {
            try
            {
                string counter;
                int intCounter;
                Console.Write("Please enter a FizzBuzz counter:");
                while (!(!string.IsNullOrEmpty(counter = Console.ReadLine().Trim()) &&
                int.TryParse(counter, out intCounter) && intCounter > 0 && intCounter <= 1000))
                {
                    Console.WriteLine("Please enter a number between 1 to 1000:");
                }
                Utility.FizzBuzz(intCounter);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void JSON_To_Model()
        {
            var techs = Program.Configuration["Technologies"];
            var json = File.ReadAllText(@$"{Directory.GetCurrentDirectory()}\{techs}");
            var technologies = Deserializer.FromJson<Technology>(json)
                .OrderBy(o => o.TechnologyId);

            Console.WriteLine($"{Environment.NewLine}Technologies:");

            foreach (var t in technologies)
            {
                Console.WriteLine(t.ToString());
            }

            var recipeFile = Program.Configuration["Recipes"];
            json = File.ReadAllText(@$"{Directory.GetCurrentDirectory()}\{recipeFile}");
            var recipes = Deserializer.FromJsonDictionary<Recipe>(json)
                .OrderBy(o => o.Key);

            Console.WriteLine($"{Environment.NewLine}Recipes:")
;
            foreach (var r in recipes)
            {
                Console.WriteLine($"{r.Key},{r.Value.Name},{r.Value.SourceShort}");
            }
        }

        public static void CSV_To_Model()
        {
            try
            {
                var todoItems = Program.Configuration["TodoItems"];
                var path = @$"{Directory.GetCurrentDirectory()}\{todoItems}";
                var todos = Deserializer.FromCsv<TodoItem>(path, new string[] { "IsComplete", "Name", "OwnerId" });

                foreach (var item in todos)
                {
                    Console.WriteLine($"{item.IsComplete},{item.Name},{item.OwnerId}");
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void Youtube_SearchQuery()
        {
            Console.WriteLine("Please enter your Youtube search query:");

            string query;

            while (string.IsNullOrEmpty(query = Console.ReadLine().Trim()))
            {
                Console.WriteLine("Your input cannot be empty or whitespace, please try again:");
            }

            // https://console.cloud.google.com/apis/api/youtube.googleapis.com/credentials?cloudshell=false&project=jovial-monument-321404
            var key = Program.Configuration["YoutubeApiKey"];
            GoogleService.YouTubeSearch(key, query, 10).Wait();
        }

        public static void Facebook_Profile()
        {
            var accessToken = Program.Configuration["FacebookAccessToken"];
            var facebookClient = new FacebookClient(Program.Configuration["FacebookApiUri"]);
            var facebookService = new FacebookService(facebookClient);

            try
            {
                var account = facebookService.GetProfile(accessToken).GetAwaiter().GetResult();
                Console.WriteLine(JToken.Parse(account).ToString(Formatting.Indented));
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void FindRoots()
        {
            Console.WriteLine("The Roots of Quadratic Equation 2x^ + 10x + 8 = 0");
            var result = Utility.FindRoots(2, 10, 8);
            Console.WriteLine($"{result.Item1}, {result.Item2}");
        }

        public static void XmlFolders()
        {
            var xmlFile = Program.Configuration["XmlFile"];
            using var reader = new StreamReader(@$"{Directory.GetCurrentDirectory()}\{xmlFile}");
            var xml = reader.ReadToEnd();
            var names = UtilityXml.GetFolders(xml, 'u');
            Console.WriteLine(string.Join(',', names));
        }

        public static async Task WeatherForcasts_Async()
        {
            string input;
            int length;
            Console.WriteLine("Total number of forcasts:");
            while (!(!string.IsNullOrEmpty(input = Console.ReadLine().Trim()) &&
                int.TryParse(input, out length) && length > 0 && length <= 10))
            {
                Console.WriteLine("Please enter a number between 1 to 10:");
            }

            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            Console.CancelKeyPress += (s, e) => { cts.Cancel(); };
            await Utility.GetWeatherForecastAsync(cts.Token, Convert.ToInt32(length));
        }
    }
}
