﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication.Data;
using WebApplication.Models;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    public class DeviceController : Controller
    {
        private ApplicationContext db = new ApplicationContext();
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
            var device = new Device(1,"Grandmas Tablet");
            var device2 = new Device(1, "My Tablet");
            List<Device> devices = new List<Device>();
            devices.Add(device);
            devices.Add(device2);

            DeviceViewModel deviceModel = new DeviceViewModel(devices,folders);

            return View(deviceModel);
        }
        public ActionResult NewFolder()
        {
            return View();
        }
        public ActionResult NewDevice()
        {
            return View();
        }
        public async Task<ActionResult> GetUserAllDevices(int? user)
        {
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest,"User Id not specified");
            }

            var foundUser = await db.Accounts.FindAsync(user);

            List<Device> devices = foundUser.Devices.ToList<Device>();
            List<SimpleDevice> sDevices = new List<SimpleDevice>();
            foreach (var dev in devices)
            {
                sDevices.Add(new SimpleDevice(dev));
            }
            if (foundUser == null)
            {
                return HttpNotFound();
            }
            return Json(sDevices, JsonRequestBehavior.AllowGet);
        }
    }
}