using System;
using System.Collections.Generic;

namespace IDPFLibrary.DTO
{
    /// <summary>
    /// SCloud class.
    /// Contains information about cloud provider.
    /// </summary>
    public class SCloud
    {
        #region properties

        /// <summary>
        /// Cloud ID.
        /// </summary>
        public int CloudId { get; set; }

        /// <summary>
        /// Auth token.
        /// </summary>
        public String Token { get; set; }

        /// <summary>
        /// Secret token.
        /// </summary>
        public String TokenSecret { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// SCloud class constructor.
        /// </summary>
        /// <param name="cloudId">Cloud ID.</param>
        /// <param name="token">Auth token.</param>
        /// <param name="tokenSecret">Secret token.</param>
        public SCloud(int cloudId, string token, string tokenSecret)
        {
            CloudId = cloudId;
            Token = token;
            TokenSecret = tokenSecret;
        }

        #endregion
    }

    /// <summary>
    /// SAccount class.
    /// Contains information about an account.
    /// </summary>
    public class SAccount
    {
        #region properties

        /// <summary>
        /// Account name.
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// List of connected clouds.
        /// </summary>
        public List<SCloud> clouds { get; set; }

        /// <summary>
        /// Account ID.
        /// </summary>
        public int AccountId { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// SAccount class constructor.
        /// </summary>
        /// <param name="name">Account name.</param>
        /// <param name="clouds">List of connected clouds.</param>
        /// <param name="accountId">Account ID.</param>
        public SAccount(string name, List<SCloud> clouds, int accountId)
        {
            Name = name;
            this.clouds = clouds;
            AccountId = accountId;
        }

        #endregion
       
    }

    /// <summary>
    /// GetDeviceAccountsDTO class.
    /// </summary>
    public class GetDeviceAccountsDTO
    {
        #region properties

        /// <summary>
        /// List of connected accounts.
        /// </summary>
        public List<SAccount> Accounts { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// GetDeviceAccountsDTO class constructor.
        /// </summary>
        public GetDeviceAccountsDTO()
        {

        }

        /// <summary>
        /// GetDeviceAccountsDTO class constructor.
        /// </summary>
        /// <param name="accounts">List of connected accounts.</param>
        public GetDeviceAccountsDTO(List<SAccount> accounts)
        {
            Accounts = accounts;
        }

        #endregion
    }
}
