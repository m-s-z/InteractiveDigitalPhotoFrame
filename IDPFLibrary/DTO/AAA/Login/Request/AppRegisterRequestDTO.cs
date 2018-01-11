using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDPFLibrary.DTO.AAA.Login.Request
{
    /// <summary>
    /// request class for AppRegister controller method
    /// </summary>
    public class AppRegisterRequestDTO
    {
        #region properties
        /// <summary>
        /// login
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// Hashed password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Hashed password repeated should be the same as Password
        /// </summary>
        public string Password2 { get; set; }
        #endregion properties
        /// <summary>
        /// contructor for AppRegisterRequestDTO class
        /// </summary>
        public AppRegisterRequestDTO()
        {

        }
    }
}
