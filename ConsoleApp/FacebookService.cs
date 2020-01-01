using System.Threading.Tasks;

namespace ConsoleApp
{
    public interface IFacebookService
    {
        Task<string> GetAccount(string accessToken);
        Task<string> GetAccountList(string accessToken, string query);
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
            var result = await _facebookClient.GetAsync(accessToken, "me", "fields=id,name,gender,age_range,locale,link,picture");

            if (result == null)
            {
                return string.Empty;
            }

            return result;
        }

        public async Task<string> GetAccountList(string accessToken, string query)
        {
            var results = await _facebookClient.GetAsync(
                accessToken, "search", query, "user", "fields=id,name,link,picture");

            if (results == null)
            {
                return string.Empty;
            }

            return results;
        }
    }
}
