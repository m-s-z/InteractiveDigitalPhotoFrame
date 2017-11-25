using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class LoginController : Controller
    {
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
            Session["UserId"] = null;
            return RedirectToAction("Index");
        }
    }
}