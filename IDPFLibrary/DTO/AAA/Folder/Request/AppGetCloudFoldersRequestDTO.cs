using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDPFLibrary.DTO.AAA.Folder.Request
{
    /// <summary>
    /// request class for AppGetCloudFolder controller method
    /// </summary>
    public class AppGetCloudFoldersRequestDTO
    {
        #region properties
        /// <summary>
        /// cloud id
        /// </summary>
        public int CloudId { get; set; }
        /// <summary>
        /// device id
        /// </summary>
        public int DeviceId { get; set; }
        #endregion properties
        /// <summary>
        /// constructor for AppGetCloudFoldersRequestDTO class
        /// </summary>
        public AppGetCloudFoldersRequestDTO()
        {

        }
    }
}
