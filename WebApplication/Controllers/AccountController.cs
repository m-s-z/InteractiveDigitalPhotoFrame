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
    public class AccountController : Controller
    {
        #region fields
        /// <summary>
        /// authentication service for authentication handling
        /// </summary>
        IAuthenticationService authService = new AuthenticationService();
        #endregion fields

        /// <summary>
        /// Constructor for AccountController
        /// </summary>
        public AccountController()
        {

        }

        /// <summary>
        /// Constructor for AccountController
        /// </summary>
        /// <param name="auth">instacne of authentication service</param>
        public AccountController(IAuthenticationService auth)
        {
            authService = auth;
        }
        #region methods
        /// <summary>
        /// prepares the account view
        /// </summary>
        /// <returns>
        /// account view
        /// </returns>
        public ActionResult Index()
        {
            Account account = new Account((string) Session["UserId"]);
            AccountViewModel model = new AccountViewModel(account);
            return View(model);
        }

        /// <summary>
        /// Changes password an account
        /// </summary>
        /// <param name="oldPassword">old password</param>
        /// <param name="password">new password</param>
        /// <param name="password2">new password repeated</param>
        /// <param name="id">account id to be changed.</param>
        /// <returns>
        /// ChangePassword view 
        /// </returns>
        [HttpPost]
        public async Task<ActionResult> ChangePassword(string oldPassword, string password, string password2, int id)
        {
            string result = "";
            if (!authService.IsAuthenticated(Session))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Login to use this request");
            }
            if (password == password2)
            {
                if (await authService.ChangePassword(oldPassword, password, authService.getLoggedInUsername(Session)))
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
            ChangePasswordViewModel view = new ChangePasswordViewModel(result);
            return View(view);
        }

        #endregion
        /*
        [HttpGet]
        public async Task<ActionResult> GetAccount()
        {
            if (!authService.IsAuthenticated(Session))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Login to use this request");
            }
            var sessionAccount = Session["UserId"];
            var account = db.Accounts.FirstOrDefault(a => a.Login == (string) sessionAccount);
            return Json(account.Login, JsonRequestBehavior.AllowGet);
        }
        */
    }
}