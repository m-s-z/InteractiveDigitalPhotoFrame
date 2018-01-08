using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    /// <summary>
    /// enum with all providers
    /// onedrive and google drive are depreciated
    /// </summary>
    public enum ProviderType
    {
        /// <summary>
        /// google drive depreciated
        /// </summary>
        GoogleDrive = 0,
        /// <summary>
        /// onedrive depreciated
        /// </summary>
        OneDrive = 1,
        /// <summary>
        /// dropbox
        /// </summary>
        DropBox = 2,
        /// <summary>
        /// flickr
        /// </summary>
        Flicker = 3
    }
    /// <summary>
    /// Cloud model class
    /// </summary>
    public class Cloud
    {
        /// <summary>
        /// constructor for Cloud
        /// </summary>
        public Cloud()
        {

        }
        /// <summary>
        /// constructor for Cloud
        /// </summary>
        /// <param name="provider">Provider type enum class</param>
        /// <param name="login">login</param>
        public Cloud(ProviderType provider, string login)
        {
            Provider = provider;
            Login = login;
        }

        /// <summary>
        /// constructor for Cloud class
        /// </summary>
        /// <param name="provider">Provider type enum class</param>
        /// <param name="login">login</param>
        /// <param name="password">password</param>
        /// <param name="id">id</param>
        public Cloud(string password, ProviderType provider, string login, int id)
        {
            Password = password;
            Provider = provider;
            Login = login;
            Id = id;
        }

        /// <summary>
        /// constructor for Cloud class
        /// </summary>
        /// <param name="provider">Provider type enum class</param>
        /// <param name="login">login</param>
        /// <param name="cloudId">cloud Id</param>
        /// <param name="password">password</param>
        /// <param name="id">id</param>
        public Cloud(int cloudId, string password, ProviderType provider, string login, int id)
        {
            Password = password;
            Provider = provider;
            Login = login;
            Id = id;
        }

        /// <summary>
        /// constructor for Cloud class
        /// </summary>
        /// <param name="provider">Provider type enum class</param>
        /// <param name="login">login</param>
        /// <param name="account">instance of Account class</param>
        /// <param name="token">token</param>
        /// <param name="tokenSecret">token secret</param>
        /// <param name="flickrUserId"> flickr userId</param>
        public Cloud(ProviderType provider, string login, Account account, string token, string tokenSecret, String flickrUserId)
        {
            Provider = provider;
            Login = login;
            Account = account;
            Token = token;
            TokenSecret = tokenSecret;
            FlickrUserId = flickrUserId;
        }

        /// <summary>
        /// flickr user id
        /// </summary>
        public String FlickrUserId { get; set; }
        /// <summary>
        /// password
        /// </summary>
        public String Password { get; set; }
        /// <summary>
        /// instance of provider enum type
        /// </summary>
        public ProviderType Provider { get; set; }
        /// <summary>
        /// login
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// instance of Account model class
        /// </summary>
        public Account Account { get; set; }
        /// <summary>
        /// token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// token secret
        /// </summary>
        public string TokenSecret { get; set; }

    }
}