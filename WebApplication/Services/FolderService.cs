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

    }
}