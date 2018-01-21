using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDPFLibrary.DTO.AAA.Account.Response
{
    /// <summary>
    /// response class for AppChangePassword controller method
    /// </summary>
    public class AppChangePasswordResponseDTO
    {
        #region properties
        /// <summary>
        /// response message string
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Authorization Response 
        /// </summary>
        public AuthorizationResponse Auth { get; set; }
        #endregion properties
        /// <summary>
        /// constructor for AppChangePasswordResponseDTO
        /// </summary>
        public AppChangePasswordResponseDTO()
        {

        }
    }
}
