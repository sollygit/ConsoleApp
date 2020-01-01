using EasyConsoleCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace ConsoleApp
{
    class Program
    {
        static bool exit = false;
        static readonly Menu menu = new Menu()
            .Add("Lovely", () => { Lovely.Test(); })
            .Add("FizzBuzz", () => { FizzBuzz(); })
            .Add("Youtube Data API", () => { GoogleDataApi.YouTubeDataApiSearch().Wait(); })
            .Add("Facebook Graph API My Account", () => { FacebookGraphApi_MyAccount(); })
            .Add("Facebook Graph API Account Query", () => { FacebookGraphApi_AccountQuery(); })
            .Add("Revese Words", () => { ReverseWords(); })
            .Add("Linked List", () => { LinkedList.Test(); })
            .Add("Is Palindrome ?", () => { IsPalindromeTest(); })
            .Add("Product Property Change", () => { ProductPropertyChanged(); })
            .Add("Fibonatchi", () => { FibonatchiTest(); })
            .Add("Integer To Roman", () => { IntegerToRomanTest(); })
            .Add("Longest Word", () => { LongestWordTest(); })
            .Add("Custom Sort", () => { Helper.CustomSort(new int[] { 8, 2, 2, 7, 5, 1, 8, 5, 3, 5 }); }) // Output: 1, 3, 7, 2, 2, 8, 8, 5, 5, 5 
            .Add("Lottery", () => { Lottery.Test(); })
            .Add("JSON Deserialize", () => { Helper.DeserializeTechnology(Helper.JSON_TECHNOLOGIES); })
            .Add("CSV to JSON", () => { CsvToJson(); })
            .Add("FizzBuzz", () => { FizzBuzz(); })
            .Add("Exit", () => exit = true);

        static void Main()
        {
            while (!exit)
            {
                menu.Display();
            }
        }

        static void ProductPropertyChanged()
        {
            var Product = new Product();
            Product.PropertyChange += new Product.PropertyChangeHandler(PropertyHasChanged);
            Product.Name = "First Name";
            Product.Name = "Second Name";
        }

        static void PropertyHasChanged(object sender, PropertyChangeEventArgs data)
        {
            if (data.PropertyName == "Name")
            {
                Console.WriteLine("New value: '" + data.NewValue + "' was: '" + data.OldValue + "'");
            }
        }

        static void LongestWordTest()
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

        static void IsPalindromeTest()
        {
            Console.Write("Please enter your word:");
            Helper.IsPalindrome(Console.ReadLine());
        }

        static void IntegerToRomanTest()
        {
            Console.Write("Please enter your number:");
            var number = Convert.ToInt32(Console.ReadLine());
            Helper.IntegerToRoman(number);
        }

        static void ReverseWords()
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

        static void FibonatchiTest()
        {
            // Example: 1, 1, 2, 3, 5, 8, 13, 21, 34 
            Console.Write("Please enter a position in a Fibonatchi series numbers:");
            var position = Convert.ToInt32(Console.ReadLine());
            var number = Helper.Fibonatchi(position - 1);
            Console.WriteLine($"The number is {number}");
        }

        static void CsvToJson()
        {
            try
            {
                using (var reader = new StreamReader($"{Directory.GetCurrentDirectory()}\\{ConfigurationManager.AppSettings["CSV_Path"]}"))
                {
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
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void FizzBuzz()
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

        static void FacebookGraphApi_MyAccount()
        {
            string accessToken = ConfigurationManager.AppSettings["FacebookAccessToken"];
            Console.WriteLine("My Facebook account using Facebook Graph API:");

            var facebookClient = new FacebookClient();
            var facebookService = new FacebookService(facebookClient);

            try
            {
                var account = facebookService.GetAccount(accessToken).GetAwaiter().GetResult();

                if (String.IsNullOrEmpty(account))
                {
                    Console.WriteLine("Something went wrong in GetAccount, Check access token.");
                }
                else
                {
                    Console.WriteLine(JToken.Parse(account).ToString(Formatting.Indented));
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static void FacebookGraphApi_AccountQuery()
        {
            string accessToken = ConfigurationManager.AppSettings["FacebookAccessToken"];
            Console.WriteLine("Please enter a name to search with Facebook Graph API:");
            string query;
            while (String.IsNullOrEmpty(query = Console.ReadLine().Trim()))
            {
                Console.WriteLine("Your input cannot be empty or whitespace, please try again:");
            }

            var facebookClient = new FacebookClient();
            var facebookService = new FacebookService(facebookClient);

            try
            {
                var result = facebookService.GetAccountList(accessToken, query).GetAwaiter().GetResult();

                if (String.IsNullOrEmpty(result))
                {
                    Console.WriteLine("Something went wrong in GetAccountList.");
                }
                else if (result == "{\"data\":[]}")
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
    }
}
