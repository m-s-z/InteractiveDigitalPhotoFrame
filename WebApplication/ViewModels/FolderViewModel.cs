using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;

namespace WebApplication.ViewModels
{
    /// <summary>
    /// folder view model class
    /// </summary>
    public class FolderViewModel
    {
        /// <summary>
        /// constructor for FolderViewModel
        /// </summary>
        /// <param name="devices">List of device model class</param>
        /// <param name="folders">list of folder model class</param>
        public FolderViewModel(List<DeviceName> devices, List<Folder> folders)
        {
            Devices = devices;
            Folders = folders;
        }
        /// <summary>
        /// constructor for FolderViewModel
        /// </summary>
        /// <param name="devices">List of device model class</param>
        /// <param name="folders">list of folder model class</param>
        /// <param name="indexOfOpenDevice">index of device</param>
        public FolderViewModel(List<DeviceName> devices, List<Folder> folders, int indexOfOpenDevice)
        {
            Devices = devices;
            Folders = folders;
            IndexOfOpenDevice = indexOfOpenDevice;
        }
        #region properties
        /// <summary>
        /// list of device model class to be displayed
        /// </summary>
        public List<DeviceName> Devices { get; set; }
        /// <summary>
        /// list of folder model class to be displayed
        /// </summary>
        public List<Folder> Folders { get; set; }
        /// <summary>
        /// the folders for this id of divice will not be collapsed
        /// </summary>
        public int IndexOfOpenDevice { get; set; }
        #endregion properties
    }
}