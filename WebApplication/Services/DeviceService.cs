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
    public class DeviceService
    {
        private ApplicationContext db = new ApplicationContext();
        public DeviceService()
        {
                
        }

        //this method will return all devices connected to the user with names that are connected to this account
        public async Task<List<DeviceName>> GetDevices(string username)
        {
            List<DeviceName> deviceNames = await db.DeviceNames.Where(n => n.Account.Login == username).ToListAsync();
            return deviceNames;
            
        }
    }
}