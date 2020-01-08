using ConsoleApp.Models;
using EasyConsoleCore;
using System;

namespace ConsoleApp
{
    class Program
    {
        static bool exit = false;
        static Menu menu;

        static void Run()
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
            .Add("Lottery", () => { MenuAction.Lotto(); })
            .Add("JSON Deserialise", () => { MenuAction.JSON_Deserialize(); })
            .Add("CSV to JSON", () => { MenuAction.CsvToJson(); })
            .Add("Fizz Buzz", () => { MenuAction.FizzBuzz(); })
            .Add("Yield AsyncEnumerable Forcasts", () => { MenuAction.Forcasts_AsyncEnumerable(); })
            .Add("Youtube Data API", () => { MenuAction.Youtube_SearchQuery(); })
            .Add("Facebook Public Profile", () => { MenuAction.Facebook_Profile(); })
            .Add("Facebook Search Query", () => { MenuAction.Facebook_SearchQuery(); })
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
            Run();
        }
    }
}
