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
    class AppGetDevicesRequestDTO
    {
        #region properties
        public int AccountId { get; set; }
        #endregion properties
        public AppGetDevicesRequestDTO()
        {

        }
    }
}
