using System;
using System.Threading;

namespace ConsoleApp.Models
{
    public interface IGenerate
    {
        int Counter { get; set; }
        void GeneateRandom(int numberToFind, int maxValue);
    }

    public class Loto: IGenerate
    {
        public delegate void SevenBoom(object sender ,PropertyEventArgs e);
        public event SevenBoom OnSevenBoom;

        public int Counter { get; set; }

        public void InvokeSevenBoom(object sender, PropertyEventArgs e)
        {
            if (OnSevenBoom != null)
            {
                OnSevenBoom(sender, e);
            }

            Counter = 0;
        }

        public void GeneateRandom(int numberToFind, int maxValue)
        {
            Counter++;
            
            var number = new Random().Next(maxValue);

            if (number % numberToFind == 0)
            {
                Console.Write($"{number} - Boom!");
                InvokeSevenBoom(this, new PropertyEventArgs(Counter));
            }
            else
            {
                Console.WriteLine(number);
            }
            Thread.Sleep(100);
        }
    }

    public class PropertyEventArgs
    {
        public int Count { get; set; }

        public PropertyEventArgs(int count)
        {
            Count = count;
        }
    }

    public static class SevenBoom
    {
        static readonly int winnerNumber = 7;
        static readonly int poolOfNumbers = 100;

        public static void Run()
        {
            var loto = new Loto();
            loto.OnSevenBoom += OnSevenBoom;

            while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
            {
                loto.GeneateRandom(winnerNumber, poolOfNumbers);
            }
        }

        private static void OnSevenBoom(object sender, PropertyEventArgs e)
        {
            Console.WriteLine($"({e.Count} steps)");
        }
    }
}
