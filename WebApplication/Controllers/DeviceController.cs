using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    public class DeviceController : Controller
    {
        // GET: Device
        public ActionResult Index()
        {
            var folder = new Folder("summer", 1, 1);
            var folder2 = new Folder("winter", 2, 1);
            List<Folder> folders = new List<Folder>();
            List<Folder> folders2 = new List<Folder>();

            folders.Add(folder);
            folders.Add(folder2);
            folders2.Add(folder);
            var device = new Device(1,"Grandmas Tablet", folders);
            var device2 = new Device(1, "My Tablet", folders2);
            List<Device> devices = new List<Device>();
            devices.Add(device);
            devices.Add(device2);

            DeviceViewModel deviceModel = new DeviceViewModel(devices);

            return View(deviceModel);
        }
    }
}