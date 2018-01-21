using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDPFLibrary.DTO.AAA.Folder.Response
{
    /// <summary>
    /// response class for AppAddFolder controller method
    /// </summary>
    public class AppAddFolderResponseDTO
    {
        /// <summary>
        /// Authorization Response 
        /// </summary>
        public AuthorizationResponse Auth { get; set; }
        /// <summary>
        /// constructor for AppAddFolderResponseDTO class
        /// </summary>
        public AppAddFolderResponseDTO()
        {

        }
    }
}
