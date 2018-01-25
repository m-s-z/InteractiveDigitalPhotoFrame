using FlickrNet;
using IDPFLibrary;
using IDPFLibrary.DTO.AAA.Folder.Response;
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
    /// Controller class responsible for manipulating and exposing folders
    /// </summary>
    public class FolderController : Controller
    {
        #region fields
        /// <summary>
        /// authentication service for authentication handling
        /// </summary>
        IAuthenticationService authService = new AuthenticationService();
        /// <summary>
        /// device service exposing device related database information
        /// </summary>
        IDeviceService deviceService = new DeviceService();
        /// <summary>
        /// folder service exposing folder related database information
        /// </summary>
        IFolderService folderService = new FolderService();
        /// <summary>
        /// cloud service exposing cloud related database information
        /// </summary>
        ICloudService cloudService = new CloudService();
        #endregion fields

        /// <summary>
        /// Constructor for FolderController
        /// </summary>
        public FolderController()
        {
            
        }

        /// <summary>
        /// Constructor for FolderController
        /// </summary>
        /// <param name="dev">instance of device service</param>
        /// <param name="cloud">instance of cloud service</param>
        public FolderController(IDeviceService dev, ICloudService cloud)
        {
            deviceService = dev;
            cloudService = cloud;
        }

        /// <summary>
        /// Constructor for FolderController
        /// </summary>
        /// <param name="cloud">instance of cloud service</param>
        public FolderController(ICloudService cloud)
        {
            cloudService = cloud;
        }

        /// <summary>
        /// Constructor for FolderController
        /// </summary>
        /// <param name="auth"> instance of authentication service</param>
        public FolderController(IAuthenticationService auth)
        {
            authService = auth;
        }

        /// <summary>
        /// constructor for FolderController
        /// </summary>
        /// <param name="auth"> instance of authentication service</param>
        /// <param name="folders"> instacne of folderService</param>
        public FolderController(IAuthenticationService auth, IFolderService folders)
        {
            authService = auth;
            folderService = folders;
        }

        /// <summary>
        /// constructor for FolderController
        /// </summary>
        /// <param name="auth"> instance of authentication service</param>
        /// <param name="cloud">instance of cloud service</param>
        /// <param name="folders"> instacne of folderService</param>
        public FolderController(IAuthenticationService auth, IFolderService folders, ICloudService cloud)
        {
            authService = auth;
            folderService = folders;
            cloudService = cloud;
        }
        #region methods

        /// <summary>
        /// Returns view of folders divided into devices 
        /// </summary>
        /// <param name="IdOfOpenDevice">optional parameter, if it is set the folders for given device will not be collapsed</param>
        /// <returns>view for folders</returns>
        public async Task<ActionResult> Index(int? IdOfOpenDevice)
        {
            List<DeviceName> devices = await deviceService.GetDevices(authService.getLoggedInUsername(Session));
            List<Folder> folders = new List<Folder>();
            //searching by cloud
            List<Cloud> clouds = await cloudService.GetClouds(authService.getLoggedInUsername(Session));
            foreach (var cloud in clouds)
            {
                List<Folder> cloudFolders = new List<Folder>();
                switch (cloud.Provider)
                {
                    case ProviderType.Flicker:
                        cloudFolders = await folderService.RefreshFlickrFolders(cloud.Id);
                        break;
                    case ProviderType.DropBox:
                        cloudFolders = await folderService.RefreshDropboxFolders(cloud.Id);
                        break;
                    default:
                        break;
                }
                if (cloudFolders != null)
                {
                    foreach (var fold in cloudFolders)
                    {
                        folders.Add(fold);
                    }
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

        /// <summary>
        /// this method creates the view for creating a new folder. It pulls all clouds connected to Account and adds them to ViewBag cloud
        /// </summary>
        /// <param name="deviceId">id of device the method should add folders to</param>
        /// <returns>
        /// view for selecting folder
        /// </returns>
        public async Task<ActionResult> NewFolder(int deviceId)
        {
            if (!authService.IsAuthenticated(Session))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Login to use this request");
            }
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


        /// <summary>
        /// prepares a confirm delete folder view
        /// </summary>
        /// <param name="folderId">folderid to be removed</param>
        /// <param name="folderName">folder name to be removed </param>
        /// <returns>
        /// confirm delete folder view
        /// </returns>
        public ActionResult DeleteFolder(int folderId, String folderName)
        {
            if (!authService.IsAuthenticated(Session))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Login to use this request");
            }
            ConfirmDeleteFolderViewModel view = new ConfirmDeleteFolderViewModel(folderId, folderName);
            return View(view);
        }


        /// <summary>
        /// deletes folder from database
        /// </summary>
        /// <param name="folderId">id of folder to be deleted</param>
        /// <returns>
        /// return foler view
        /// </returns>
        public async Task<ActionResult> ConfirmDeleteFolder(int folderId)
        {
            if (!authService.IsAuthenticated(Session))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Login to use this request");
            }
            await folderService.deleteFolder(folderId);
            return Redirect("Index");
        }
        /// <summary>
        /// deletes folder from database
        /// </summary>
        /// <param name="folderId">id of folder to be deleted</param>
        /// <returns>
        /// Http status code result
        /// </returns>
        [HttpDelete]
        public async Task<ActionResult> AppDeleteFolder(int folderId, string token, int userId)
        {
            AppDeleteFolderResponseDTO dto = new AppDeleteFolderResponseDTO();
            AuthorizationResponse auth = await authService.AppIsAuthenticated(token, userId);
            if (auth != AuthorizationResponse.Ok)
            {
                dto.Auth = auth;
                return Json(dto);
            }
            await folderService.deleteFolder(folderId);
            dto.Auth = auth;
            return Json(dto);
        }

        /// <summary>
        /// prepares view with folders that can be added to device
        /// </summary>
        /// <param name="Clouds">cloud id from which we will add folders</param>
        /// <param name="deviceId">device id to which device we will add folder</param>
        /// <returns>
        /// select folder view model
        /// </returns>
        public async Task<ActionResult> SelectFolder(int Clouds, int deviceId)
        {
            if (!authService.IsAuthenticated(Session))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Login to use this request");
            }
            List<UniversalFolder> folders = new List<UniversalFolder>();
            Cloud cloud = await cloudService.GetCloud(Clouds);
            switch(cloud.Provider)
            {
                case ProviderType.Flicker:
                    folders = await folderService.GetFlickrFolders(cloud.Id, deviceId);
                    break;
                case ProviderType.DropBox:
                    folders = await folderService.GetDropboxFolders(cloud.Id, deviceId);
                    break;
            }
            SelectFolderViewModel view = new SelectFolderViewModel(cloud, folders, deviceId);
            return View(view);
        }

        /// <summary>
        /// method for adding list of folders
        /// </summary>
        /// <param name="model">list of folders to be added</param>
        /// <param name="cloudId">cloud from which the folders will be added</param>
        /// <param name="deviceId">device to which folders will be added</param>
        /// <returns>
        /// folder view
        /// </returns>
        public async Task<ActionResult> ConfirmAddFolder(SelectFolderViewModel model, int cloudId, int deviceId)
        {
            if (!authService.IsAuthenticated(Session))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Login to use this request");
            }
            if (model.SelectedFolders != null)
            {
                await folderService.AddCloudFolders(model.SelectedFolders.ToList<String>(), cloudId, deviceId);
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// method for adding list of folders
        /// </summary>
        /// <param name="model">list of folders to be added</param>
        /// <param name="cloudId">cloud from which the folders will be added</param>
        /// <param name="deviceId">device to which folders will be added</param>
        /// <returns>
        /// HttpStatusCodeResult
        /// </returns>
        [HttpPost]
        public async Task<ActionResult> AppAddFolder(List<string> folders, int cloudId, int deviceId, string token, int userId)
        {
            AppAddFolderResponseDTO dto = new AppAddFolderResponseDTO();
            AuthorizationResponse auth = await authService.AppIsAuthenticated(token, userId);
            if (auth != AuthorizationResponse.Ok)
            {
                dto.Auth = auth;
                return Json(dto);
            }
            await folderService.AddCloudFolders(folders, cloudId, deviceId);
            dto.Auth = auth;
            return Json(dto);
        }
        /// <summary>
        /// Method for obtaining all folders conected to given device
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns>AppGetDeviceFoldersResponseDTO</returns>
        [HttpPost]
        public async Task<ActionResult> AppGetDeviceFolders(int deviceId, int accountId, string token)
        {
            AppGetDeviceFoldersResponseDTO dto = new AppGetDeviceFoldersResponseDTO();
            AuthorizationResponse auth = await authService.AppIsAuthenticated(token, accountId);
            if (auth != AuthorizationResponse.Ok)
            {
                dto.Auth = auth;
                return Json(dto);
            }
            List<Folder> folders = new List<Folder>();
            //searching by cloud
            string user = await authService.GetAccountLogin(accountId);
            List<Cloud> clouds = await cloudService.GetClouds(user);
            foreach (var cloud in clouds)
            {
                List<Folder> cloudFolders = new List<Folder>();
                switch (cloud.Provider)
                {
                    case ProviderType.Flicker:
                        cloudFolders = await folderService.RefreshFlickrFolders(cloud.Id);
                        break;
                    case ProviderType.DropBox:
                        cloudFolders = await folderService.RefreshDropboxFolders(cloud.Id);
                        break;
                    default:
                        break;
                }
                if (cloudFolders != null)
                {
                    foreach (var fold in cloudFolders)
                    {
                        folders.Add(fold);
                    }
                }
            }
            List<SFolder> sFolders = new List<SFolder>();
            foreach(var f in folders)
            {
                if (f.Cloud.Account.Id == accountId)
                {
                    SFolder sFolder = new SFolder();
                    sFolder.CloudId = f.CloudId;
                    sFolder.DeviceId = f.DeviceId;
                    sFolder.Name = f.Name;
                    sFolder.FolderId = f.FolderId;
                    sFolders.Add(sFolder);
                }
            }
            dto.Auth = auth;
            dto.Folders = sFolders;
            return Json(dto);
        }
        /// <summary>
        /// method for getting folders pulled from the provider's side, we ommit folders that were already assigned to the device.
        /// </summary>
        /// <param name="cloudId"></param>
        /// <param name="deviceId"></param>
        /// <returns>
        /// AppGetCloudFoldersResponseDTO
        /// </returns>
        [HttpPost]
        public async Task<ActionResult> AppGetCloudFolders(int cloudId, int deviceId, string token, int userId)
        {
            AppGetCloudFoldersResponseDTO dto = new AppGetCloudFoldersResponseDTO();
            AuthorizationResponse auth = await authService.AppIsAuthenticated(token, userId);
            if (auth != AuthorizationResponse.Ok)
            {
                dto.Auth = auth;
                return Json(dto);
            }
            List<UniversalFolder> folders = new List<UniversalFolder>();
            List<SUniversalFolder> sFolders = new List<SUniversalFolder>();
            Cloud cloud = await cloudService.GetCloud(cloudId);
            switch (cloud.Provider)
            {
                case ProviderType.Flicker:
                    folders = await folderService.GetFlickrFolders(cloud.Id, deviceId);
                    break;
                case ProviderType.DropBox:
                    folders = await folderService.GetDropboxFolders(cloud.Id, deviceId);
                    break;
            }
            foreach(var f in folders)
            {
                SUniversalFolder sFolder = new SUniversalFolder();
                sFolder.DateUpdated = f.DateUpdated;
                sFolder.NumberOfPhotos = f.NumberOfPhotos;
                sFolder.Title = f.Title;
                sFolders.Add(sFolder);
            }
            dto.folders = sFolders;
            dto.Auth = auth;
            return Json(dto);
        }
        #endregion methods

    }
}