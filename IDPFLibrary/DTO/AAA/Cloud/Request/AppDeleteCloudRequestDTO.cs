using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDPFLibrary.DTO.AAA.Cloud.Request
{
    /// <summary>
    /// request class fro AppDeleteCloud controller method
    /// </summary>
    public class AppDeleteCloudRequestDTO
    {
        /// <summary>
        /// cloud id
        /// </summary>
        public int CloudId { get; set; }
        /// <summary>
        /// provider user id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// authentication token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// constructor for AppDeleteCloudRequestDTO
        /// </summary>
        public AppDeleteCloudRequestDTO()
        {

        }
    }
}
