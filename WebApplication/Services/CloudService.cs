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
    }
}