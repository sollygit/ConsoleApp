using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public interface IFacebookClient
    {
        Task<string> GetAsync(string accessToken, string endpoint, string args = null);
        Task<string> GetAsync(string accessToken, string endpoint, string query, string type, string args = null);
    }

    public class FacebookClient : IFacebookClient
    {
        private readonly HttpClient _httpClient;

        public FacebookClient()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["FacebookApiUri"]) };
            _httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> GetAsync(string accessToken, string endpoint, string args = null)
        {
            var uri = $"{endpoint}?access_token={accessToken}&{args}";
            var response = await _httpClient.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
                return string.Empty;

            var result = await response.Content.ReadAsStringAsync();

            return result;
        }

        public async Task<string> GetAsync(string accessToken, string endpoint, string query, string type, string args = null)
        {
            var uri = $"{endpoint}?access_token={accessToken}&q={query}&type={type}&limit=10&{args}";
            var response = await _httpClient.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
                return string.Empty;

            var result = await response.Content.ReadAsStringAsync();

            return result;
        }
    }
}
