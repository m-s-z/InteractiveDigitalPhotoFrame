using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDPFLibrary.DTO.AAA.Account.Request
{
    /// <summary>
    /// response class for AppChangePassword controller method
    /// </summary>
    public class AppChangePasswordRequestDTO
    {
        #region properties
        /// <summary>
        /// old password
        /// </summary>
        public string OldPassword { get; set; }
        /// <summary>
        /// password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// second password should be the same as password
        /// </summary>
        public string  Password2 { get; set; }
        /// <summary>
        /// id for the account that the password should be changed to
        /// </summary>
        public int AccountId { get; set; }
        #endregion properties
        /// <summary>
        /// constructor for AppChangePasswordRequestDTO class
        /// </summary>
        public AppChangePasswordRequestDTO()
        {

        }
    }
}
