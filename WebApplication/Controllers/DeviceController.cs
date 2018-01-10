using IDPFLibrary.DTO;
using IDPFLibrary.DTO.AAA.Device.Request;
using IDPFLibrary.DTO.AAA.Device.Response;
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
        #region fields
        /// <summary>
        /// authentication service for authentication handling
        /// </summary>
        IAuthenticationService authService = new AuthenticationService();
        /// <summary>
        /// folder service exposing folder related database information
        /// </summary>
        IDeviceService deviceService = new DeviceService();
        #endregion fields

        /// <summary>
        /// constructor for DeviceController
        /// </summary>
        public DeviceController()
        {

        }

        /// <summary>
        /// constructor for DeviceController
        /// </summary>
        /// <param name="dev">instance of device service</param>
        public DeviceController(IDeviceService dev)
        {
            deviceService = dev;
        }

        /// <summary>
        /// constructor for DeviceController
        /// </summary>
        /// <param name="auth"> instance of authentication service</param>
        public DeviceController(IAuthenticationService auth)
        {
            authService = auth;
        }

        /// <summary>
        /// constructor for DeviceController
        /// </summary>
        /// <param name="dev">instance of device service</param>
        /// <param name="auth"> instance of authentication service</param>
        public DeviceController(IDeviceService dev, IAuthenticationService auth)
        {
            deviceService = dev;
            authService = auth;
        }
        #region methods

        /// <summary>
        /// prepares device view
        /// </summary>
        /// <returns>
        /// device view
        /// </returns>
        public async Task<ActionResult> Index()
        {
            List<DeviceName> devices = await deviceService.GetDevices(authService.getLoggedInUsername(Session));
            DeviceViewModel deviceViewModel = new DeviceViewModel(devices);
            return View(deviceViewModel);
        }
        
        /// <summary>
        /// prepares new device view
        /// </summary>
        /// <returns>
        /// device view
        /// </returns>
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

        /// <summary>
        /// generates a new paircode
        /// </summary>
        /// <param name="deviceId">device id for which the paircode will be generated</param>
        /// <param name="deviceToken">device auhtentication token</param>
        /// <returns>
        /// string with paircode
        /// </returns>
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

        /// <summary>
        /// prepares delete device view
        /// </summary>
        /// <param name="deviceId">device Id to be deleted</param>
        /// <param name="deviceName">device name to be deleted</param>
        /// <returns>
        /// confirm delete device view
        /// </returns>
        public ActionResult DeleteDevice(int deviceId, String deviceName)
        {
            if (!authService.IsAuthenticated(Session))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Login to use this request");
            }
            ConfirmDeleteDeviceViewModel view = new ConfirmDeleteDeviceViewModel(deviceId, deviceName);
            return View(view);
        }

        /// <summary>
        /// deletes device
        /// </summary>
        /// <param name="deviceId">device id to be dedleted</param>
        /// <returns>
        /// device view
        /// </returns>
        public async Task<ActionResult> ConfirmDeleteDevice(int deviceId)
        {
            if (!authService.IsAuthenticated(Session))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Login to use this request");
            }
            await deviceService.UnpairDevice(deviceId, authService.getLoggedInUsername(Session));
            return Redirect("Index");
        }


        /// <summary>
        /// pairs with device
        /// </summary>
        /// <param name="pairCode">paircode for pairing</param>
        /// <param name="deviceName">device to paired with</param>
        /// <returns>
        /// pair device view
        /// </returns>
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

        /// <summary>
        /// pairs with device
        /// </summary>
        /// <param name="pairCode">paircode for pairing</param>
        /// <param name="deviceName">device to paired with</param>
        /// <returns>
        /// string with result
        /// </returns>
        [HttpPost]
        public async Task<ActionResult> AppPairDevice(String pairCode, string deviceName)
        {
            if (!authService.IsAuthenticated(Session))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Login to use this request");
            }
            pairCode = pairCode.ToUpper();
            AppPairDeviceResponseDTO dto = new AppPairDeviceResponseDTO();
            dto.Message = await deviceService.PairDevice(pairCode, deviceName, authService.getLoggedInUsername(Session));
            return Json(dto);
        }

        /// <summary>
        /// creates a new device
        /// </summary>
        /// <param name="key">secret application key</param>
        /// <returns>
        /// CreateNewDeviceDTO object on success
        /// Forbidden if key is invalid
        /// </returns>
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

        /// <summary>
        /// gets all accounts connected to the device
        /// </summary>
        /// <param name="deviceId">device id for which the paircode will be generated</param>
        /// <param name="deviceToken">device auhtentication token</param>
        /// <returns>
        /// GetDeviceAccountsDTO object on success 
        /// http status Forbidden if token is invalid
        /// </returns>
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


        /// <summary>
        /// unpairs account from a given device
        /// </summary>
        /// <param name="deviceId">device id for which the paircode will be generated</param>
        /// <param name="deviceToken">device auhtentication token</param>
        /// <param name="accountId">account id to be unpaired</param>
        /// <returns>
        /// http status Ok on success
        /// http status not found if account id cannot be found
        /// http status Forbidden if token is invalid
        /// </returns>
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

        /// <summary>
        /// finds all photos connected to the device and and accounts
        /// </summary>
        /// <param name="deviceId">device id for which the paircode will be generated</param>
        /// <param name="deviceToken">device auhtentication token</param>
        /// <param name="accountIds">account ids to download photos from</param>
        /// <returns>
        /// GetAllFlickrPhotosURLResponseDTO object on success
        /// internal server error if the cloud token has been revoked
        /// Forbidden if device token is invalid
        /// </returns>
        [HttpPost]
        public async Task<ActionResult> GetAllPhotosUrl(int deviceId, string deviceToken, List<int> accountIds)
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

        /// <summary>
        /// method for AAA to get devices. Used to get all devices connected to given account
        /// </summary>
        /// <param name="accountId">account id</param>
        /// <returns>
        /// AppGetDevicesResponseDTO
        /// </returns>
        [HttpGet]
        public async Task<ActionResult> AppGetDevices(int accountId)
        {

            AppGetDevicesResponseDTO dto = new AppGetDevicesResponseDTO();
            string acc = await authService.GetAccountLogin(accountId);
            List<DeviceName> devices = await deviceService.GetDevices(acc);
            List<SDeviceName> sDevices = new List<SDeviceName>();
            foreach(var dev in devices)
            {
                SDeviceName sDevice = new SDeviceName();
                sDevice.DeviceId = dev.Device.DeviceId;
                sDevice.Name = dev.CustomDeviceName;
                sDevices.Add(sDevice);
            }
            dto.Devices = sDevices;
            return Json(sDevices);
        }

        #endregion methods
        /*
        public async Task<ActionResult> Test()
        {
            //var response = await deviceService.GeneratePairCode(6, "7EGLOIZ");
            var response = await deviceService.GetAllPhotosUrl(new List<int>() { 14 }, 6);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
        */
    }
}