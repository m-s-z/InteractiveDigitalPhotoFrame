using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDPFLibrary.DTO.AAA.Device.Request
{
    class AppPairDeviceRequestDTO
    {
        #region properties
        /// <summary>
        /// paircode displayed on the device
        /// </summary>
        public String PairCode { get; set; }

        /// <summary>
        /// Custom device name for the device
        /// </summary>
        public String DeviceName { get; set; }
        #endregion properties
        /// <summary>
        /// constructor for AppPairDeviceRequestDTO
        /// </summary>
        public AppPairDeviceRequestDTO()
        {

        }
    }
}
