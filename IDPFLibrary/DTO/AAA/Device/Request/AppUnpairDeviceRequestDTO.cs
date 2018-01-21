namespace IDPFLibrary.DTO.AAA.Device.Request
{
    /// <summary>
    /// Request DTO for AppUnpairDevice controller method
    /// </summary>
    public class AppUnpairDeviceRequestDTO
    {
        #region properties
        /// <summary>
        /// Account ID.
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// Device ID.
        /// </summary>
        public int DeviceId { get; set; }
        /// <summary>
        /// authentication token
        /// </summary>
        public string Token { get; set; }

        #endregion properties

        /// <summary>
        /// Constructor for AppUnpairDeviceRequestDTO class.
        /// </summary>
        public AppUnpairDeviceRequestDTO()
        {

        }
    }
}