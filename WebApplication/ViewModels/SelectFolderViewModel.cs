using FlickrNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.ViewModels
{
    public class UniversalFolder
    {
        public UniversalFolder()
        {

        }

        public UniversalFolder(string title, int numberOfPhotos, DateTime dateUpdated)
        {
            Title = title;
            NumberOfPhotos = numberOfPhotos;
            DateUpdated = dateUpdated;
        }

        public String Title { get; set; }
        public int NumberOfPhotos { get; set; }
        public DateTime DateUpdated { get; set; }
    }
    public class SelectFolderViewModel
    {
        public SelectFolderViewModel()
        {

        }

        public SelectFolderViewModel(Cloud selectedCloud, List<UniversalFolder> folders, int deviceId)
        {
            SelectedCloud = selectedCloud;
            Folders = folders;
            DeviceId = deviceId;
        }
        public int DeviceId { get; set; }
        public IEnumerable<String> SelectedFolders { get; set; }
        public Cloud SelectedCloud { get; set; }
        public List<UniversalFolder> Folders { get; set; }
    }
}