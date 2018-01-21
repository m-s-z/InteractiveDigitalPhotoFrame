using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDPFLibrary.DTO.AAA.Cloud.Response
{
    /// <summary>
    /// response class for AppDeleteCloud controller method
    /// </summary>
    public class AppDeleteCloudResponseDTO
    {
        /// <summary>
        /// Authorization Response 
        /// </summary>
        public AuthorizationResponse Auth { get; set; }
        /// <summary>
        /// constructor for AppDeleteCloudResponseDTO
        /// </summary>
        public AppDeleteCloudResponseDTO()
        {

        }
    }
}
