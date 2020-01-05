using ConsoleApp.Models;
using System.Threading.Tasks;

namespace ConsoleApp.Services
{
    public interface IFacebookService
    {
        Task<string> GetAccount(string accessToken);
        Task<string> Search(string accessToken, string query);
    }

    public class FacebookService : IFacebookService
    {
        private readonly IFacebookClient _facebookClient;

        public FacebookService(IFacebookClient facebookClient)
        {
            _facebookClient = facebookClient;
        }

        public async Task<string> GetAccount(string accessToken)
        {
            return await _facebookClient.Get(accessToken, "me", "id,name,email,gender,birthday,picture");
        }

        public async Task<string> Search(string accessToken, string query)
        {
            return await _facebookClient.Get(accessToken, "search", query, "place");
        }
    }
}
