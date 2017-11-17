﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    public class CloudController : Controller
    {
        // GET: Cloud
        public ActionResult Index()
        {
            Cloud cloud1 = new Cloud(ProviderType.DropBox, "amps@gmail.com");
            Cloud cloud2 = new Cloud(ProviderType.Flicker, "amps@gmail.com");
            Cloud cloud3 = new Cloud(ProviderType.DropBox, "sdmkas@gmail.com");
            List<Cloud> clouds = new List<Cloud>();
            clouds.Add(cloud1);
            clouds.Add(cloud2);
            clouds.Add(cloud3);
            CloudViewModel model = new CloudViewModel(clouds);

            return View(model);
        }
    }
}