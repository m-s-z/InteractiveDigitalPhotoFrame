using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDPFLibrary.DTO.AAA.Folder.Response
{
    /// <summary>
    /// Helper class to emulate Folder model class
    /// </summary>
    public class SFolder
    {
        #region properties
        /// <summary>
        /// folder id
        /// </summary>
        public int FolderId { get; set; }
        /// <summary>
        /// folder name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// device id
        /// </summary>
        public int DeviceId { get; set; }
        /// <summary>
        /// cloud id
        /// </summary>
        public int CloudId { get; set; }
        #endregion properties
        /// <summary>
        /// constructor for SFolder class
        /// </summary>
        public SFolder()
        {

        }
    }
    /// <summary>
    /// response class for AppGetDeviceFolder controller method
    /// </summary>
    public class AppGetDeviceFoldersResponseDTO
    {
        /// <summary>
        /// list of folders
        /// </summary>
        public List<SFolder> Folders { get; set; }
        /// <summary>
        /// Authorization Response 
        /// </summary>
        public AuthorizationResponse Auth { get; set; }
        /// <summary>
        /// constructor for AppGetDeviceFoldersResponseDTO class
        /// </summary>
        public AppGetDeviceFoldersResponseDTO()
        {

        }
    }
}
