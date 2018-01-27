using Dropbox.Api;
using Dropbox.Api.Files;
using FlickrNet;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication.Data;
using WebApplication.Models;
using WebApplication.Utils;
using WebApplication.ViewModels;

namespace WebApplication.Services
{
    /// <summary>
    /// folder service class for manipulating the database in regards to folders
    /// </summary>
    public class FolderService : IFolderService
    {
        #region fields
        /// <summary>
        /// instance of application context to manipulate the database
        /// </summary>
        private ApplicationContext db = new ApplicationContext();
        #endregion fields
        /// <summary>
        /// constructor for FolderService class
        /// </summary>
        public FolderService()
        {

        }
        #region methods

        /// <summary>
        /// method for retrieving all folders connected to device
        /// </summary>
        /// <param name="deviceId">device id</param>
        /// <returns>
        /// list of Folder model class
        /// empty list if device cannot be found
        /// </returns>
        public async Task<List<Folder>> getFolders(int deviceId)
        {
            List<Folder> folders = await db.Folders.Where(f => f.DeviceId == deviceId).ToListAsync();
            return folders;
        }

        /// <summary>
        /// method for deleting folder
        /// </summary>
        /// <param name="folderId">folder id</param>
        /// <returns>
        /// true on success
        /// false if folder cannot be found
        /// </returns>
        public async Task<bool> deleteFolder(int folderId)
        {
            Folder folder = await db.Folders.FindAsync(folderId);
            if (folder != null)
            {
                db.Folders.Remove(folder);
                int check = await db.SaveChangesAsync();
                return true;
            }
            return false;
        }
        /// <summary>
        /// method for checking if any of the Flickr Folders have been removed if they have we delete them from our db and return the actual list of folders
        /// </summary>
        /// <param name="cloudId">cloud id from which we retrieve the folders</param>
        /// <returns>
        /// List of Folder model class
        /// </returns>
        public async Task<List<Folder>> RefreshFlickrFolders(int cloudId)
        {
            bool check;
            Cloud cloud = await db.Clouds.FindAsync(cloudId);
            List<Folder> oldFolders = await db.Folders.Where(f => f.CloudId == cloudId).ToListAsync<Folder>();
            FlickrManager fm = new FlickrManager();
            Flickr flicker = await fm.GetAuthInstance(cloudId);
            try
            {
                List<Photoset> newfolders = flicker.PhotosetsGetList(cloud.FlickrUserId).ToList<Photoset>();
            
                List<String> toDelete = new List<string>();
                //deleting all folders that have been removed on the side of Flickr
                foreach (var f in oldFolders)
                {
                    Photoset album = newfolders.FirstOrDefault(g => g.Title == f.Name);
                    if(album == null)
                    {
                        toDelete.Add(f.Name);
                        check = await deleteFolder(f.FolderId);
                    }
                }
                foreach (var d in toDelete)
                {
                    Folder found = oldFolders.FirstOrDefault(f => f.Name == d);
                    oldFolders.Remove(found);
                }
                return oldFolders;
                }
            catch (Exception e)
            {
                ICloudService cloudService = new CloudService();
                check = await cloudService.removeCloud(cloudId);
                return null;
            }
        }

        /// <summary>
        /// method for checking if any of the Dropbox Folders have been removed if they have we delete them from our db and return the actual list of folders
        /// </summary>
        /// <param name="cloudId">cloud id from which we retrieve the folders</param>
        /// <returns>
        /// List of Folder model class
        /// </returns>
        public async Task<List<Folder>> RefreshDropboxFolders(int cloudId)
        {
            bool check;
            Cloud cloud = await db.Clouds.FindAsync(cloudId);
            List<Folder> oldFolders = await db.Folders.Where(f => f.CloudId == cloudId).ToListAsync<Folder>();
            try
            {
                DropboxClient dbx = new DropboxClient(cloud.Token);
                var list = await dbx.Files.ListFolderAsync(String.Empty, true);
                
                var newFolders = list.Entries.Where(i => i.IsFolder);
                //deleting all folders that have been removed on the side of DropBox
                List<String> toDelete = new List<string>();
                foreach (var f in oldFolders)
                {
                    var album = newFolders.FirstOrDefault(g => g.PathDisplay == f.Name);
                    if (album == null)
                    {
                        check = await deleteFolder(f.FolderId);
                        toDelete.Add(f.Name);
                    }
                }
                foreach (var d in toDelete)
                {
                    Folder found = oldFolders.FirstOrDefault(f => f.Name == d);
                    oldFolders.Remove(found);
                }
                return oldFolders;
            }catch(Exception e)
            {
                ICloudService cloudService = new CloudService();
                check = await cloudService.removeCloud(cloudId);
                return null;
            }
        }
        /// <summary>
        /// returns all Flickr folders that have not been choosen yet. If token to cloud has been revoked we remove the cloud
        /// </summary>
        /// <param name="cloudId">cloud id</param>
        /// <param name="deviceId">device id</param>
        /// <returns>
        /// List of UniversalFolder object
        /// null if token has been removed
        /// </returns>
        public async Task<List<UniversalFolder>> GetFlickrFolders(int cloudId, int deviceId)
        {

            Cloud cloud = await db.Clouds.FindAsync(cloudId);
            List<UniversalFolder> response = new List<UniversalFolder>();
            try
            {
                List<Folder> oldFolders = await db.Folders.Where(f => f.CloudId == cloudId && f.DeviceId == deviceId).ToListAsync<Folder>();
                FlickrManager fm = new FlickrManager();
                Flickr flicker = await fm.GetAuthInstance(cloudId);
                List<Photoset> newfolders = flicker.PhotosetsGetList(cloud.FlickrUserId).ToList<Photoset>();
                foreach (var f in newfolders)
                {
                    Folder alreadyChoosen = oldFolders.FirstOrDefault(i => i.Name == f.Title);
                    if (alreadyChoosen == null)
                    {
                        UniversalFolder folder = new UniversalFolder(f.Title, f.NumberOfPhotos, f.DateUpdated);
                        response.Add(folder);
                    }
                }

                return response;
            }catch(Exception e)
            {
                ICloudService cloudService = new CloudService();
                await cloudService.removeCloud(cloudId);
                return null;
            }

        }
        /// <summary>
        /// returns all Dropbox folders that have not been choosen yet. If token to cloud has been revoked we remove the cloud
        /// </summary>
        /// <param name="cloudId">cloud id</param>
        /// <param name="deviceId">device id</param>
        /// <returns>
        /// List of UniversalFolder object
        /// null if token has been removed
        /// </returns>
        public async Task<List<UniversalFolder>> GetDropboxFolders(int cloudId, int deviceId)
        {
            try
            {
                Cloud cloud = await db.Clouds.FindAsync(cloudId);
                List<Folder> oldFolders = await db.Folders.Where(f => f.CloudId == cloudId && f.DeviceId == deviceId).ToListAsync<Folder>();
                List<UniversalFolder> response = new List<UniversalFolder>();
                DropboxClient dbx = new DropboxClient(cloud.Token);
                //boolean parameter set to true to obtain recursivly all folders
                var list = await dbx.Files.ListFolderAsync(String.Empty, true);
                foreach (var folder in list.Entries.Where(i => i.IsFolder))
                {
                    Folder alreadyChoosen = oldFolders.FirstOrDefault(f => f.Name == folder.PathDisplay);
                    if (alreadyChoosen == null)
                    {
                        var folderContents = await dbx.Files.ListFolderAsync(folder.PathLower);
                        var folderList = folderContents.Entries.Where(i => i.IsFile);
                        List<FileMetadata> fileList = new List<FileMetadata>();
                        foreach (var f in folderList)
                        {
                            
                            fileList.Add(f as FileMetadata);
                        }
                        DateTime updated = DateTime.Now;
                        try
                        {
                            updated = fileList.OrderByDescending(i => i.ClientModified).First().ClientModified;
                        }
                        catch (Exception e)
                        {

                        }
                        UniversalFolder fold = new UniversalFolder(folder.PathDisplay, fileList.Count, updated);
                        response.Add(fold);
                    }
                }
                return response;
            }catch(Exception e)
            {
                ICloudService cloudService = new CloudService();
                await cloudService.removeCloud(cloudId);
                return null;
            }

        }
        /// <summary>
        /// returns all selected Flickr folders. If a folder has been removed on the side of the provider we do not return it
        /// </summary>
        /// <param name="cloudId">cloud id</param>
        /// <param name="deviceId">device id</param>
        /// <returns>
        /// List of Photoset class (FlickrNet class)
        /// </returns>
        public async Task<List<Photoset>> GetDeviceFlickrFolders(int cloudId, int deviceId)
        {

            Cloud cloud = await db.Clouds.FindAsync(cloudId);
            List<Folder> oldFolders = await db.Folders.Where(f => f.CloudId == cloudId && f.DeviceId == deviceId).ToListAsync<Folder>();
            FlickrManager fm = new FlickrManager();
            Flickr flicker = await fm.GetAuthInstance(cloudId);
            List<Photoset> newfolders = flicker.PhotosetsGetList(cloud.FlickrUserId).ToList<Photoset>();
            List<string> photosetsToBeRemoved = new List<string>();
            foreach (var f in newfolders)
            {
                Folder folder = oldFolders.FirstOrDefault(g => g.Name == f.Title);
                if (folder == null)
                {
                    photosetsToBeRemoved.Add(f.PhotosetId);
                }
            }
            foreach(var f in photosetsToBeRemoved)
            {
                Photoset temp =  newfolders.FirstOrDefault(a => a.PhotosetId == f);
                newfolders.Remove(temp);
            }
            return newfolders;
        }
        /// <summary>
        /// returns all selected Dropbox folders. If a folder has been removed on the side of the provider we do not return it
        /// </summary>
        /// <param name="cloudId">cloud id</param>
        /// <param name="deviceId">device id</param>
        /// <returns>
        /// List of Folder model class
        /// </returns>
        public async Task<List<Folder>> GetDeviceDropboxFolders(int cloudId, int deviceId)
        {
            Cloud cloud = await db.Clouds.FindAsync(cloudId);

            DropboxClient dbx = new DropboxClient(cloud.Token);
            List<Folder> oldFolders = await db.Folders.Where(f => f.CloudId == cloudId && f.DeviceId == deviceId).ToListAsync<Folder>();
            var list = await dbx.Files.ListFolderAsync(String.Empty, true);
            List<int> foldersToRemove = new List<int>();
            foreach (var folder in oldFolders)
            {
                if (list.Entries.Where(i => i.IsFolder).FirstOrDefault(f => f.PathDisplay == folder.Name) == null)
                {
                    foldersToRemove.Add(folder.FolderId);
                }
            }
            foreach(var folder in foldersToRemove)
            {
                var found = oldFolders.FirstOrDefault(i => i.FolderId == folder);
                oldFolders.Remove(found);
            }
            return oldFolders;
        }

        /// <summary>
        /// method for adding folders
        /// </summary>
        /// <param name="folders">list of string (folder names)</param>
        /// <param name="cloudId"> cloud id</param>
        /// <param name="deviceId"> device id</param>
        /// <returns>
        /// true on succes
        /// false otherwise
        /// </returns>
        public async Task<bool> AddCloudFolders(List<string> folders, int cloudId, int deviceId)
        {
            Cloud cloud = await db.Clouds.FindAsync(cloudId);
            Device device = await db.Devices.FindAsync(deviceId);
            if (cloud != null && device != null)
            {
                try
                {
                    foreach (var f in folders)
                    {
                        Folder folder = new Folder(f, device, cloud);
                        db.Folders.Add(folder);
                    }
                    await db.SaveChangesAsync();
                }catch(Exception e)
                {
                    return false;
                }
                return true;
            }
            return false;
        }
        #endregion methods

    }
}