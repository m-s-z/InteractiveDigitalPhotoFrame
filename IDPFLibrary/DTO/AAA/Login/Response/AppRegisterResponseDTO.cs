using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDPFLibrary.DTO.AAA.Login.Response
{
    /// <summary>
    /// response class for AppRegister controller method
    /// </summary>
    public class AppRegisterResponseDTO
    {
        #region properties
        /// <summary>
        /// string message
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Flag indicating whether the register was successful or not.
        /// </summary>
        public bool IsSuccess { get; set; }
        #endregion properties
        /// <summary>
        /// constructor for AppRegisterResponseDTO class
        /// </summary>
        public AppRegisterResponseDTO()
        {

        }
    }
}
