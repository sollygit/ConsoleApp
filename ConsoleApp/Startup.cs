﻿using EasyConsoleCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;

namespace ConsoleApp
{
    internal class Startup
    {
        readonly IConfiguration Configuration;

        public Startup()
        {
            var env = new HostingEnvironment {
                EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production",
                ApplicationName = AppDomain.CurrentDomain.FriendlyName,
                ContentRootPath = AppDomain.CurrentDomain.BaseDirectory,
                ContentRootFileProvider = new PhysicalFileProvider(AppDomain.CurrentDomain.BaseDirectory)
            };

            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddUserSecrets<Program>()
                .AddEnvironmentVariables()
                .Build();

            new ServiceCollection()
                .AddLogging(configure => configure.AddConsole())
                .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Debug)
                .AddSingleton(Configuration)
                .BuildServiceProvider();
        }

        public void Run()
        {
            var exit = false;
            var Technologies = Configuration["Technologies"]!;
            var Recipes = Configuration["Recipes"]!;
            var XmlFile = Configuration["XmlFile"]!;
            var TodoItems = Configuration["TodoItems"]!;
            var FacebookApiUri = Configuration["FacebookApiUri"]!;
            var FacebookAccessToken = Configuration["FacebookAccessToken"]!;
            var YoutubeApiKey = Configuration["YoutubeApiKey"]!;
            var menu = new Menu()
                .Add("Exit", () => { exit = true; })
                .Add("Revese Words", new Action(Functions.Reverse_Words))
                .Add("Linked List", new Action(Functions.Linked_List))
                .Add("Is Palindrome", new Action(Functions.IsPalindromeTest))
                .Add("On Property Change", new Action(Functions.WeatherForecastPropertyChanged))
                .Add("Fibonatchi", new Action(Functions.Fibonacci))
                .Add("Integer to Roman", new Action(Functions.IntegerToRoman))
                .Add("Longest Word", new Action(() => Functions.LongestWord(TodoItems)))
                .Add("Custom Sort", new Action(Functions.CustomSort))
                .Add("Seven Boom (Press ESC to stop)", new Action(Functions.RunSevenBoom))
                .Add("Fizz Buzz", new Action(Functions.FizzBuzz))
                .Add("JSON to Model", new Action(() => Functions.JSON_To_Model(Technologies, Recipes)))
                .Add("CSV to Model", new Action(() => Functions.CSV_To_Model(TodoItems)))
                .Add("XML Folders", new Action(() => Functions.XmlFolders(XmlFile)))
                .Add("YouTube Data API", new Action(() => Functions.Youtube_SearchQuery(YoutubeApiKey)))
                .Add("Facebook Profile", new Action(() => Functions.Facebook_Profile(FacebookApiUri, FacebookAccessToken)))
                .Add("Quadratic Calculation", new Action(Functions.FindRoots))
                .Add("Weather Forcasts Async", new Action(async () => await Functions.WeatherForcasts_Async()));

            while (!exit)
            {
                menu.Display();

                if (!exit)
                {
                    Console.WriteLine("Hit Enter to continue...");
                    Console.ReadLine();
                }
            }
        }
    }
}
