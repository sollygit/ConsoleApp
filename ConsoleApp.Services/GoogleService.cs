using ConsoleApp.Models;
using System;
using System.Threading.Tasks;

namespace ConsoleApp.Services
{
    public static class GoogleService
    {
        public static async Task YouTubeSearch(string key, string query, int maxResults = 50)
        {
            try
            {
                await new YouTube().Search(key, query, maxResults);
            }

            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    Console.WriteLine(e.Message);
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
