using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Services
{
    public class DeviceService
    {
        //number of characters in a pair code
        private const int CODESIZE = 7;
        private ApplicationContext db = new ApplicationContext();
        public DeviceService()
        {
                
        }

        //this method will return all devices connected to the user with names that are connected to this account
        //there is a workaround with select here since either entity framework or linq is bugged and tends to return null in Account and device fields
        public async Task<List<DeviceName>> GetDevices(string username)
        {
            List<DeviceName> actualDdeviceNames = new List<DeviceName>();
            var deviceNames = await db.DeviceNames.Select(dn => new { dn.Device, dn.Account, dn.CustomDeviceName }).Where(n => n.Account.Login == username).ToListAsync();
            foreach(var dev in deviceNames)
            {
                DeviceName newDeviceName = new DeviceName(dev.Account, dev.Device, dev.CustomDeviceName);
                actualDdeviceNames.Add(newDeviceName);
            }
            return actualDdeviceNames;
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
                await db.SaveChangesAsync();
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
                    await db.SaveChangesAsync();
                    return "Success";
                }
            }
            return "Failed to pair with device. We could not find a device with that pair code. You can generate a new code by pressing the pair device button on your digital photo frame";
        }

        //returns new genreated code of lentght CODESIZE (7) on success. If the device cannot be found it returns an empty string
        public async Task<string> GeneratePairCode(int deviceId)
        {
            string code = "";
            Device device = await db.Devices.FindAsync(deviceId);
            if (device != null)
            {
                char[] chars = new char[62];
                chars =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
                byte[] data = new byte[1];
                using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
                {
                    crypto.GetNonZeroBytes(data);
                    data = new byte[CODESIZE];
                    crypto.GetNonZeroBytes(data);
                }
                StringBuilder result = new StringBuilder(CODESIZE);
                foreach (byte b in data)
                {
                    result.Append(chars[b % (chars.Length)]);
                }
                code = result.ToString();
                device.ConnectionCode = code;
                db.Entry(device).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
            }
            return code;
        }
    }
}