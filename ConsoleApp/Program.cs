using ConsoleApp.Actions;
using ConsoleApp.Models;
using ConsoleApp.Service;
using EasyConsoleCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace ConsoleApp
{
    internal class Program
    {
        static bool exit = false;
        static Menu menu;

        public static IConfiguration Configuration { get; private set; }
        public static ServiceProvider ServiceProvider { get; private set; }

        static IConfigurationBuilder Configure(IConfigurationBuilder config, string environmentName)
        {
            return config
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
                .AddUserSecrets<Program>()
                .AddEnvironmentVariables();
        }

        static IConfiguration CreateConfiguration()
        {
            var env = new HostingEnvironment
            {
                EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production",
                ApplicationName = AppDomain.CurrentDomain.FriendlyName,
                ContentRootPath = AppDomain.CurrentDomain.BaseDirectory,
                ContentRootFileProvider = new PhysicalFileProvider(AppDomain.CurrentDomain.BaseDirectory)
            };

            var config = new ConfigurationBuilder();
            var configured = Configure(config, env.EnvironmentName);

            return configured.Build();
        }

        static void Run()
        {
            menu = new Menu()
                .Add("Exit", () => { exit = true; })
                .Add("Revese Words", new Action(Functions.Reverse_Words))
                .Add("Linked List", new Action(Functions.Linked_List))
                .Add("Is Palindrome", new Action(Functions.IsPalindromeTest))
                .Add("On Property Change", new Action(Functions.ProductPropertyChanged))
                .Add("Fibonatchi", new Action(Functions.Fibonacci))
                .Add("Integer to Roman", new Action(Functions.IntegerToRoman))
                .Add("Longest Word", new Action(Functions.LongestWord))
                .Add("Custom Sort", new Action(Functions.CustomSort))
                .Add("Seven Boom (Press ESC to stop)", new Action(Functions.RunSevenBoom))
                .Add("Fizz Buzz", new Action(Functions.FizzBuzz))
                .Add("JSON to Model", new Action(Functions.JSON_To_Model))
                .Add("CSV to Model", new Action(Functions.CSV_To_Model))
                .Add("XML Folders", new Action(Functions.XmlFolders))
                .Add("YouTube Data API", new Action(Functions.Youtube_SearchQuery))
                .Add("Facebook Profile", new Action(Functions.Facebook_Profile))
                .Add("Quadratic Calculation", new Action(Functions.FindRoots))
                .Add("Weather Forcasts Async", new Action(async () => await Functions.WeatherForcasts_Async()))

                // Warehouse
                .Add("Products", new Action(async () => await Warehouse.Products()))
                .Add("Retailer Products", new Action(async () => await Warehouse.RetailerProducts()))
                .Add("Output Products", new Action(async () => await Warehouse.OutputProducts()));

            while (!exit)
            {
                menu.Display();
                if (!exit)
                {
                    Console.WriteLine("Hit Enter to continue...");
                    Console.ReadLine();
                }
            }

            Lovely.Test();
        }

        static void Main(string[] _args)
        {
            Configuration = CreateConfiguration();

            // Configure Services
            var services = new ServiceCollection()
                .AddLogging(configure => configure.AddConsole())
                .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Debug)
                .AddSingleton(Configuration)
                .AddSingleton<IWarehouseService, WarehouseService>();

            ServiceProvider = services.BuildServiceProvider();
            Run();
        }
    }
}
