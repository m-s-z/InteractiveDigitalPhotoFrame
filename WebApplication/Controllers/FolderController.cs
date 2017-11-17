using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class FolderController : Controller
    {
        // GET: Folder
        public ActionResult Index()
        {
            var folder = new Folder("summer", 1,1);
            return View(folder);
        }
    }
}