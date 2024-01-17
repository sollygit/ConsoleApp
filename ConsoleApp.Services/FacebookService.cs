using ConsoleApp.Models;
using System.Threading.Tasks;

namespace ConsoleApp.Services
{
    public interface IFacebookService
    {
        Task<string> GetProfile(string accessToken);
    }

    public class FacebookService : IFacebookService
    {
        private readonly IFacebookClient _facebookClient;

        public FacebookService(IFacebookClient facebookClient)
        {
            _facebookClient = facebookClient;
        }

        public async Task<string> GetProfile(string accessToken)
        {
            return await _facebookClient.Get(accessToken, "me", "id,name,email,gender,birthday,picture");
        }
    }
}
