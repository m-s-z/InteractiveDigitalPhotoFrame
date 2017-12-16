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

namespace WebApplication.Services
{
    public class FolderService
    {
        private ApplicationContext db = new ApplicationContext();
        public FolderService()
        {

        }

        //gets all folders asssigned to given device if device id cannot be found it returns an empty list
        public async Task<List<Folder>> getFolders(int deviceId)
        {
            List<Folder> folders = await db.Folders.Where(f => f.DeviceId == deviceId).ToListAsync();
            return folders;
        }

        //deletes folder with specified id and return true on success and false if the folder cannot be found
        public async Task<bool> deleteFolder(int folderId)
        {
            Folder folder = await db.Folders.FindAsync(folderId);
            if (folder != null)
            {
                db.Folders.Remove(folder);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }
        //checks if any of the Flickr Folders have been removed if they have we delete them from our db and return the actual list of folders
        public async Task<List<Folder>> RefreshFlickrFolders(int cloudId)
        {
            Cloud cloud = await db.Clouds.FindAsync(cloudId);
            List<Folder> oldFolders = await db.Folders.Where(f => f.CloudId == cloudId).ToListAsync<Folder>();
            FlickrManager fm = new FlickrManager();
            Flickr flicker = await fm.GetAuthInstance(cloudId);
            List<Photoset> newfolders = flicker.PhotosetsGetList(cloud.FlickrUserId).ToList<Photoset>();
            //deleting all folders that have been removed on the side of Flickr
            foreach (var f in oldFolders)
            {
                Photoset album = newfolders.FirstOrDefault(g => g.Title == f.Name);
                if(album == null)
                {
                    await deleteFolder(f.FolderId);
                    oldFolders.Remove(f);
                }
            }
            return oldFolders;
            
        }
        public async Task<List<Photoset>> GetFlickrFolders(int cloudId)
        {
            List<Folder> oldFolders = await db.Folders.Where(f => f.CloudId == cloudId).ToListAsync<Folder>();
            Cloud cloud = await db.Clouds.FindAsync(cloudId);
            FlickrManager fm = new FlickrManager();
            Flickr flicker = await fm.GetAuthInstance(cloudId);
            List<Photoset> folders = flicker.PhotosetsGetList(cloud.FlickrUserId).ToList<Photoset>();
            //We dont want to display the folders we already have
            foreach(var f in oldFolders)
            {
                Photoset temp = folders.FirstOrDefault(a => a.Title == f.Name);
                if(temp != null)
                {
                    folders.Remove(temp);
                }
            }
            return folders;
        }

        public async Task<bool> AddFlickrFolders(List<string> folders, int cloudId, int deviceId)
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

    }
}