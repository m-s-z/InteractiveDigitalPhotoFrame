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

    }
}