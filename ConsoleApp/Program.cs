using ConsoleApp.Actions;
using ConsoleApp.Models;
using ConsoleApp.Service;
using EasyConsoleCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace ConsoleApp
{
    class Program
    {
        static bool exit = false;
        static Menu menu;

        public static ServiceProvider ServiceProvider { get; set; }

        static void Run()
        {
            menu = new Menu()
            .Add("Revese Words", new Action(Functions.Reverse_Words))
            .Add("Linked List", new Action(Functions.Linked_List))
            .Add("Is Palindrome", new Action(Functions.IsPalindromeTest))
            .Add("On Property Change", new Action(Functions.ProductPropertyChanged))
            .Add("Fibonatchi", new Action(Functions.Fibonatchi))
            .Add("Integer to Roman", new Action(Functions.IntegerToRoman))
            .Add("Longest Word", new Action(Functions.LongestWord))
            .Add("Custom Sort", new Action(Functions.CustomSort))
            .Add("Seven Boom", new Action(Functions.RunSevenBoom))
            .Add("Fizz Buzz", new Action(Functions.FizzBuzz))
            .Add("JSON CSV Manipulation", new Action(Functions.Json_Csv_Manipulation))
            .Add("Youtube Data API", new Action(Functions.Youtube_SearchQuery))
            .Add("Facebook Public Profile", new Action(Functions.Facebook_Profile))
            .Add("Facebook Search Query", new Action(Functions.Facebook_SearchQuery))
            .Add("Async Enumerable Forcasts", new Action(Functions.Forcasts_AsyncEnumerable))

            // Warehouse
            .Add("Products", new Action(Warehouse.Products))
            .Add("Retailer Products", new Action(Warehouse.RetailerProducts))
            .Add("Output Products", new Action(Warehouse.OutputProducts))
            .Add("Exit", () => { exit = true; });

            while (!exit)
            {
                menu.Display();
                Console.WriteLine("");
            }

            Lovely.Test();
        }

        static void Main()
        {
            // Setup services
            var services = new ServiceCollection()
                .AddLogging(configure => configure.AddConsole())
                .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Debug)
                .AddSingleton<IWarehouseService, WarehouseService>();

            ServiceProvider = services.BuildServiceProvider();

            Run();
        }
    }
}
