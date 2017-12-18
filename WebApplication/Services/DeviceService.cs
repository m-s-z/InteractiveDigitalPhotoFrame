using IDPFLibrary.DTO;
using IDPFLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using FlickrNet;
using WebApplication.Data;
using WebApplication.Models;
using WebApplication.Utils;

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
                List<Cloud> clouds = await db.Clouds.Where(c => c.Account.Login == userName).ToListAsync<Cloud>();
                foreach(var c in clouds)
                {
                    List<Folder> folders = await db.Folders.Where(f => f.DeviceId == deviceId && f.CloudId == c.Id).ToListAsync<Folder>();
                    foreach(var f in folders)
                    {
                        db.Folders.Remove(f);
                        db.Entry(f).State = System.Data.Entity.EntityState.Modified;
                        await db.SaveChangesAsync();
                    }

                }
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
        public async Task<string> GeneratePairCode(int deviceId, string deviceToken)
        {
            string code = "";
            Device device = await db.Devices.FindAsync(deviceId);
            if (device != null)
            {
                if (device.DeviceToken == deviceToken)
                {
                    code = TrueRandomString(CODESIZE);                    device.ConnectionCode = code;
                    db.Entry(device).State = System.Data.Entity.EntityState.Modified;
                    await db.SaveChangesAsync();
                }
            }
            return code;
        }
        public async Task<CreateNewDeviceDTO> CreateDevice(string code)
        {
            CreateNewDeviceDTO dto = null;
            if(code == RegistrationCode.CODE)
            {
                bool flag = true;
                ICollection<Account> accounts = new List<Account>();
                string token = "";
                //check if generated string already exists i need the string to be unique else i cant retrieve id
                while (flag)
                {
                    token = TrueRandomString(CODESIZE);
                    Device temp = await db.Devices.FirstOrDefaultAsync(d => d.DeviceToken == token);
                    if(temp == null)
                    {
                        flag = false;
                    }

                }
                string name = "";

                Device device = new Device(token, name, accounts);
                db.Devices.Add(device);
                await db.SaveChangesAsync();
                device = await db.Devices.FirstOrDefaultAsync(d => d.DeviceToken == token);
                dto = new CreateNewDeviceDTO(device.DeviceId, device.DeviceToken);
            }
            return dto;
        }
        private string TrueRandomString(int lenght)
        {
            char[] chars = new char[62];
            chars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[lenght];
                crypto.GetNonZeroBytes(data);
            }
            StringBuilder result = new StringBuilder(lenght);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }

        public async Task<GetDeviceAccountsDTO> GetDeviceAccounts(int deviceId, string deviceToken)
        {
            GetDeviceAccountsDTO dto = null;
            Device device = await db.Devices.FindAsync(deviceId);
            if(device != null)
            {
                if(device.DeviceToken == deviceToken)
                {
                    List<SAccount> sAccounts = new List<SAccount>();
                    foreach(var acc in device.Accounts)
                    {
                        List<Cloud> clouds = await db.Clouds.Where(c => c.Account.Id == acc.Id).ToListAsync();
                        List<SCloud> sClouds = new List<SCloud>();
                        foreach(var c in clouds)
                        {
                            SCloud sCloud = new SCloud(c.Id, c.Token, c.TokenSecret);
                            sClouds.Add(sCloud);
                        }
                        SAccount sacc = new SAccount(acc.Login,sClouds,acc.Id);
                        sAccounts.Add(sacc);
                    }
                    dto = new GetDeviceAccountsDTO(sAccounts);
                }
            }
            return dto;
        }
        public async Task<bool> DeviceIsAuthenticated(int deviceId, string deviceToken)
        {
            Device device = await db.Devices.FindAsync(deviceId);
            return device.DeviceToken == deviceToken;
        }

        public async Task<List<string>> GetAllFlickrPhotosUrl(List<int> accountIds, int deviceId)
        {
            List<string> photoUrlList = new List<string>();

            FlickrManager flickrManager = new FlickrManager();

            FolderService folderService = new FolderService();

            try
            {
                foreach (int accountId in accountIds)
                {
                    List<Cloud> cloudList = await db.Clouds.Where(p => p.Account.Id == accountId).ToListAsync<Cloud>();

                    foreach (var cloud in cloudList)
                    {
                        Flickr flickr = await flickrManager.GetAuthInstance(cloud.Id);
                        List<Photoset> photosetList = await folderService.GetDeviceFlickrFolders(cloud.Id, deviceId);
                        foreach (var photoset in photosetList)
                        {
                            var photoCollection = flickr.PhotosetsGetPhotos(photoset.PhotosetId);
                            foreach (var photo in photoCollection)
                            {
                                photoUrlList.Add(photo.LargeUrl);
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                photoUrlList = null;
            }
            return photoUrlList;
        }
    }
}