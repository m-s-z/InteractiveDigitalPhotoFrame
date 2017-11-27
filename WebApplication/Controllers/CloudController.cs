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
    public class CloudController : Controller
    {
        private ApplicationContext db = new ApplicationContext();
        AuthenticationService authService = new AuthenticationService();
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
        public async Task<ActionResult> ConfirmDeleteCloud(int cloudId)
        {
            if (!authService.IsAuthenticated(Session))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Login to use this request");
            }
            //next part maybe use services?
            /*
            Cloud cloud = await db.Clouds.FindAsync(cloudId);
            if (cloud != null)
            {
                db.Clouds.Remove(cloud);

            }
            await db.SaveChangesAsync();*/
            return Redirect("Index");
        }

        public ActionResult DeleteCloud(int cloudId)
        {
            ConfirmDeleteCloudViewModel view = new ConfirmDeleteCloudViewModel(cloudId);

            return View(view);
        }

        public ActionResult NewCloud()
        {
            return View();
        }

        public ActionResult ManageCloud(string cloud)
        {
            MangeCloudViewModel view = new MangeCloudViewModel(cloud);
            return View(view);
        }
    }
}