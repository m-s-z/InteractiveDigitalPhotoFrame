using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    /// <summary>
    /// Folder Model class
    /// </summary>
    public class Folder
    {
        #region properties
        /// <summary>
        /// id
        /// </summary>
        public int FolderId { get; set; }
        /// <summary>
        /// name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// dedvice Id
        /// </summary>
        public int DeviceId { get; set; }
        /// <summary>
        /// instancec of Device model class
        /// </summary>
        public Device Device { get; set; }
        /// <summary>
        /// cloud Id
        /// </summary>
        public int CloudId { get; set; }
        /// <summary>
        /// instance of Cloud model class
        /// </summary>
        public Cloud Cloud { get; set; }
        #endregion properties
        /// <summary>
        /// constructor for folder model class
        /// </summary>
        public Folder()
        {

        }
        /// <summary>
        /// constructor for folder model class
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="deviceId">device id</param>
        /// <param name="cloudId">cloud id</param>
        public Folder(string name, int deviceId, int cloudId)
        {
            Name = name;
            DeviceId = deviceId;
            CloudId = cloudId;
        }
        /// <summary>
        /// constructor for folder model class
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="device">instance of device model class</param>
        /// <param name="cloud">instance of cloud model class</param>
        public Folder(string name, Device device, Cloud cloud)
        {
            Name = name;
            Device = device;
            Cloud = cloud;
        }
    }
}