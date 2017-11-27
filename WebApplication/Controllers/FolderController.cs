﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        // GET: Folder
        public ActionResult Index(int? IdOfOpenDevice)
        {
            int index;
            var folder = new Folder("summer", 1, 1);
            var folder2 = new Folder("winter", 2, 1);
            List<Folder> folders = new List<Folder>();
            List<Folder> folders2 = new List<Folder>();

            folders.Add(folder);
            folders.Add(folder2);
            folders2.Add(folder);
            var device = new Device(1, "Grandmas Tablet");
            var device2 = new Device(2, "My Tablet");
            List<Device> devices = new List<Device>();
            devices.Add(device);
            devices.Add(device2);
            if (IdOfOpenDevice != null)
            {
                index = devices.FindIndex(d => d.DeviceId == IdOfOpenDevice);
            }
            else
            {
                index = -1; 
            }
            FolderViewModel deviceModel = new FolderViewModel(devices, folders, index);

            return View(deviceModel);
        }

        

        /*[Route("folder/BindDevice/{Folderid:min(0)}/{DeviceId:min(0)}")]
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