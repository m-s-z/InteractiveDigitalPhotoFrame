using IDPFLibrary.DTO;
using IDPFLibrary.Utils;
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
    /// <summary>
    /// Controller class responsible for manipulating and exposing devices
    /// </summary>
    public class DeviceController : Controller
    {
        IAuthenticationService authService = new AuthenticationService();
        IDeviceService deviceService = new DeviceService();
        // GET: Device
        public async Task<ActionResult> Index()
        {
            List<DeviceName> devices = await deviceService.GetDevices(authService.getLoggedInUsername(Session));
            DeviceViewModel deviceViewModel = new DeviceViewModel(devices);
            return View(deviceViewModel);
        }
        
        public ActionResult NewDevice()
        {
            return View();
        }

        /*
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
        }*/

        [HttpPost]
        public async Task<ActionResult> GeneratePairCode(int deviceId, string deviceToken)
        {
            string pairCode = await deviceService.GeneratePairCode(deviceId, deviceToken);
            if (pairCode != "")
            {
                return Json(pairCode, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "invalid device token");
            }
        }

        public ActionResult DeleteDevice(int deviceId, String deviceName)
        {
            if (!authService.IsAuthenticated(Session))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Login to use this request");
            }
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
            pairCode = pairCode.ToUpper();
            string response = await deviceService.PairDevice(pairCode, deviceName, authService.getLoggedInUsername(Session));
            PairDeviceViewModel view = new PairDeviceViewModel(response);
            return View(view);
        }
        [HttpPost]
        public async Task<ActionResult> CreateNewDevice(String key)
        {
            CreateNewDeviceDTO dto = await deviceService.CreateDevice(key);
            if(dto ==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "invalid key");
            }
            else
            {
                return Json(dto, JsonRequestBehavior.DenyGet);
            }
        }
        [HttpPost]
        public async Task<ActionResult> GetDeviceAccounts(int deviceId, string deviceToken)
        {
            GetDeviceAccountsDTO dto = await deviceService.GetDeviceAccounts(deviceId, deviceToken);
            if(dto ==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "invalid token");
            }
            else
            {
                return Json(dto, JsonRequestBehavior.DenyGet);
            }
        }
        [HttpPost]
        public async Task<ActionResult> UnpairDevice(int deviceId, string deviceToken, int accountId)
        {
            if(await deviceService.DeviceIsAuthenticated(deviceId,deviceToken))
            {
                string username = await authService.GetAccountLogin(accountId);
                if(await deviceService.UnpairDevice(deviceId,username))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound, "accountId not found");

                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "invalid device token");

        }

        [HttpPost] public async Task<ActionResult> GetAllPhotosUrl(int deviceId, string deviceToken, List<int> accountIds)
        {
            if (await deviceService.DeviceIsAuthenticated(deviceId, deviceToken))
            {
                GetAllFlickrPhotosURLResponseDTO dto = await deviceService.GetAllPhotosUrl(accountIds, deviceId);
                if (dto != null)
                {
                    return Json(dto, JsonRequestBehavior.DenyGet);
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Cloud token has been revoked");
                }
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "invalid device token");
            }
        }

        

        public async Task<ActionResult> Test()
        {
            var response = await deviceService.GeneratePairCode(6, "7EGLOIZ");
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

    }
}