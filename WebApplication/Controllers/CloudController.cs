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
using WebApplication.Utils;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    public class CloudController : Controller
    {
        private ApplicationContext db = new ApplicationContext();
        AuthenticationService authService = new AuthenticationService();
        CloudService cloudService = new CloudService();
        // GET: Cloud
        public async Task<ActionResult> Index()
        {
            if (!authService.IsAuthenticated(Session))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Login to use this request");
            }
            List<Cloud> clouds = await cloudService.GetClouds(authService.getLoggedInUsername(Session));
            
            CloudViewModel model = new CloudViewModel(clouds);

            return View(model);
        }
        public async Task<ActionResult> ConfirmDeleteCloud(int cloudId)
        {
            if (!authService.IsAuthenticated(Session))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Login to use this request");
            }
            await cloudService.removeCloud(cloudId);
            return Redirect("Index");
        }

        public ActionResult DeleteCloud(int cloudId)
        {
            if (!authService.IsAuthenticated(Session))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Login to use this request");
            }
            ConfirmDeleteCloudViewModel view = new ConfirmDeleteCloudViewModel(cloudId);

            return View(view);
        }

        public ActionResult NewCloud()
        {
            if (!authService.IsAuthenticated(Session))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Login to use this request");
            }

            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Drop Box", Value = ProviderType.DropBox.ToString() });

            items.Add(new SelectListItem { Text = "Flickr", Value = ProviderType.Flicker.ToString(), Selected = true });

            items.Add(new SelectListItem { Text = "Google Drive", Value = ProviderType.GoogleDrive.ToString()});

            items.Add(new SelectListItem { Text = "One Drive", Value = ProviderType.OneDrive.ToString()});

            ViewBag.Providers = items;
            return View();
        }

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
        }

        public ActionResult ConnectWithProvider(ProviderType Providers, string accountName)
        {
            if (!authService.IsAuthenticated(Session))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Login to use this request");
            }
            Flickr f = FlickrManager.GetInstance();
            var fullUrl = this.Url.Action("ConfirmFlickrConnection", "Cloud", new { accountName = accountName }, this.Request.Url.Scheme);
            OAuthRequestToken token = f.OAuthGetRequestToken(fullUrl);
            Session["RequestToken"] = token;
            string url = f.OAuthCalculateAuthorizationUrl(token.Token, AuthLevel.Read);
            return Redirect(url);
        }

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
    }
}