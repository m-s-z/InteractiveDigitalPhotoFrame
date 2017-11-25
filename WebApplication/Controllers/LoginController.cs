using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication.Data;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    public class LoginController : Controller
    {
        private ApplicationContext db = new ApplicationContext();
        AuthenticationService authService = new AuthenticationService();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string login, string password)
        {
            Session["UserId"] = login;
            return RedirectToAction("../Home/Index");
        }

        public ActionResult LogOut()
        {
            if (!authService.IsAuthenticated(Session))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Login to use this request");
            }
            Session["UserId"] = null;
            return RedirectToAction("Index");
        }
    }
}