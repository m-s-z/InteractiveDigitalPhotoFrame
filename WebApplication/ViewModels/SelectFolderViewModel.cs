using FlickrNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.ViewModels
{
    public class SelectFolderViewModel
    {
        public SelectFolderViewModel()
        {

        }

        public SelectFolderViewModel(Cloud selectedCloud, List<Photoset> folders, int deviceId)
        {
            SelectedCloud = selectedCloud;
            Folders = folders;
            DeviceId = deviceId;
        }
        public int DeviceId { get; set; }
        public IEnumerable<String> SelectedFolders { get; set; }
        public Cloud SelectedCloud { get; set; }
        public List<Photoset> Folders { get; set; }
    }
}