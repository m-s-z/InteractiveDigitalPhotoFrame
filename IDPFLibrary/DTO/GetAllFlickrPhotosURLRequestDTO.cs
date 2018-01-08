using System.Collections.Generic;

namespace IDPFLibrary.DTO
{
    /// <summary>
    /// GetAllFlickrPhotosURLRequestDTO class.
    /// </summary>
    public class GetAllFlickrPhotosURLRequestDTO
    {
        #region properties

        /// <summary>
        /// Device ID.
        /// </summary>
        public int DeviceId { get; set; }

        /// <summary>
        /// Device token.
        /// </summary>
        public string DeviceToken { get; set; }

        /// <summary>
        /// List of all connected accounts.
        /// </summary>
        public List<int> AccountIds { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// GetAllFlickrPhotosURLRequestDTO class constructor.
        /// </summary>
        public GetAllFlickrPhotosURLRequestDTO()
        {

        }

        /// <summary>
        /// GetAllFlickrPhotosURLRequestDTO class constructor.
        /// </summary>
        /// <param name="deviceId">Device ID to set.</param>
        /// <param name="deviceToken">Device token to set.</param>
        /// <param name="accountIds">List of connected accounts to set.</param>
        public GetAllFlickrPhotosURLRequestDTO(int deviceId, string deviceToken, List<int> accountIds)
        {
            DeviceId = deviceId;
            DeviceToken = deviceToken;
            AccountIds = accountIds;
        }

        #endregion
    }
}