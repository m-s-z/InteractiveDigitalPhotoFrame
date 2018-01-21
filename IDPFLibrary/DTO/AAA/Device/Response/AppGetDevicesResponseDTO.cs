using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDPFLibrary.DTO.AAA.Device.Response
{
    /// <summary>
    /// inner class to substitue Device Name model class
    /// </summary>
    public class SDeviceName
    {
        #region properties
        /// <summary>
        /// device id
        /// </summary>
        public int DeviceId { get; set; }
        /// <summary>
        /// Custom name for device
        /// </summary>
        public string Name { get; set; }
        #endregion properties
        /// <summary>
        /// constructor for SDeviceName
        /// </summary>
        public SDeviceName()
        {

        }
    }

    /// <summary>
    /// response dto for AppGetDevices controller method
    /// </summary>
    public class AppGetDevicesResponseDTO
    {
        #region properties
        /// <summary>
        /// account id
        /// </summary>
        public List<SDeviceName> Devices { get; set; }
        /// <summary>
        /// Authorization Response 
        /// </summary>
        public AuthorizationResponse Auth { get; set; }
        #endregion properties
        /// <summary>
        /// constructor for AppGetDevicesResponseDTO
        /// </summary>
        public AppGetDevicesResponseDTO()
        {

        }
    }
}
