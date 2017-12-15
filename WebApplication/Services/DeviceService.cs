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
            List<DeviceName> deviceNames = await db.DeviceNames.Where(n => n.Account.Login == username).ToListAsync<DeviceName>();
            List<Device> device = db.DeviceNames.Select(dn => dn.Device).ToList<Device>();
            foreach(var dev in deviceNames)
            {
                //there was a problem that in deviceNames list there null object this is a workaround although due to the fact that in db indexing starts at 1 i need to substract 1
                //dev.Device = device[dev.DeviceNameId -1];
            }
            return deviceNames;
        }

        public async Task<bool> UnpairDevice(int deviceId, string userName)
        {
            Device device = await db.Devices.FirstOrDefaultAsync(d => d.DeviceId == deviceId);
            var founduser = await db.Accounts.FirstOrDefaultAsync(u => u.Login == userName);
            if (device != null && founduser != null)
            {
                DeviceName deviceName = await db.DeviceNames.FirstOrDefaultAsync(dn => dn.Account.Id == founduser.Id && dn.Device.DeviceId == device.DeviceId);
                founduser.Devices.Remove(device);
                device.Accounts.Remove(founduser);
                db.Entry(founduser).State = System.Data.Entity.EntityState.Modified;
                db.Entry(device).State = System.Data.Entity.EntityState.Modified;
                if (deviceName != null)
                {
                    db.DeviceNames.Remove(deviceName);
                }
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<string> PairDevice(string code, string deviceName, string userName)
        {
            Device device = await db.Devices.FirstOrDefaultAsync(d => d.ConnectionCode == code);
            if(device != null)
            {
                var founduser = await db.Accounts.FirstOrDefaultAsync(u => u.Login == userName);
                if(founduser !=null)
                {
                    DeviceName newDeviceName = new DeviceName(founduser, device, deviceName);
                    db.DeviceNames.Add(newDeviceName);
                    founduser.Devices.Add(device);
                    device.Accounts.Add(founduser);
                    db.Entry(founduser).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(device).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return "Success";
                }
            }
            
            return "Failed to pair with device. We could not find a device with that pair code. You can generate a new code by pressing the pair device button on your digital photo frame";
            
        }
    }
}