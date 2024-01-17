using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ConsoleApp.Models
{
    public interface IFacebookClient
    {
        Task<string> Get(string accessToken, string endpoint, string args);
    }

    public class FacebookClient : IFacebookClient
    {
        private readonly HttpClient _httpClient;

        public FacebookClient(string uri)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(uri) };
            _httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> Get(string accessToken, string endpoint, string args)
        {
            var uri = $"{endpoint}?fields={args}&access_token={accessToken}";
            var response = await _httpClient.GetAsync(uri);

            Console.WriteLine($"{_httpClient.BaseAddress}{uri}");

            return await response.Content.ReadAsStringAsync();
        }
    }
}
