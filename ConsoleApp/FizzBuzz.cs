using System;
using System.IO;

namespace ConsoleApp
{
    public enum OutputType
    {
        Console,
        File
    }

    public class FizzBuzz
    {
        private OutputType outputType;
        private StreamWriter file;
        private int x;
        private int y;

        public void Start(int x, OutputType outputType)
        {
            this.outputType = outputType;
            this.x = x;
            this.y = 0;

            if (outputType == OutputType.File)
            {
                try
                {
                    file = new StreamWriter(File.Create("output.txt"));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            while (y < x)
                Go();

            if (outputType == OutputType.File)
                file.Close();
        }

        private void Go()
        {
            if (y % 5 == 0 && y % 3 == 0)
            {
                if (outputType == OutputType.File)
                    file.WriteLine("FizzBuzz");
                else
                    Console.WriteLine("FizzBuzz");
            }
            else if (y % 3 == 0)
            {
                if (outputType == OutputType.File)
                    file.WriteLine("Fizz");
                else
                    Console.WriteLine("Fizz");
            }
            else if (y % 5 == 0)
            {
                if (outputType == OutputType.File)
                    file.WriteLine("Buzz");
                else
                    Console.WriteLine("Buzz");
            }
            else
            {
                if (outputType == OutputType.File)
                    file.WriteLine(y);
                else
                    Console.WriteLine(y);
            }

            y++;
        }
    }
}
