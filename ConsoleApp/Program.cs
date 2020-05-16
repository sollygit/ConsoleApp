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

        static void Run(ServiceProvider serviceProvider)
        {
            menu = new Menu()
            .Add("Revese Words", () => { MenuAction.Reverse_Words(); })
            .Add("Linked List", () => { MenuAction.Linked_List(); })
            .Add("Is Palindrome", () => { MenuAction.IsPalindromeTest(); })
            .Add("On Property Change", () => { MenuAction.ProductPropertyChanged(); })
            .Add("Fibonatchi", () => { MenuAction.Fibonatchi(); })
            .Add("Integer to Roman", () => { MenuAction.IntegerToRoman(); })
            .Add("Longest Word", () => { MenuAction.LongestWord(); })
            .Add("Custom Sort", () => { MenuAction.CustomSort(); })
            .Add("Seven Boom", () => { MenuAction.RunSevenBoom(); })
            .Add("Fizz Buzz", () => { MenuAction.FizzBuzz(); })
            .Add("JSON CSV Manipulation", () => { MenuAction.Json_Csv_Manipulation(); })
            .Add("Youtube Data API", () => { MenuAction.Youtube_SearchQuery(); })
            .Add("Facebook Public Profile", () => { MenuAction.Facebook_Profile(); })
            .Add("Facebook Search Query", () => { MenuAction.Facebook_SearchQuery(); })
            .Add("Async Enumerable Forcasts", () => { MenuAction.Forcasts_AsyncEnumerable(); })

            // Warehouse Action
            .Add("Products", () => { WarehouseAction.Products(serviceProvider); })
            .Add("Retailer Products", () => { WarehouseAction.RetailerProducts(serviceProvider); })
            .Add("Output Products", () => { WarehouseAction.OutputProducts(serviceProvider); })
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

            var serviceProvider = services.BuildServiceProvider();

            Run(serviceProvider);
        }
    }
}
