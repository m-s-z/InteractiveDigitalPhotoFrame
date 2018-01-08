namespace IDPFLibrary.DTO
{
    /// <summary>
    /// CreateNewDeviceDTO class.
    /// </summary>
    public class CreateNewDeviceDTO
    {
        #region properties

        /// <summary>
        /// ID of the device.
        /// </summary>
        public int DeviceId { get; set; }

        /// <summary>
        /// Token of the device.
        /// </summary>
        public string DeviceToken { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// CreateNewDeviceDTO class constructor.
        /// </summary>
        public CreateNewDeviceDTO()
        {

        }

        /// <summary>
        /// CreateNewDeviceDTO class constructor.
        /// </summary>
        /// <param name="deviceId">Device ID to set.</param>
        /// <param name="deviceToken">Device Token to set.</param>
        public CreateNewDeviceDTO(int deviceId, string deviceToken)
        {
            DeviceId = deviceId;
            DeviceToken = deviceToken;
        }

        #endregion
    }
}
