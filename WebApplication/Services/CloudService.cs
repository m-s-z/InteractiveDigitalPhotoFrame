using FlickrNet;
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
    public class CloudService : ICloudService
    {
        private ApplicationContext db = new ApplicationContext();
        public CloudService()
        {

        }
        //gets all clouds connected to account in case username has no clouds assigned it returns an empty list.
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
        //deletes clodu with specified id and return true on success and false if the cloud cannot be found
        public async Task<bool> removeCloud(int cloudId)
        {
            Cloud cloud = await db.Clouds.FindAsync(cloudId);
            if (cloud != null)
            {
                db.Clouds.Remove(cloud);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }
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

        public async Task<Cloud> GetCloud(int cloudId)
        {
            Cloud cloud = await db.Clouds.FindAsync(cloudId);
            return cloud;
        }
    }
}