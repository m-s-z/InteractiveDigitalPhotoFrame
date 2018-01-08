using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;

namespace WebApplication.ViewModels
{
    /// <summary>
    /// device view model class
    /// </summary>
    public class DeviceViewModel
    {
        /// <summary>
        /// constructor for DeviceViewModel
        /// </summary>
        /// <param name="devices">List of device model class</param>
        public DeviceViewModel(List<DeviceName> devices)
        {
            Devices = devices;
        }
        #region properties
        /// <summary>
        /// list of device model class to be displayed
        /// </summary>
        public List<DeviceName> Devices { get; set; }
        #endregion properties
    }
}