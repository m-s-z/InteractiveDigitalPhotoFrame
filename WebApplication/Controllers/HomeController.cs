using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    /// <summary>
    /// Controller responsible for the home page
    /// </summary>
    public class HomeController : Controller
    {
        #region methods
        //should not be authenticated since this runs before we can redirect to login
        /// <summary>
        /// displays the home page
        /// </summary>
        /// <returns>
        /// Home view
        /// </returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        #endregion methods
    }
}
