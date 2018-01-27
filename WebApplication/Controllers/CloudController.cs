﻿using Dropbox.Api;
using FlickrNet;
using Microsoft.OneDrive.Sdk;
using Microsoft.OneDrive.Sdk.Authentication;
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
using WebApplication.Utils;
using WebApplication.ViewModels;
using IDPFLibrary.Utils;
using IDPFLibrary;
using IDPFLibrary.DTO.AAA.Cloud.Response;

namespace WebApplication.Controllers
{
    /// <summary>
    /// Controller class responsible for manipulating and exposing clouds
    /// </summary>
    public class CloudController : Controller
    {
        #region fields
        /// <summary>
        /// authentication service for authentication handling
        /// </summary>
        IAuthenticationService authService = new AuthenticationService();
        /// <summary>
        /// cloud service exposing cloud related database information
        /// </summary>
        ICloudService cloudService = new CloudService();
        #endregion fields

        /// <summary>
        /// constructor for CloudController
        /// </summary>
        public CloudController()
        {

        }

        /// <summary>
        /// constructor for CloudController
        /// </summary>
        /// <param name="cs">instance of cloud service</param>
        public CloudController(ICloudService cs)
        {
            cloudService = cs;

        }
        #region methods
        /// <summary>
        /// method for preparing cloud view
        /// </summary>
        /// <returns>
        /// cloud index view
        /// </returns>
        public async Task<ActionResult> Index()
        {
            List<Cloud> clouds = await cloudService.GetClouds(authService.getLoggedInUsername(Session));
            
            CloudViewModel model = new CloudViewModel(clouds);

            return View(model);
        }
        /// <summary>
        /// controller method used for deleting a cloud
        /// </summary>
        /// <param name="cloudId">cloud id indentifying the cloud to be removed</param>
        /// <returns>
        /// returns cloud view
        /// </returns>
        public async Task<ActionResult> ConfirmDeleteCloud(int cloudId)
        {
            if (!authService.IsAuthenticated(Session))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Login to use this request");
            }
            await cloudService.removeCloud(cloudId);
            return Redirect("Index");
        }

        /// <summary>
        /// controller method used for deleting a cloud
        /// </summary>
        /// <param name="cloudId">cloud id indentifying the cloud to be removed</param>
        /// <returns>
        /// returns cloud view
        /// </returns>
        [HttpPost]
        public async Task<ActionResult> AppDeleteCloud(int cloudId, string token, int userId)
        {
            AppDeleteCloudResponseDTO dto = new AppDeleteCloudResponseDTO();
            AuthorizationResponse auth = await authService.AppIsAuthenticated(token, userId);
            if (auth != AuthorizationResponse.Ok)
            {
                dto.Auth = auth;
                return Json(dto);
            }
            await cloudService.removeCloud(cloudId);
            dto.Auth = auth;
            return Json(dto);
        }
        /// <summary>
        /// controller method responsible for displaying the delete cloud view. This method does not remove the cloud from the database
        /// </summary>
        /// <param name="cloudId">cloud Id of the cloud to be removed</param>
        /// <returns>
        /// returns confirm delete cloud view
        /// </returns>
        public ActionResult DeleteCloud(int cloudId)
        {
            if (!authService.IsAuthenticated(Session))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Login to use this request");
            }
            ConfirmDeleteCloudViewModel view = new ConfirmDeleteCloudViewModel(cloudId);

            return View(view);
        }
        /// <summary>
        /// controller method responsible for displaying new cloud view. This controller does not create a new cloud
        /// </summary>
        /// <returns>
        /// returns new cloud view
        /// </returns>
        public ActionResult NewCloud()
        {
            if (!authService.IsAuthenticated(Session))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Login to use this request");
            }

            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Dropbox", Value = ProviderType.DropBox.ToString() });

            items.Add(new SelectListItem { Text = "Flickr", Value = ProviderType.Flicker.ToString(), Selected = true });

            ViewBag.Providers = items;
            return View();
        }

        /*
        public ActionResult ManageCloud(string cloud, int cloudId)
        {
            if (!authService.IsAuthenticated(Session))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Login to use this request");
            }
            MangeCloudViewModel view = new MangeCloudViewModel(cloudId, cloud);
            return View(view);
        }
        public async Task<ActionResult> ChangePassword(int cloudId, string oldPassword, string password, string password2)
        {
            string result = "";

            if (!authService.IsAuthenticated(Session))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Login to use this request");
            }
            if (password == password2)
            {
                if (await cloudService.ChangePassword(oldPassword, password, cloudId))
                {
                    result = "Success";
                }
                else
                {
                    result = "Old password does not match";
                }
            }
            else
            {
                result = "new passwords do not match";
            }
            ChangeCloudPasswordViewModel view = new ChangeCloudPasswordViewModel(result);
            return View(view);
        }*/
        /// <summary>
        /// this controller method is the first step to connecting with a provider.
        /// </summary>
        /// <param name="Providers">type of provider a new connection is to be made</param>
        /// <param name="accountName">custom name for cloud account</param>
        /// <returns>redirects to appropriate authentication page</returns>
        public ActionResult ConnectWithProvider(ProviderType Providers, string accountName)
        {
            if (!authService.IsAuthenticated(Session))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Login to use this request");
            }
            switch(Providers)
            {
                case ProviderType.Flicker:
                    Flickr f = FlickrManager.GetInstance();
                    var fullUrl = this.Url.Action("ConfirmFlickrConnection", "Cloud", new { accountName = accountName }, this.Request.Url.Scheme);
                    OAuthRequestToken token = f.OAuthGetRequestToken(fullUrl);
                    Session["RequestToken"] = token;
                    string url = f.OAuthCalculateAuthorizationUrl(token.Token, AuthLevel.Read);
                    return Redirect(url);
                case ProviderType.DropBox:
                    var dropboxRedirectUrl = this.Url.Action("ConfirmDropBoxConnection", "Cloud", null , this.Request.Url.Scheme);
                    DeviceService devService = new DeviceService();
                    Session["DropBoxState"] = devService.TrueRandomString(6);
                    Session["NewCloudName"] = accountName;
                    var redirect = DropboxOAuth2Helper.GetAuthorizeUri(OAuthResponseType.Code, ApiKeys.DropBoxApiKey , dropboxRedirectUrl, Session["DropBoxState"] as String);
                    return Redirect(redirect.ToString());
                default:
                    return View();
            }
        }

        /// <summary>
        /// Adds a new Flickr cloud
        /// </summary>
        /// <param name="accountName">custom name for cloud</param>
        /// <returns>
        /// view with result of adding
        /// </returns>
        public async Task<ActionResult> ConfirmFlickrConnection(string accountName)
        {
            if (!authService.IsAuthenticated(Session))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Login to use this request");
            }
            Flickr f = FlickrManager.GetInstance();
            OAuthRequestToken requestToken = Session["RequestToken"] as OAuthRequestToken;
            OAuthAccessToken accessToken = f.OAuthGetAccessToken(requestToken, Request.QueryString["oauth_verifier"]);
            await cloudService.CreateFlickerAccount(accessToken, accountName, authService.getLoggedInUsername(Session));
            return View();
        }

        /// <summary>
        /// Adds a new Dropbox cloud
        /// </summary>
        /// <param name="code">code</param>
        /// <param name="state">state</param>
        /// <returns>
        /// view with result of adding
        /// </returns>
        public async Task<ActionResult> ConfirmDropBoxConnection(string code, string state)
        {
            if (!authService.IsAuthenticated(Session))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Login to use this request");
            }
            string accountName = "Not Specified Name";
            if(Session["DropBoxState"] as String != state)
            {
                return Redirect("Index");
            }
            if(Session["NewCloudName"] != null)
            {
                accountName = Session["NewCloudName"] as string;
            }
            string redirectUrl = this.Url.Action("ConfirmDropBoxConnection", "Cloud", null, this.Request.Url.Scheme);
            OAuth2Response response = await DropboxOAuth2Helper.ProcessCodeFlowAsync(code, ApiKeys.DropBoxApiKey, ApiKeys.DropBoxApiKeySecret, redirectUrl);
            await cloudService.CreateDropBoxAccount(response.AccessToken, accountName, authService.getLoggedInUsername(Session), response.Uid);
            return View();
        }
        /// <summary>
        /// method for adding a cloud to the database
        /// </summary>
        /// <param name="accountId">account id</param>
        /// <param name="cloudName">custom cloud name</param>
        /// <param name="provider">provider of type CloudProviderType</param>
        /// <param name="token">token</param>
        /// <param name="tokenSecret">token secret</param>
        /// <param name="userId">user id</param>
        /// <returns>
        /// AppCreateCloudResponseDTO
        /// </returns>
        [HttpPut]
        public async Task<ActionResult> AppCreateCloud(int accountId, string cloudName, CloudProviderType provider, string token, string tokenSecret, string userId, string authToken)
        {
            AppCreateCloudResponseDTO dto = new AppCreateCloudResponseDTO();
            AuthorizationResponse auth = await authService.AppIsAuthenticated(authToken, accountId);
            if (auth != AuthorizationResponse.Ok)
            {
                dto.Auth = auth;
                return Json(dto);
            }
            dto.Message = await cloudService.AppCreateCloud(accountId, cloudName, provider, token, tokenSecret, userId);
            dto.Auth = auth;
            return Json(dto);
        }
        /// <summary>
        /// returns all clouds connected to account
        /// </summary>
        /// <returns>
        /// AppGetCloudsResponseDTO
        /// </returns>
        [HttpPost]
        public async Task<ActionResult> AppGetClouds(string token, int userId)
        {
            AppGetCloudsResponseDTO dto = new AppGetCloudsResponseDTO();
            AuthorizationResponse auth = await authService.AppIsAuthenticated(token, userId);
            if (auth != AuthorizationResponse.Ok)
            {
                dto.Auth = auth;
                return Json(dto);
            }
            string user = await authService.GetAccountLogin(userId);
            List<Cloud> clouds = await cloudService.GetClouds(user);
            List<RCloud> rClouds = new List<RCloud>();
            foreach(var c in clouds)
            {
                RCloud rCloud = new RCloud();
                rCloud.CloudName = c.Login;
                if(c.Provider == ProviderType.DropBox)
                {
                    rCloud.provider = CloudProviderType.Dropbox;
                }else if(c.Provider == ProviderType.Flicker)
                {
                    rCloud.provider = CloudProviderType.Flickr;
                }
                rCloud.CloudId = c.Id;
                rCloud.Token = c.Token;
                rCloud.TokenSecret = c.TokenSecret;
                rCloud.UserId = c.FlickrUserId;
                rClouds.Add(rCloud);
            }
            dto.clouds = rClouds;
            dto.Auth = auth;
            return Json(dto);
        }
        #endregion methods
    }
}