using FlickrNet;
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
    /// cloud service class for manipulating the database in regards to cloud
    /// </summary>
    public class CloudService : ICloudService
    {

        #region fields
        /// <summary>
        /// instance of application context to manipulate the database
        /// </summary>
        private ApplicationContext db = new ApplicationContext();
        #endregion fields
        /// <summary>
        /// constructor for CloudService class
        /// </summary>
        public CloudService()
        {

        }
        #region methods
        /// <summary>
        /// method for obtaining all clouds connected to given username
        /// </summary>
        /// <param name="username">username</param>
        /// <returns>
        /// List of clouds
        /// in case no account was found an empty list  is returned
        /// </returns>
        public async Task<List<Cloud>> GetClouds(string username)
        {
            try
            {
                var temp = db.Clouds.Where(c => c.Account.Login == username);
                List<Cloud> clouds = await temp.ToListAsync<Cloud>();
                return clouds;
            }catch(Exception e)
            {
                return new List<Cloud>();
            }

        }

        /// <summary>
        /// removes a cloud from the database based on id
        /// </summary>
        /// <param name="cloudId">cloud id</param>
        /// <returns>
        /// true on success
        /// false if account cannot be found
        /// </returns>
        public async Task<bool> removeCloud(int cloudId)
        {
            Cloud cloud = await db.Clouds.FindAsync(cloudId);
            if (cloud != null)
            {
                db.Clouds.Remove(cloud);
                int check = await db.SaveChangesAsync();
                return true;
            }
            return false;
        }
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
        public async Task<bool> ChangePassword(string oldPassword, string newPassword, int cloudId )
        {
            Cloud cloud = await db.Clouds.FindAsync(cloudId);
            if (cloud != null)
            {
                if (cloud.Password == oldPassword)
                {
                    cloud.Password = newPassword;
                    db.Entry(cloud).State = System.Data.Entity.EntityState.Modified;
                    await db.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }
        /*
        //legacy method left as reference do not use
        public async Task<bool> CreateNewAccount(ProviderType provider, OAuthAccessToken token)
        {
            bool result = false;
            switch(provider)
            {
                case ProviderType.Flicker:
              //      result = await CreateFlickerAccount();
                    break;
                default:
                    break;
            }
            return result;
        }
        */
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
        public async Task<bool> CreateFlickerAccount(OAuthAccessToken token, string accountName, string username)
        {
            try
            {
                Account foundUser = await db.Accounts.FirstOrDefaultAsync(a => a.Login == username);
                Cloud cloud = new Cloud(ProviderType.Flicker, accountName, foundUser, token.Token, token.TokenSecret, token.UserId);
                db.Clouds.Add(cloud);
                await db.SaveChangesAsync();
            }catch(Exception e)
            {
                return false;
            }
            return true;
        }
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
        public async Task<bool> CreateDropBoxAccount(string token, string accountName, string username, string userId)
        {
            try
            {
                Account foundUser = await db.Accounts.FirstOrDefaultAsync(a => a.Login == username);
                Cloud cloud = new Cloud(ProviderType.DropBox, accountName, foundUser, token, "", userId);
                db.Clouds.Add(cloud);
                await db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
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
        public async Task<string> AppCreateCloud(int accountId, string cloudName, CloudProviderType provider, string token, string tokenSecret, string userId)
        {
            Cloud cloud = new Cloud();
            Account account = await db.Accounts.FindAsync(accountId);
            if(account == null)
            {
                return "Account couldnt be found";
            }
            if(provider == CloudProviderType.Flickr)
            {
                cloud = new Cloud(ProviderType.Flicker, cloudName, account, token, tokenSecret, userId);
            }
            else if(provider == CloudProviderType.Dropbox)
            {
                cloud = new Cloud(ProviderType.DropBox, cloudName, account, token, tokenSecret, userId);
            }
            db.Clouds.Add(cloud);
            try
            {
                int check = await db.SaveChangesAsync();
            }catch(Exception e)
            {
                return "Cloud could not be added";
            }
            return "Success";
        }

        /// <summary>
        /// method for getting cloud model class instance
        /// </summary>
        /// <param name="cloudId">cloud id of the cloud to retrieve</param>
        /// <returns>
        /// Cloud model instance
        /// </returns>
        public async Task<Cloud> GetCloud(int cloudId)
        {
            Cloud cloud = await db.Clouds.FindAsync(cloudId);
            return cloud;
        }
        #endregion methods
    }
}