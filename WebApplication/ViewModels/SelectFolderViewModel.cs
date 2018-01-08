using FlickrNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.ViewModels
{
    /// <summary>
    /// universal folder class
    /// </summary>
    public class UniversalFolder
    {
        /// <summary>
        /// constructor for UniversalFolder class
        /// </summary>
        public UniversalFolder()
        {

        }
        /// <summary>
        /// constructor for UniversalFolder class
        /// </summary>
        /// <param name="title">title</param>
        /// <param name="numberOfPhotos">number of photos</param>
        /// <param name="dateUpdated">last updated date</param>
        public UniversalFolder(string title, int numberOfPhotos, DateTime dateUpdated)
        {
            Title = title;
            NumberOfPhotos = numberOfPhotos;
            DateUpdated = dateUpdated;
        }
        #region properties
        /// <summary>
        /// title of folder, in case of dropbox the full path
        /// </summary>
        public String Title { get; set; }
        /// <summary>
        /// number of photos in folder
        /// </summary>
        public int NumberOfPhotos { get; set; }
        /// <summary>
        /// date that the file was last updated
        /// </summary>
        public DateTime DateUpdated { get; set; }
        #endregion properties
    }
    /// <summary>
    /// select folder view model class
    /// </summary>
    public class SelectFolderViewModel
    {
        /// <summary>
        /// constructor for SelectFolderViewModel class
        /// </summary>
        public SelectFolderViewModel()
        {

        }
        /// <summary>
        /// constructor for SelectFolderViewModel class
        /// </summary>
        /// <param name="selectedCloud">instance of cloud model</param>
        /// <param name="folders">list of universal folder class</param>
        /// <param name="deviceId">id</param>
        public SelectFolderViewModel(Cloud selectedCloud, List<UniversalFolder> folders, int deviceId)
        {
            SelectedCloud = selectedCloud;
            Folders = folders;
            DeviceId = deviceId;
        }
        #region properties
        /// <summary>
        /// device id to which we will select folders
        /// </summary>
        public int DeviceId { get; set; }
        /// <summary>
        /// list of strings (folder names that where selected. This field gets filled in the view
        /// </summary>
        public IEnumerable<String> SelectedFolders { get; set; }
        /// <summary>
        /// instance of cloud model from which we pull folders to display
        /// </summary>
        public Cloud SelectedCloud { get; set; }
        /// <summary>
        /// list of folders from which one can select foder names
        /// </summary>
        public List<UniversalFolder> Folders { get; set; }
        #endregion properties
    }
}