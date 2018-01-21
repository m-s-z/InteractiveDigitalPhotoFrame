using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    /// <summary>
    /// class for authorization of AAA requests
    /// </summary>
    public class AuthorizationToken
    {
        #region properties
        /// <summary>
        /// id
        /// </summary>
        public int AuthorizationTokenId { get; set; }
        /// <summary>
        /// account for which the token is generated
        /// </summary>
        public Account Account { get; set; }
        /// <summary>
        /// token for authorization
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// date until which the token is valid
        /// </summary>
        public DateTime ExpirationDate { get; set; }
        #endregion properties
        /// <summary>
        /// contructor for AuthorizationToken
        /// </summary>
        public AuthorizationToken()
        {

        }
        /// <summary>
        /// contructor for AuthorizationToken
        /// </summary>
        /// <param name="account">instance of Account model class</param>
        /// <param name="token">string token used for authorization</param>
        /// <param name="expirationDate">expiration date</param>
        public AuthorizationToken(Account account, string token, DateTime expirationDate)
        {
            Account = account;
            Token = token;
            ExpirationDate = expirationDate;
        }
    }
}