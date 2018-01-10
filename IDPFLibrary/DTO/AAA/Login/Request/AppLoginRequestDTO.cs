using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDPFLibrary.DTO.AAA.Login.Request
{
    /// <summary>
    /// request class for AppLogin controller method
    /// </summary>
    public class AppLoginRequestDTO
    {
        /// <summary>
        /// login for the account
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// string with hashed password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// constructor for AppLoginRequestDTO class
        /// </summary>
        public AppLoginRequestDTO()
        {

        }
    }
}
