using System;
using System.Configuration;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public static class GoogleDataApi
    {
        public static async Task YouTubeDataApiSearch()
        {
            var key = ConfigurationManager.AppSettings["YoutubeApiKey"];
            var query = "Queen - Bohemian Rhapsody";
            var maxResults = 50;

            try
            {
                await new YouTubeSearch().Run(key, query, maxResults);
            }

            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
