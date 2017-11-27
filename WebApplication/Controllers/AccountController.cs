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
    public class AccountController : Controller
    {
        private ApplicationContext db = new ApplicationContext();
        AuthenticationService authService = new AuthenticationService();
        // GET: Account
        public ActionResult Index()
        {
            Account account = new Account((string) Session["UserId"]);
            AccountViewModel model = new AccountViewModel(account);
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> ChangeLogin(Account account, int id)
        {
            if (!authService.IsAuthenticated(Session))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Login to use this request");
            }
            return View(account);
        }

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
    }
}