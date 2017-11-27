using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication.Data;
using WebApplication.Services;
using WebApplication.ViewModels;

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
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterConfirm(string login, string password, string password2)
        {
            RegisterConfirmViewModel view = new RegisterConfirmViewModel("success");
            return View(view);
        }
    }
}