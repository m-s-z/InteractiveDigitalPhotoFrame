using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDPFLibrary.DTO.AAA.Device.Request
{
    
    /// <summary>
    /// Request dto for AppGetDevices controller method
    /// </summary>
    public class AppGetDevicesRequestDTO
    {
        #region properties
        /// <summary>
        /// account id
        /// </summary>
        public int AccountId { get; set; }
        #endregion properties
        /// <summary>
        /// constructor for AppGetDevicesRequestDTO class
        /// </summary>
        public AppGetDevicesRequestDTO()
        {

        }
    }
}
