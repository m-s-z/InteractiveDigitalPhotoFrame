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

        public ActionResult AddFolder(int DeviceId)
        {
            return Content(DeviceId.ToString());
        }

        //string is nullable so parameter is not necessary
        public ActionResult Folders(string sortBy)
        {
            if(String.IsNullOrWhiteSpace(sortBy))
            {
                sortBy = "decreasing";
            }
            return Content(String.Format("sort by = {0}", sortBy));
        }

        [Route("folder/BindDevice/{Folderid:min(0)}/{DeviceId:min(0)}")]
        public ActionResult BindDevice(int FolderId, int DeviceId)
        {
            return Content("fodler id = " + FolderId + " device ID " + DeviceId);
        }
    }
}