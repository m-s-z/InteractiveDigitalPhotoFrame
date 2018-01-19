using System.Threading.Tasks;
using System.Web;

namespace WebApplication.Services
{
    /// <summary>
    /// interface for authentication service
    /// </summary>
    public interface IAuthenticationService
    {
        #region methods
        /// <summary>
        /// method for changing an accounts password
        /// </summary>
        /// <param name="oldPassword">old password</param>
        /// <param name="newPassword">new password</param>
        /// <param name="username">username</param>
        /// <returns>
        /// true on success
        /// false account cannot befound
        /// false if oldpassword does not match the database hash
        /// </returns>
        Task<bool> ChangePassword(string oldPassword, string newPassword, string username);
        /// <summary>
        /// method for changing an accounts password
        /// </summary>
        /// <param name="oldPassword">old password</param>
        /// <param name="newPassword">new password</param>
        /// <param name="username">username</param>
        /// <returns>
        /// true on success
        /// false account cannot befound
        /// false if oldpassword does not match the database hash
        /// </returns>
        Task<bool> AppChangePassword(string oldPassword, string newPassword, string username);
        /// <summary>
        /// method for getting account model class
        /// </summary>
        /// <param name="id">id of account</param>
        /// <returns>
        /// Account model class
        /// </returns>
        Task<string> GetAccountLogin(int id);
        /// <summary>
        /// method for retrieving username from session
        /// </summary>
        /// <param name="session">Http Session object</param>
        /// <returns>
        /// string with username on success
        /// null if no user is authenticated
        /// </returns>
        string getLoggedInUsername(HttpSessionStateBase session);
        /// <summary>
        /// checks if the session contains an uthenticated user
        /// </summary>
        /// <param name="Session">Http Session object</param>
        /// <returns>
        /// true if there is an authenticated instance
        /// false if there is no authenticated instance
        /// </returns>
        bool IsAuthenticated(HttpSessionStateBase Session);
        /// <summary>
        /// function used to authenticate into the service
        /// </summary>
        /// <param name="username">accounts username</param>
        /// <param name="password">password assigned to account</param>
        /// <returns>true on successfull authentication
        /// false on unsucessfull authentication</returns>
        Task<bool> Login(string username, string password);
        /// <summary>
        /// function used to authenticate into the service
        /// </summary>
        /// <param name="username">accounts username</param>
        /// <param name="password">password assigned to account</param>
        /// <returns>true on successfull authentication
        /// false on unsucessfull authentication</returns>
        Task<bool> AppLogin(string username, string password);
        /// <summary>
        /// method for registering a new account
        /// </summary>
        /// <param name="login">login</param>
        /// <param name="password">password</param>
        /// <returns>
        /// returns a string with result message
        /// </returns>
        Task<string> RegisterAccount(string login, string password);
        /// <summary>
        /// method for registering a new account
        /// </summary>
        /// <param name="login">login</param>
        /// <param name="password">password</param>
        /// <returns>
        /// returns a string with result message
        /// </returns>
        Task<bool> AppRegisterAccount(string login, string password);
        /// <summary>
        /// method for obtaining user id
        /// </summary>
        /// <param name="session">session</param>
        /// <returns>id of logged in account</returns>
        Task<int> GetAccountId(HttpSessionStateBase session);
        #endregion methods
    }
}