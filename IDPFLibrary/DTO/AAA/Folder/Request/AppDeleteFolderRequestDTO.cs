using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDPFLibrary.DTO.AAA.Folder.Request
{
    /// <summary>
    /// request class for AppDeleteFolder controller method
    /// </summary>
    public class AppDeleteFolderRequestDTO
    {
        #region properties
        /// <summary>
        /// folder id
        /// </summary>
        public int FolderId { get; set; }

        /// <summary>
        /// Account ID.
        /// </summary>
        public int AccountId { get; set; }
        #endregion properties
        /// <summary>
        /// constructor for AppDeleteFolderRequestDTO
        /// </summary>
        public AppDeleteFolderRequestDTO()
        {

        }
    }
}
