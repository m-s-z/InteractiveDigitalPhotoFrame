using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.ViewModels
{
    /// <summary>
    /// new folder view model class
    /// </summary>
    public class NewFolderViewModel
    {
        /// <summary>
        /// constructor for NewFolderViewModel class
        /// </summary>
        public NewFolderViewModel()
        {
                
        }
        /// <summary>
        /// constructor for NewFolderViewModel class
        /// </summary>
        /// <param name="deviceId">id</param>
        public NewFolderViewModel(int deviceId)
        {
            this.DeviceId = deviceId;
        }
        #region properties
        /// <summary>
        /// device of id to add the folder to
        /// </summary>
        public int DeviceId { get; set; }
        #endregion properties
    }
}