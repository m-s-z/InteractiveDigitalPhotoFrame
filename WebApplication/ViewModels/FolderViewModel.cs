using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;

namespace WebApplication.ViewModels
{
    public class FolderViewModel
    {
        public FolderViewModel(Device device, List<Folder> folders)
        {
            Device = device;
            Folders = folders;
        }

        public Device Device { get; set; }
        public List<Folder> Folders { get; set; }
    }
}