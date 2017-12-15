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
    public class CloudController : Controller
    {
        private ApplicationContext db = new ApplicationContext();
        AuthenticationService authService = new AuthenticationService();
        CloudService cloudService = new CloudService();
        // GET: Cloud
        public async Task<ActionResult> Index()
        {
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
            if (!authService.IsAuthenticated(Session))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Login to use this request");
            }
            string result = "success";
            ChangeCloudPasswordViewModel view = new ChangeCloudPasswordViewModel(result);
            return View(view);
        }
    }
}