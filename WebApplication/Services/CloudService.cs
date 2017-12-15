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
    public class CloudService
    {
        private ApplicationContext db = new ApplicationContext();
        public CloudService()
        {

        }
        //gets all clouds connected to account in case username has no clouds assigned it returns an empty list.
        public async Task<List<Cloud>> GetClouds(string username)
        {
            List<Cloud> clouds = await db.Clouds.Where(c => c.Account.Login == username).ToListAsync<Cloud>();
            return clouds;
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
    }
}