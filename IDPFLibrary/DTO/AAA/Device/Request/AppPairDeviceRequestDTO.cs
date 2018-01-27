using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDPFLibrary.DTO.AAA.Device.Request
{
    public class AppPairDeviceRequestDTO
    {
        #region properties
        /// <summary>
        /// paircode displayed on the device
        /// </summary>
        public String PairCode { get; set; }

        /// <summary>
        /// Custom device name for the device
        /// </summary>
        public string DeviceName { get; set; }
        /// <summary>
        /// provider user id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// authentication token
        /// </summary>
        public string Token { get; set; }
        #endregion properties
        /// <summary>
        /// constructor for AppPairDeviceRequestDTO
        /// </summary>
        public AppPairDeviceRequestDTO()
        {

        }
    }
}
