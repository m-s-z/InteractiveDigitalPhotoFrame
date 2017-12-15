﻿using System;
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
    public class DeviceController : Controller
    {
        private ApplicationContext db = new ApplicationContext();
        AuthenticationService authService = new AuthenticationService();
        DeviceService deviceService = new DeviceService();
        // GET: Device
        public async Task<ActionResult> Index()
        {
            if (!authService.IsAuthenticated(Session))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Login to use this request");
            }

            List<DeviceName> devices = await deviceService.GetDevices(authService.getLoggedInUsername(Session));
            DeviceViewModel deviceViewModel = new DeviceViewModel(devices);

            return View(deviceViewModel);
        }
        
        public ActionResult NewDevice()
        {
            return View();
        }

        //left as reference dont use this controller method
        [HttpGet]
        public async Task<ActionResult> GetUserAllDevices(int? user)
        {
            if(!authService.IsAuthenticated(Session))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Login to use this request");
            }
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

        [HttpGet]
        public async Task<ActionResult> GeneratePairCode(int deviceId)
        {
            string pairCode = await deviceService.GeneratePairCode(deviceId);
            return Json(pairCode,JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteDevice(int deviceId, String deviceName)
        {
            ConfirmDeleteDeviceViewModel view = new ConfirmDeleteDeviceViewModel(deviceId, deviceName);
            return View(view);
        }
        public async Task<ActionResult> ConfirmDeleteDevice(int deviceId)
        {
            if (!authService.IsAuthenticated(Session))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Login to use this request");
            }
            await deviceService.UnpairDevice(deviceId, authService.getLoggedInUsername(Session));
            return Redirect("Index");
        }

        public async Task<ActionResult> PairDevice(String pairCode, string deviceName)
        {
            if (!authService.IsAuthenticated(Session))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Login to use this request");
            }

            string response = await deviceService.PairDevice(pairCode, deviceName, authService.getLoggedInUsername(Session));
            PairDeviceViewModel view = new PairDeviceViewModel(response);
            return View(view);
        }
    }
}