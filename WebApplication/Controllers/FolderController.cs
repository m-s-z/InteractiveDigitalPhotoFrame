using FlickrNet;
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
        CloudService cloudService = new CloudService();
        // GET: Folder
        public async Task<ActionResult> Index(int? IdOfOpenDevice)
        {
            List<DeviceName> devices = await deviceService.GetDevices(authService.getLoggedInUsername(Session));
            List<Folder> folders = new List<Folder>();
            //searching by cloud
            List<Cloud> clouds = await cloudService.GetClouds(authService.getLoggedInUsername(Session));
            foreach (var cloud in clouds)
            {
                switch(cloud.Provider)
                {
                    case ProviderType.Flicker:
                        List<Folder> cloudFolders = await folderService.RefreshFlickrFolders(cloud.Id);
                        foreach (var fold in cloudFolders)
                        {
                            folders.Add(fold);
                        }
                        break;
                    default:
                        break;
                }
                
            }
            /*
             * legacy searching by device
            foreach (var dev in devices)
            {
                List<Folder> deviceFolders = await folderService.getFolders(dev.Device.DeviceId);
                foreach (var fold in deviceFolders)
                {
                    folders.Add(fold);
                }
            }
            */
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
        public async Task<ActionResult> NewFolder(int deviceId)
        {
            List<Cloud> clouds = await cloudService.GetClouds(authService.getLoggedInUsername(Session));
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var cloud in clouds)
            {
                items.Add(new SelectListItem { Text = cloud.Login, Value = cloud.Id.ToString() });
            }
            ViewBag.Clouds = items;
            NewFolderViewModel view = new NewFolderViewModel(deviceId);
            return View(view);
        }
        public ActionResult DeleteFolder(int folderId, String folderName)
        {
            ConfirmDeleteFolderViewModel view = new ConfirmDeleteFolderViewModel(folderId, folderName);
            return View(view);
        }
        public async Task<ActionResult> ConfirmDeleteFolder(int folderId)
        {
            if (!authService.IsAuthenticated(Session))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Login to use this request");
            }
            await folderService.deleteFolder(folderId);
            return Redirect("Index");
        }

        public async Task<ActionResult> SelectFolder(int Clouds, int deviceId)
        {
            Cloud cloud = await cloudService.GetCloud(Clouds);
            List<Photoset> folders = await folderService.GetFlickrFolders(cloud.Id);
            SelectFolderViewModel view = new SelectFolderViewModel(cloud, folders, deviceId);
            return View(view);
        }
        public async Task<ActionResult> ConfirmAddFolder(SelectFolderViewModel model, int cloudId, int deviceId)
        {
            await folderService.AddFlickrFolders(model.SelectedFolders.ToList<String>(), cloudId, deviceId);
            return RedirectToAction("Index");
        }

    }
}