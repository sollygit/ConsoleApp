using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp.Models
{
    public class YouTube
    {
        public async Task Search(string apiKey, string query, int maxResults)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = apiKey,
                ApplicationName = this.GetType().ToString()
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = query;
            searchListRequest.MaxResults = maxResults;

            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = await searchListRequest.ExecuteAsync();

            var videos = new List<string>();
            var channels = new List<string>();
            var playlists = new List<string>();

            // Add each result to the appropriate list, and then display the lists of matching videos, channels, and playlists.
            foreach (var searchResult in searchListResponse.Items)
            {
                switch (searchResult.Id.Kind)
                {
                    case "youtube#video":
                        videos.Add($"{searchResult.Snippet.Title} ({searchResult.Id.VideoId})");
                        break;

                    case "youtube#channel":
                        channels.Add($"{searchResult.Snippet.Title} ({searchResult.Id.ChannelId})");
                        break;

                    case "youtube#playlist":
                        playlists.Add($"{searchResult.Snippet.Title} ({searchResult.Id.PlaylistId})");
                        break;
                }
            }

            Console.WriteLine($"Videos:\n{string.Join("\n", videos)}\n");
            Console.WriteLine($"Channels:\n{string.Join("\n", channels)}\n");
            Console.WriteLine($"Playlists:\n{string.Join("\n", playlists)}\n");
        }
    }
}
