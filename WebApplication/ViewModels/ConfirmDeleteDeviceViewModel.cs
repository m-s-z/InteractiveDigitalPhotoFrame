using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.ViewModels
{
    /// <summary>
    /// Confirm delete device view model
    /// </summary>
    public class ConfirmDeleteDeviceViewModel
    {
        /// <summary>
        /// constructor for ConfirmDeleteDeviceViewModel
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="name">name</param>
        public ConfirmDeleteDeviceViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
        #region properties
        /// <summary>
        /// id of device to be removed
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// name of device to be removed, only for display the device is being removed based on id
        /// </summary>
        public String Name { get; set; }
        #endregion properties
    }
}