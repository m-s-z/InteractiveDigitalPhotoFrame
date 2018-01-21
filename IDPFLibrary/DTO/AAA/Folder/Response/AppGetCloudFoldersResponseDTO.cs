using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDPFLibrary.DTO.AAA.Folder.Response
{
    /// <summary>
    /// Helper class for universal folder
    /// </summary>
    public class SUniversalFolder
    {
        #region properties
        /// <summary>
        /// title of folder, in case of dropbox the full path
        /// </summary>
        public String Title { get; set; }
        /// <summary>
        /// number of photos in folder
        /// </summary>
        public int NumberOfPhotos { get; set; }
        /// <summary>
        /// date that the file was last updated
        /// </summary>
        public DateTime DateUpdated { get; set; }
        #endregion properties
        /// <summary>
        /// constructor for SUniversalFolder
        /// </summary>
        public SUniversalFolder()
        {

        }
    }
    /// <summary>
    /// response class for AppGetCloudFolders controller method
    /// </summary>
    public class AppGetCloudFoldersResponseDTO
    {
        /// <summary>
        /// List of universal folders
        /// </summary>
        public List<SUniversalFolder> folders { get; set; }
        /// <summary>
        /// Authorization Response 
        /// </summary>
        public AuthorizationResponse Auth { get; set; }
        /// <summary>
        /// constructor for AppGetCloudFoldersResponseDTO class
        /// </summary>
        public AppGetCloudFoldersResponseDTO()
        {

        }
    }
}
