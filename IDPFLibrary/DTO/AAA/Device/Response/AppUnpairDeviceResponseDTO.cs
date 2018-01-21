using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDPFLibrary.DTO.AAA.Device.Response
{
    /// <summary>
    /// Response class for AppUnpairDevice controller method
    /// </summary>
    public class AppUnpairDeviceResponseDTO
    {
        #region properties
        /// <summary>
        /// Authorization Response 
        /// </summary>
        public AuthorizationResponse Auth { get; set; }
        #endregion properties
        /// <summary>
        /// constructor for AppUnpairDevice
        /// </summary>
        public AppUnpairDeviceResponseDTO()
        {
                
        }
    }
}
