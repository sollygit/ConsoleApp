using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public static class AsyncUtil
    {
        public static async Task GetSequenceAsync(CancellationToken token)
        {
            await foreach (var number in GenerateSequence())
            {
                Console.WriteLine($"The time is {DateTime.Now:hh:mm:ss}. Retrieved {number}");
            }
        }

        private static async IAsyncEnumerable<int> GenerateSequence()
        {
            for (int i = 0; i < 10; i++)
            {
                // every 3 elements, wait 2 sec.
                if (i % 3 == 0)
                    await Task.Delay(2000);
                yield return i;
            }
        }
    }
}
