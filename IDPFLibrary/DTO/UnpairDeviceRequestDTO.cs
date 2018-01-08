namespace IDPFLibrary.DTO
{
    /// <summary>
    /// UnpairDeviceRequestDTO class.
    /// </summary>
    public class UnpairDeviceRequestDTO
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
        /// Account ID.
        /// </summary>
        public int AccountId { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// UnpairDeviceRequestDTO class constructor.
        /// </summary>
        public UnpairDeviceRequestDTO()
        {

        }

        /// <summary>
        /// UnpairDeviceRequestDTO class constructor.
        /// </summary>
        /// <param name="deviceId">Device ID.</param>
        /// <param name="deviceToken">Device token.</param>
        /// <param name="accountId">Account ID.</param>
        public UnpairDeviceRequestDTO(int deviceId, string deviceToken, int accountId)
        {
            DeviceId = deviceId;
            DeviceToken = deviceToken;
            AccountId = accountId;
        }

        #endregion
    }
}
