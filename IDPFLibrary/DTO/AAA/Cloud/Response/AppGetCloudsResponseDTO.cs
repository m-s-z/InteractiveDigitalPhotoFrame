using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDPFLibrary.DTO.AAA.Cloud.Response
{
    /// <summary>
    /// helper class for mirroring cloud model class
    /// </summary>
    public class RCloud
    {
        #region properties
        /// <summary>
        /// custom cloud name
        /// </summary>
        public string CloudName { get; set; }
        /// <summary>
        /// provider
        /// </summary>
        public CloudProviderType provider { get; set; }
        /// <summary>
        /// token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// token secret
        /// </summary>
        public string TokenSecret { get; set; }
        /// <summary>
        /// user id
        /// </summary>
        public string  UserId { get; set; }
        #endregion properties
        /// <summary>
        /// constructor for Rcloud class
        /// </summary>
        public RCloud()
        {

        }
    }
    /// <summary>
    /// response class for AppGetClouds controller method
    /// </summary>
    public class AppGetCloudsResponseDTO
    {
        #region properties
        /// <summary>
        /// returned clouds
        /// </summary>
        public List<RCloud> clouds { get; set; }
        #endregion properties
        /// <summary>
        /// constructor for AppGetCloudsResponseDTO class
        /// </summary>
        public AppGetCloudsResponseDTO()
        {

        }
    }
}
