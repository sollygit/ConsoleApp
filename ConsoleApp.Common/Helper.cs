﻿using ChoETL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace ConsoleApp.Common
{
    public static class Helper
    {
        public static string Spaceless(string word)
        {
            if (String.IsNullOrEmpty(word)) return string.Empty;

            return word.Replace(" ", string.Empty);
        }

        public static bool IsPalindrome(string word)
        {
            var spaceless = Spaceless(word);
            var palindrom = String.Join(string.Empty, spaceless.Reverse());
            bool isPali = palindrom.Equals(spaceless, StringComparison.OrdinalIgnoreCase);

            Console.WriteLine(isPali ?
                $"The word '{word}' is a Palindrome: {palindrom}" :
                $"The word '{word}' is not a Palindrome: {palindrom}");

            return isPali;
        }

        public static void CustomSort(int[] arr)
        {
            var numbers = arr
                .GroupBy(x => x)
                .OrderBy(x => x.Key)
                .OrderBy(x => x.Count())
                .SelectMany(x => x);

            // Input:  8, 2, 2, 7, 5, 1, 8, 5, 3, 5
            // Output: 1, 3, 7, 2, 2, 8, 8, 5, 5, 5
            Console.Write(string.Join(',', numbers));
        }

        public static int Fibonatchi(int position)
        {
            if (position == 0)
            {
                return 1;
            }
            if (position == 1)
            {
                return 1;
            }
            else
            {
                return Fibonatchi(position - 2) + Fibonatchi(position - 1);
            }
        }

        public static string IntegerToRoman(int number)
        {
            var arrRomans = new string[] {
                "M", "CM" ,"D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I"
            };

            var arrValues = new int[] {
                1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1
            };

            if (number <= 0)
            {
                Console.WriteLine("Input number has to be positive!");
                return String.Empty;
            }

            StringBuilder sb = new StringBuilder();
            int i = 0;
            int original = number;

            while (number > 0)
            {
                if (number - arrValues[i] >= 0)
                {
                    sb.Append(arrRomans[i]);
                    number -= arrValues[i];
                }
                else
                {
                    i++;
                }
            }

            Console.WriteLine($"{original} in Roman is {sb}");

            return sb.ToString();
        }

        public static void SwapNumbers(int x, int y)
        {
            Console.WriteLine($"Before: x={x}, y={y}");

            x ^= y;
            y = x ^ y;
            x ^= y;

            Console.WriteLine($"After: x={x}, y={y}");

        }

        public static void FizzBuzz()
        {
            var arrNumbers = Enumerable.Range(1, 100);
            bool isFizz;
            bool isBuzz;

            foreach (var number in arrNumbers)
            {
                isFizz = number % 3 == 0;
                isBuzz = number % 5 == 0;

                if (isFizz && isBuzz)
                {
                    Console.WriteLine("Fizbuzz");
                }
                else if (isFizz)
                {
                    Console.WriteLine("Fiz");
                }
                else if (isBuzz)
                {
                    Console.WriteLine("Buzz");
                }
                else
                {
                    Console.WriteLine(number);
                }
            }

        }

        public static int GetLongestWord(string words)
        {
            var max = words
                .Split(new char[] { ' ', ',', '.' })
                .Select(x => x.Length)
                .Max();

            return max;
        }

        public static string ReverseWords(string words)
        {
            if (String.IsNullOrEmpty(words))
            {
                throw new ArgumentNullException(words, "Words input is required");
            }

            var arrReversed = words
                .Split(new char[] {' '})
                .Reverse();

            return String.Join(" ", arrReversed);
        }

        public static Dictionary<string, int> CountWords(string sentence)
        {
            var words = sentence
                .ToLower()
                .Split(new char[] { ' ', ',' })
                .Where(x => x.Length > 2)
                .GroupBy(x => x)
                .Select(x => new KeyValuePair<string, int>(x.Key, x.Count()))
                .OrderByDescending(x => x.Value)
                .ToDictionary(x => x.Key, x => x.Value);

            foreach (var word in words)
            { 
                Console.WriteLine($"{word.Key}: {word.Value} times");
            }

            return words;
        }

        public static Dictionary<string, T> Deserialize<T>(string json) where T : class
        {
            var jsonOptions = new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true,
                AllowTrailingCommas = true
            };
            var items = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, T>>(json, jsonOptions);

            return items;
        }

        public static IEnumerable<T> DeserializeJson<T>(string json) where T : class
        {
            var items = JsonConvert.DeserializeObject<IEnumerable<T>>(json);
            var jsonObj = items.ToJson();

            // Ensure JSON is valid and output to debug console
            Debug.WriteLine(JToken.Parse(jsonObj).ToString(Formatting.Indented));

            return items;
        }

        public static IEnumerable<T> Deserialize<T>(string path, string[] cols) where T : class
        {
            var items = new List<T>();
            var config = new ChoCSVRecordConfiguration();

            for (int i = 0; i < cols.Length; i++)
            {
                config.CSVRecordFieldConfigurations.Add(new ChoCSVRecordFieldConfiguration(cols[i], i+1));
            }

            foreach (var item in new ChoCSVReader<T>(path, config).WithFirstLineHeader())
            {
                items.Add(item);
            }

            return items;
        }

        public static DateTime GetRandomDate(DateTime from, DateTime to)
        {
            var rnd = new Random();
            var range = to - from;
            var randTimeSpan = new TimeSpan((long)(rnd.NextDouble() * range.Ticks));

            return from + randTimeSpan;
        }

        public static Tuple<double, double> FindRoots(double a, double b, double c)
        {
            int m;
            double r1, r2, d1;
            d1 = b * b - 4 * a * c;
            if (a == 0)
                m = 1;
            else if (d1 > 0)
                m = 2;
            else if (d1 == 0)
                m = 3;
            else
                m = 4;

            switch (m)
            {
                case 1:
                case 2:
                    r1 = (-b + Math.Sqrt(d1)) / (2 * a);
                    r2 = (-b - Math.Sqrt(d1)) / (2 * a);
                    return new Tuple<double, double>(r1, r2);
                case 3:
                    r1 = (-b) / (2 * a);
                    return new Tuple<double, double>(r1, r1);
                default:
                    r1 = (-b) / (2 * a);
                    r2 = Math.Sqrt(-d1) / (2 * a);
                    return new Tuple<double, double>(r1, r2);
            }
        }
    }
}
