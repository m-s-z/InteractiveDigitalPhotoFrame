using System;
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
           
            var device = new Device(1,"Grandmas Tablet");
            var device2 = new Device(2, "My Tablet");
            List<Device> devices = new List<Device>();
            devices.Add(device);
            devices.Add(device2);

            DeviceViewModel deviceViewModel = new DeviceViewModel(devices);

            return View(deviceViewModel);
        }
        
        public ActionResult NewDevice()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> GetUserAllDevices(int? user)
        {
            if(Session["UserId"] == null)
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

        public ActionResult DeleteDevice(int deviceId, String deviceName)
        {
            ConfirmDeleteDeviceViewModel view = new ConfirmDeleteDeviceViewModel(deviceId, deviceName);
            return View(view);
        }
        public ActionResult ConfirmDeleteDevice(int deviceId)
        {
            return Redirect("Index");
        }

        public async Task<ActionResult> PairDevice(String pairCode)
        {
            PairDeviceViewModel view = new PairDeviceViewModel("Success");
            return View(view);
        }
    }
}