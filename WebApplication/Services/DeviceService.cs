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
using Dropbox.Api;

namespace WebApplication.Services
{
    /// <summary>
    /// cloud service class for manipulating the database in regards to cloud
    /// </summary>
    public class DeviceService : IDeviceService
    {
        #region fields
        /// <summary>
        /// instance of application context to manipulate the database
        /// </summary>
        private ApplicationContext db = new ApplicationContext();
        /// <summary>
        /// number of characters in paircode
        /// </summary>
        private const int CODESIZE = 7;
        #endregion fields
        /// <summary>
        /// contructor for DeviceService class
        /// </summary>
        public DeviceService()
        {
                
        }
        #region methods
        //there is a workaround with select here since either entity framework or linq is bugged and tends to return null in Account and device fields
        /// <summary>
        /// this method will return all devices connected to the user with names that are connected to this account
        /// </summary>
        /// <param name="username">username of account from which to retrieve devices</param>
        /// <returns>
        /// List of DeviceName model class
        /// returns an empty list if account cannot be found
        /// </returns>
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
        /// <summary>
        /// method for unpairing a device from an account
        /// </summary>
        /// <param name="deviceId">dedvice id</param>
        /// <param name="userName"> account username</param>
        /// <returns>
        /// true on success
        /// false if either device or user cannot be found
        /// </returns>
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

        /// <summary>
        /// method for conecting a device to an account
        /// </summary>
        /// <param name="code">pair code from device</param>
        /// <param name="deviceName">custom device name specific to account</param>
        /// <param name="userName">account username</param>
        /// <returns>
        /// string message based on result
        /// </returns>
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

        /// <summary>
        /// method for generating a pair code of CODESIZE (7)
        /// </summary>
        /// <param name="deviceId">device id</param>
        /// <param name="deviceToken">device token for authorization</param>
        /// <returns>
        /// string paircode on success
        /// empty string token does not match the database (authorization failure)
        /// </returns>
        public async Task<string> GeneratePairCode(int deviceId, string deviceToken)
        {
            string code = "";
            Device device = await db.Devices.FindAsync(deviceId);
            if (device != null)
            {
                if (device.DeviceToken == deviceToken)
                {
                    code = TrueRandomString(CODESIZE);
                    device.ConnectionCode = code;
                    db.Entry(device).State = System.Data.Entity.EntityState.Modified;
                    await db.SaveChangesAsync();
                }
            }
            return code;
        }
        /// <summary>
        /// method for creating a device
        /// </summary>
        /// <param name="code">secret application code</param>
        /// <returns>
        /// CreateNewDeviceDTO class
        /// </returns>
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
        /// <summary>
        /// method for creating a true random string
        /// </summary>
        /// <param name="lenght">string length</param>
        /// <returns>
        /// randomly generated string
        /// </returns>
        public string TrueRandomString(int lenght)
        {
            char[] chars = new char[62];
            chars =
            "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
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

        /// <summary>
        /// method for getting all accounts connected to given device
        /// </summary>
        /// <param name="deviceId">device id</param>
        /// <param name="deviceToken"> device token</param>
        /// <returns>
        /// GetDeviceAccountsDTO class
        /// </returns>
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

        /// <summary>
        /// method for chekcing if device token matches the database. Used for device authorization
        /// </summary>
        /// <param name="deviceId">device id</param>
        /// <param name="deviceToken">device token</param>
        /// <returns>
        /// true if token matches database token
        /// false otherwise
        /// </returns>
        public async Task<bool> DeviceIsAuthenticated(int deviceId, string deviceToken)
        {
            Device device = await db.Devices.FindAsync(deviceId);
            return device.DeviceToken == deviceToken;
        }
        /// <summary>
        /// method for getting all photo urls with thier respective metadata
        /// </summary>
        /// <param name="accountIds">list of accounts from which to extract photos from</param>
        /// <param name="deviceId">target device</param>
        /// <returns>
        /// GetAllFlickrPhotosURLResponseDTO class
        /// </returns>
        public async Task<GetAllFlickrPhotosURLResponseDTO> GetAllPhotosUrl(List<int> accountIds, int deviceId)
        {
            // there is a need to handle if token has expired or has been revoked
            List<Urls> photoUrlList = new List<Urls>();

            FlickrManager flickrManager = new FlickrManager();

            DropboxClient dbx;


            IFolderService folderService = new FolderService();

            try
            {
                foreach (int accountId in accountIds)
                {
                    List<Cloud> cloudList = await db.Clouds.Where(p => p.Account.Id == accountId).ToListAsync<Cloud>();

                    foreach (var cloud in cloudList)
                    {
                        try
                        {
                            if (cloud.Provider == ProviderType.Flicker)
                            {
                                Flickr flickr = await flickrManager.GetAuthInstance(cloud.Id);
                                List<Photoset> photosetList = await folderService.GetDeviceFlickrFolders(cloud.Id, deviceId);
                                foreach (var photoset in photosetList)
                                {

                                    var photoCollection = flickr.PhotosetsGetPhotos(photoset.PhotosetId);
                                    foreach (var photo in photoCollection)
                                    {
                                        Urls url = new Urls(photo.LargeUrl, photo.PhotoId, IDPFLibrary.CloudProviderType.Flickr, photo.DateUploaded);
                                        photoUrlList.Add(url);
                                    }
                                }
                            }else if(cloud.Provider == ProviderType.DropBox)
                            {
                                dbx = new DropboxClient(cloud.Token);
                                List<Folder> folderList= await folderService.GetDeviceDropboxFolders(cloud.Id, deviceId);
                                foreach (var folder in folderList)
                                {
                                    var list = await dbx.Files.ListFolderAsync(folder.Name, false);
                                    foreach (var file in list.Entries.Where(i => i.IsFile))
                                    {
                                        if (PhotoChecker.IsImage(file.Name))
                                        {
                                            var dbxUrl = await dbx.Files.GetTemporaryLinkAsync(file.PathDisplay);
                                            Urls url = new Urls(dbxUrl.Link, dbxUrl.Metadata.Id, IDPFLibrary.CloudProviderType.Dropbox, dbxUrl.Metadata.ClientModified);
                                            photoUrlList.Add(url);
                                        }
                                    }
                                    
                                }
                            }
                        }
                        catch (Exception e)
                        {

                            ICloudService cloudService = new CloudService();
                            await cloudService.removeCloud(cloud.Id);
                            return null;
                        }

                    }
                }
            }
            catch (Exception e)
            {
                photoUrlList = null;
            }
            GetAllFlickrPhotosURLResponseDTO response = new GetAllFlickrPhotosURLResponseDTO(photoUrlList);
            return response;
        }
        #endregion methods
    }
}