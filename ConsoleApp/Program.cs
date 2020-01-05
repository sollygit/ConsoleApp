using ConsoleApp.Common;
using ConsoleApp.Models;
using ConsoleApp.Services;
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
            .Add("Revese Words", () => { MenuAction.Reverse_Words(); })
            .Add("Linked List", () => { MenuAction.Linked_List(); })
            .Add("Is Palindrome?", () => { MenuAction.IsPalindromeTest(); })
            .Add("On Property Change", () => { MenuAction.ProductPropertyChanged(); })
            .Add("Fibonatchi", () => { MenuAction.Fibonatchi(); })
            .Add("Integer To Roman", () => { MenuAction.IntegerToRoman(); })
            .Add("Longest Word", () => { MenuAction.LongestWord(); })
            .Add("Custom Sort", () => { MenuAction.CustomSort(); })
            .Add("Lottery", () => { MenuAction.Lotto(); })
            .Add("JSON Deserialise", () => { MenuAction.JSON_Deserialize(); })
            .Add("CSV to JSON", () => { MenuAction.CsvToJson(); })
            .Add("Fizz Buzz", () => { MenuAction.FizzBuzz(); })
            .Add("Youtube Data API", () => { MenuAction.Youtube_SearchQuery(); })
            .Add("Facebook Public Profile", () => { MenuAction.Facebook_Profile(); })
            .Add("Facebook Search Query", () => { MenuAction.Facebook_SearchQuery(); })
            .Add("Exit", () => exit = true);

        static void Main()
        {
            Lovely.Test();

            while (!exit)
            {
                menu.Display();
            }
        }
    }
}
