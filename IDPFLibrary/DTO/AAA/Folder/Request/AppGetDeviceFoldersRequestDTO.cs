using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDPFLibrary.DTO.AAA.Folder.Request
{
    /// <summary>
    /// request class for AppGetDeviceFolders controller method
    /// </summary>
    public class AppGetDeviceFoldersRequestDTO
    {
        #region properties
        /// <summary>
        /// Account ID.
        /// </summary>
        public int AccountId { get; set; }
        /// <summary>
        /// device id
        /// </summary>
        public int DeviceId { get; set; }
        /// <summary>
        /// authentication token
        /// </summary>
        public string Token { get; set; }
        #endregion properties
        /// <summary>
        /// constructor for AppGetDeviceFoldersRequestDTO class
        /// </summary>
        public AppGetDeviceFoldersRequestDTO()
        {

        }
    }
}
