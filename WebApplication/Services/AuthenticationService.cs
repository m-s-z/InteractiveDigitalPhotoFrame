using IDPFLibrary;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Services
{
    /// <summary>
    /// authentication service class for session based authentication
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        #region fields
        /// <summary>
        /// instance of application context to manipulate the database
        /// </summary>
        private ApplicationContext db = new ApplicationContext();
        /// <summary>
        /// constructor for AuthenticationService class
        /// </summary>
        #endregion fields
        public AuthenticationService()
        {

        }
        #region methods
        /// <summary>
        /// function used to authenticate into the service
        /// </summary>
        /// <param name="username">accounts username</param>
        /// <param name="password">password assigned to account</param>
        /// <returns>true on successfull authentication
        /// false on unsucessfull authentication</returns>
        public async Task<bool> Login(string username, string password)
        {
            var foundUser = await db.Accounts.FirstOrDefaultAsync( u => u.Login.Equals(username.ToLower()));
            if (foundUser != null)
            {
                //if (foundUser.Password == password && foundUser.Login.ToLower() == username.ToLower())
                if (Account.PasswordEquals(password, foundUser.Password) && foundUser.Login == username.ToLower())
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// function used to authenticate into the service
        /// </summary>
        /// <param name="username">accounts username</param>
        /// <param name="password">password assigned to account</param>
        /// <returns>true on successfull authentication
        /// false on unsucessfull authentication</returns>
        public async Task<string> AppLogin(string username, string password)
        {
            var foundUser = await db.Accounts.FirstOrDefaultAsync(u => u.Login.Equals(username.ToLower()));
            if (foundUser != null)
            {
                //if (foundUser.Password == password && foundUser.Login.ToLower() == username.ToLower())
                if (Account.PasswordEquals(password, foundUser.Password) && foundUser.Login == username.ToLower())
                {
                    IDeviceService devService = new DeviceService();
                    string token = devService.TrueRandomString(10);
                    DateTime date = new DateTime();
                    date.AddHours(2);
                    AuthorizationToken auth = new AuthorizationToken(foundUser, token, date);
                    db.AuthorizationTokens.Add(auth);
                    await db.SaveChangesAsync();
                    return token;
                }
            }
            return "";
        }
        /// <summary>
        /// checks if the session contains an uthenticated user
        /// </summary>
        /// <param name="Session">Http Session object</param>
        /// <returns>
        /// true if there is an authenticated instance
        /// false if there is no authenticated instance
        /// </returns>
        public bool IsAuthenticated(HttpSessionStateBase Session)
        {
            if (Session["UserId"] == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// checks if the token is valid and has not expired
        /// </summary>
        /// <param name="token">authorization token</param>
        /// <param name="userId">userId</param>
        /// <returns></returns>
        public async Task<AuthorizationResponse> AppIsAuthenticated(string token, int userId)
        {
            AuthorizationToken auth = await db.AuthorizationTokens.FirstOrDefaultAsync(a => a.Account.Id == userId && a.Token == token);
            if (auth == null)
            {
                return AuthorizationResponse.InvalidToken;
            }
            else if (auth.ExpirationDate < DateTime.Now)
            {
                return AuthorizationResponse.Ok;
            }
            else
            {
                return AuthorizationResponse.TokenExpired;
            }
        }
        /// <summary>
        /// method for retrieving username from session
        /// </summary>
        /// <param name="session">Http Session object</param>
        /// <returns>
        /// string with username on success
        /// null if no user is authenticated
        /// </returns>
        public string getLoggedInUsername(HttpSessionStateBase session)
        {
            if (session["UserId"] != null)
            {
                return session["UserId"] as string;
            }else
            {
                return null;
            }
        }
        /// <summary>
        /// method for registering a new account
        /// </summary>
        /// <param name="login">login</param>
        /// <param name="password">password</param>
        /// <returns>
        /// returns a string with result message
        /// </returns>
        public async Task<bool> AppRegisterAccount(string login, string password)
        {
            bool result = false;
            string hashedPassword = password;
            string lowerCaseLogin = login.ToLower();
            List<Device> devices = new List<Device>();
            Account account = new Account(lowerCaseLogin, hashedPassword, devices);
            try
            {
                db.Accounts.Add(account);
                await db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// method for registering a new account
        /// </summary>
        /// <param name="login">login</param>
        /// <param name="password">password</param>
        /// <returns>
        /// returns a string with result message
        /// </returns>
        public async Task<string> RegisterAccount(string login, string password)
        {
            string result = "success";
            string hashedPassword = Account.HashPassword(password);
            string lowerCaseLogin = login.ToLower();
            List<Device> devices = new List<Device>();
            Account account = new Account(lowerCaseLogin, hashedPassword, devices);
            try
            {
                db.Accounts.Add(account);
                await db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                result = "An account with that username already exists please try another";
            }
            return result;
        }
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
        public async Task<bool> ChangePassword(string oldPassword, string newPassword, string username)
        {
            Account foundUser = await db.Accounts.FirstOrDefaultAsync(a => a.Login == username.ToLower());
            if(foundUser != null)
            {
                if(Account.PasswordEquals(oldPassword, foundUser.Password))
                {
                    foundUser.Password = Account.HashPassword(newPassword);
                    db.Entry(foundUser).State = System.Data.Entity.EntityState.Modified;
                    await db.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }
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
        public async Task<bool> AppChangePassword(string oldPassword, string newPassword, string username)
        {
            Account foundUser = await db.Accounts.FirstOrDefaultAsync(a => a.Login == username.ToLower());
            if (foundUser != null)
            {
                if (oldPassword == foundUser.Password)
                {
                    foundUser.Password = Account.HashPassword(newPassword);
                    db.Entry(foundUser).State = System.Data.Entity.EntityState.Modified;
                    await db.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// method for getting account model class
        /// </summary>
        /// <param name="id">id of account</param>
        /// <returns>
        /// Account model class
        /// </returns>
        public async Task<string> GetAccountLogin(int id)
        {
            Account account = await db.Accounts.FindAsync(id);
            return account.Login;
        }

        /// <summary>
        /// method for obtaining user id
        /// </summary>
        /// <param name="session">session</param>
        /// <returns>id of logged in account</returns>
        public async Task<int> GetAccountId(HttpSessionStateBase session)
        {
            if (session["UserId"] != null)
            {
                string userLogin = session["UserId"] as string;
                Account acc = await db.Accounts.FirstOrDefaultAsync(p => p.Login == userLogin);
                return acc.Id;
            }
            else
            {
                return -1;
            }
        }

        
        /// <summary>
        /// method for obtaining user id
        /// </summary>
        /// <param name="login"></param>
        /// <returns>id of logged in account</returns>
        public async Task<int> GetAccountId(string login)
        {
            Account acc = await db.Accounts.FirstOrDefaultAsync(p => p.Login == login);
            return acc.Id;
        }
        #endregion methods
    }
}