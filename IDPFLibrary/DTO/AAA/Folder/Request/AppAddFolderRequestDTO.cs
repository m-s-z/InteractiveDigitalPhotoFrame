using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDPFLibrary.DTO.AAA.Folder.Request
{
    /// <summary>
    /// request class for AppAddFolder controller method
    /// </summary>
    public class AppAddFolderRequestDTO
    {
        #region properties
        /// <summary>
        /// list of strings with folder names
        /// </summary>
        public List<string> Folders { get; set; }
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
        /// constructor for AppAddFolderRequestDTO class
        /// </summary>
        public AppAddFolderRequestDTO()
        {

        }
    }
}
