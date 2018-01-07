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
    /// Controller for logging in/out nad registering
    /// </summary>
    public class LoginController : Controller
    {
        #region fields
        /// <summary>
        /// authentication service for authentication handling
        /// </summary>
        IAuthenticationService authService = new AuthenticationService();
        #endregion fields

        public LoginController()
        {

        }
        public LoginController(IAuthenticationService auth)
        {
            authService = auth;
        }

        #region methods
        /// <summary>
        /// method for displaying login screen
        /// </summary>
        /// <returns>
        /// Login view
        /// </returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Controller method for loggin accepting credentials. On succes it sets Session["UserId"]
        /// </summary>
        /// <param name="login">login</param>
        /// <param name="password">password</param>
        /// <returns>On success redirect to home
        /// On failure returns login view</returns>
        [HttpPost]
        public async Task<ActionResult> Login(string login, string password)
        {
            if (await authService.Login(login,password))
            {
                Session["UserId"] = login;
                return RedirectToAction("../Home/Index");
            }else
            {
                TempData["LoginFailed"] = true;
                return RedirectToAction("Index");
            }
            
            
        }

        /// <summary>
        /// logs out of the service
        /// </summary>
        /// <returns>
        /// login view
        /// </returns>
        public ActionResult LogOut()
        {
            if (!authService.IsAuthenticated(Session))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Login to use this request");
            }
            Session["UserId"] = null;
            return RedirectToAction("Index");
        }

        /// <summary>
        /// preperas view for registering
        /// </summary>
        /// <returns>
        /// register view
        /// </returns>
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// registers a new account
        /// </summary>
        /// <param name="login">new login</param>
        /// <param name="password">password</param>
        /// <param name="password2">password repeated</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> RegisterConfirm(string login, string password, string password2)
        {
            string registrationResult = "";
            if(password != password2)
            {
                registrationResult = "passwords do not match";
            }
            else
            {
                registrationResult = await authService.RegisterAccount(login, password);
            }
            RegisterConfirmViewModel view = new RegisterConfirmViewModel(registrationResult);
            return View(view);
        }
        #endregion methods
    }
}