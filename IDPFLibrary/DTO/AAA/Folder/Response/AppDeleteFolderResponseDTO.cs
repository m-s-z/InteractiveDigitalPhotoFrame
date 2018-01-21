using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDPFLibrary.DTO.AAA.Folder.Response
{
    /// <summary>
    /// reposne class for AppDeleteFolder controller method
    /// </summary>
    public class AppDeleteFolderResponseDTO
    {
        /// <summary>
        /// Authorization Response 
        /// </summary>
        public AuthorizationResponse Auth { get; set; }
        /// <summary>
        /// constructor for AppDeleteFolderResponseDTO
        /// </summary>
        public AppDeleteFolderResponseDTO()
        {

        }
    }
}
