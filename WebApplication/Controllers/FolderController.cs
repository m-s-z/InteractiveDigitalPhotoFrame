using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication.Data;
using WebApplication.Models;
using WebApplication.Services;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    public class FolderController : Controller
    {
        private ApplicationContext db = new ApplicationContext();
        AuthenticationService authService = new AuthenticationService();
        DeviceService deviceService = new DeviceService();
        FolderService folderService = new FolderService();
        // GET: Folder
        public async Task<ActionResult> Index(int? IdOfOpenDevice)
        {
            List<DeviceName> devices = await deviceService.GetDevices(authService.getLoggedInUsername(Session));
            List<Folder> folders = new List<Folder>();
            foreach(var dev in devices)
            {
                List<Folder> deviceFolders = await folderService.getFolders(dev.Device.DeviceId);
                foreach (var fold in deviceFolders)
                {
                    folders.Add(fold);
                }
            }
            int index;
            if (IdOfOpenDevice != null)
            {
                index = devices.FindIndex(d => d.Device.DeviceId == IdOfOpenDevice);
            }
            else
            {
                index = -1; 
            }
            FolderViewModel deviceModel = new FolderViewModel(devices, folders, index);

            return View(deviceModel);
        }

        

        /*
         * for now kept as a reference to be deleted soon
         * [Route("folder/BindDevice/{Folderid:min(0)}/{DeviceId:min(0)}")]
        public ActionResult BindDevice(int FolderId, int DeviceId)
        {
            return Content("fodler id = " + FolderId + " device ID " + DeviceId);
        }*/
        public ActionResult NewFolder()
        {
            return View();
        }
        public ActionResult DeleteFolder(int folderId, String folderName)
        {
            ConfirmDeleteFolderViewModel view = new ConfirmDeleteFolderViewModel(folderId, folderName);
            return View(view);
        }
        public ActionResult ConfirmDeleteFolder(int folderId)
        {
            if (!authService.IsAuthenticated(Session))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Login to use this request");
            }
            return Redirect("Index");
        }
    }
}