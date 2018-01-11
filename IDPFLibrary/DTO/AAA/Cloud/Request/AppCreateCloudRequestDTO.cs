using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDPFLibrary.DTO.AAA.Cloud.Request
{
    /// <summary>
    /// request class for AppCreateCloud controller method
    /// </summary>
    public class AppCreateCloudRequestDTO
    {
        #region properties
        /// <summary>
        /// account id
        /// </summary>
        public int AccountId { get; set; }
        /// <summary>
        /// custom cloud name
        /// </summary>
        public string CloudName { get; set; }
        /// <summary>
        /// provider type we support Dropbox and Flickr
        /// </summary>
        public CloudProviderType Provider { get; set; }
        /// <summary>
        /// provider token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// provider token secret
        /// </summary>
        public string TokenSecret { get; set; }
        /// <summary>
        /// provider user id
        /// </summary>
        public string UserId { get; set; }
        #endregion properties
        /// <summary>
        /// constructor for AppCreateCloudRequestDTO class
        /// </summary>
        public AppCreateCloudRequestDTO()
        {

        }
    }
}
