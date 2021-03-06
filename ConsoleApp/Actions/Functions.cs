﻿using ConsoleApp.Common;
using ConsoleApp.Models;
using ConsoleApp.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;

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

            var reversed = Helper.ReverseWords(words);
            Console.WriteLine($"The reverse of \"{words}\" is \"{reversed}\"");
        }

        public static void Linked_List()
        {
            LinkedList.Test();
        }

        public static void IsPalindromeTest()
        {
            Console.Write("Please enter your word:");
            Helper.IsPalindrome(Console.ReadLine());
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

        public static void Fibonatchi()
        {
            // Example: 1, 1, 2, 3, 5, 8, 13, 21, 34 
            Console.Write("Please enter a position in a Fibonatchi series numbers:");
            var position = Convert.ToInt32(Console.ReadLine());
            var number = Helper.Fibonatchi(position - 1);
            Console.WriteLine($"The number is {number}");
        }

        public static void IntegerToRoman()
        {
            Console.Write("Please enter your number:");
            var number = Convert.ToInt32(Console.ReadLine());
            Helper.IntegerToRoman(number);
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

        public static void CustomSort()
        {
            Helper.CustomSort(new int[] { 8, 2, 2, 7, 5, 1, 8, 5, 3, 5 });
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

        public static void JSON_To_Model()
        {
            var json = File.ReadAllText(@$"{Directory.GetCurrentDirectory()}\{ConfigurationManager.AppSettings["Technologies"]}");
            var technologies = Helper.DeserializeJson<Technology>(json)
                .OrderBy(o => o.TechnologyId);
            
            Console.WriteLine($"{Environment.NewLine}Technologies:");

            foreach (var t in technologies)
            {
                Console.WriteLine($"{t.TechnologyId},{t.TechnologyName}");
            }

            json = File.ReadAllText(@$"{Directory.GetCurrentDirectory()}\{ConfigurationManager.AppSettings["Recipes"]}");
            var recipes = Helper.Deserialize<Recipe>(json)
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
                var path = @$"{Directory.GetCurrentDirectory()}\{ConfigurationManager.AppSettings["TodoItems"]}";
                var todos = Helper.Deserialize<TodoItem>(path, new string[] { "IsComplete", "Name", "OwnerId" });

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

        public static void Forcasts_AsyncEnumerable()
        {
            var cts = new CancellationTokenSource();

            Console.CancelKeyPress += (s, e) =>
            {
                cts.Cancel();
            };

            AsyncHelper.GetWeatherForecastAsync(cts.Token).Wait();
        }

        public static void FindRoots()
        {
            Console.WriteLine("The Roots of Quadratic Equation 2x^ + 10x + 8 = 0");
            var result = Helper.FindRoots(2, 10, 8);
            Console.WriteLine($"{result.Item1}, {result.Item2}");
        }

        public static void XmlFolders()
        {
            using var reader = new StreamReader(@$"{Directory.GetCurrentDirectory()}\{ConfigurationManager.AppSettings["XmlFile"]}");
            var xml = reader.ReadToEnd();
            var names = XmlHelper.GetFolders(xml, 'u');
            Console.WriteLine(string.Join(',', names));
        }
    }
}
