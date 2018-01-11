using System.Collections.Generic;
using System.Threading.Tasks;
using FlickrNet;
using IDPFLibrary;
using WebApplication.Models;

namespace WebApplication.Services
{
    /// <summary>
    /// interface for cloud service
    /// </summary>
    public interface ICloudService
    {
        #region methods
        /// <summary>
        /// method for changing password. Dedpreciated do not use
        /// </summary>
        /// <param name="oldPassword">old password</param>
        /// <param name="newPassword">new password</param>
        /// <param name="cloudId">cloud id</param>
        /// <returns>
        /// true on success
        /// false if cloud cannot be found
        /// </returns>
        Task<bool> ChangePassword(string oldPassword, string newPassword, int cloudId);
        /// <summary>
        /// method for creating dropbox cloud model and adding it to the database
        /// </summary>
        /// <param name="token">token later used to authenticate requests</param>
        /// <param name="accountName">new custom cloud name</param>
        /// <param name="username">username of account to which we should add the cloud</param>
        /// <param name="userId">userid specific to dropbox</param>
        /// <returns>
        /// true on success
        /// false otherwise
        /// </returns>
        Task<bool> CreateDropBoxAccount(string token, string accountName, string username, string userId);
        /// <summary>
        /// method for creating flickr cloud model and adding it to the database
        /// </summary>
        /// <param name="token">token later used to authenticate requests</param>
        /// <param name="accountName">new custom cloud name</param>
        /// <param name="username">username of account to which we should add the cloud</param>
        /// <returns>
        /// true on success
        /// false otherwise
        /// </returns>
        Task<bool> CreateFlickerAccount(OAuthAccessToken token, string accountName, string username);
        /// <summary>
        /// method for getting cloud model class instance
        /// </summary>
        /// <param name="cloudId">cloud id of the cloud to retrieve</param>
        /// <returns>
        /// Cloud model instance
        /// </returns>
        Task<Cloud> GetCloud(int cloudId);
        /// <summary>
        /// method for obtaining all clouds connected to given username
        /// </summary>
        /// <param name="username">username</param>
        /// <returns>
        /// List of clouds
        /// in case no account was found an empty list  is returned
        /// </returns>
        Task<List<Cloud>> GetClouds(string username);
        /// <summary>
        /// removes a cloud from the database based on id
        /// </summary>
        /// <param name="cloudId">cloud id</param>
        /// <returns>
        /// true on success
        /// false if account cannot be found
        /// </returns>
        Task<bool> removeCloud(int cloudId);
        /// <summary>
        /// Method for adding a new cloud to the Database
        /// </summary>
        /// <param name="accountId">account id</param>
        /// <param name="cloudName">coustom cloud name</param>
        /// <param name="provider">provider of type CloudProviderType</param>
        /// <param name="token">token</param>
        /// <param name="tokenSecret">token secret</param>
        /// <param name="userId">user id</param>
        /// <returns>
        /// string response
        /// </returns>
        Task<string> AppCreateCloud(int accountId, string cloudName, CloudProviderType provider, string token, string tokenSecret, string userId);

        #endregion methods
    }
}