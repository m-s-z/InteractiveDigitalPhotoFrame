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
        /// device id
        /// </summary>
        public int DeviceId { get; set; }
        /// <summary>
        /// constructor for AppGetDeviceFoldersRequestDTO class
        /// </summary>
        #endregion properties
        public AppGetDeviceFoldersRequestDTO()
        {

        }
    }
}
