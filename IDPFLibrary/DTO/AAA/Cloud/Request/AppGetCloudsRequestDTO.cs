using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDPFLibrary.DTO.AAA.Cloud.Request
{
    /// <summary>
    /// request dto for AppGetClouds controller method
    /// </summary>
    public class AppGetCloudsRequestDTO
    {
        /// <summary>
        /// authorization token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// userId
        /// </summary>
        public int UserId { get; set; }
    }
}
