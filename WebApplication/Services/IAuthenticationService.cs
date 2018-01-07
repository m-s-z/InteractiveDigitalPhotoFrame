using System.Threading.Tasks;
using System.Web;

namespace WebApplication.Services
{
    public interface IAuthenticationService
    {
        Task<bool> ChangePassword(string oldPassword, string newPassword, string username);
        Task<string> GetAccountLogin(int id);
        string getLoggedInUsername(HttpSessionStateBase session);
        bool IsAuthenticated(HttpSessionStateBase Session);
        Task<bool> Login(string username, string password);
        Task<string> RegisterAccount(string login, string password);
    }
}