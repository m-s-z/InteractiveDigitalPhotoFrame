using Xamarin.Forms;

namespace DPF.Models
{
    /// <summary>
    /// NetworkConnectionModel class.
    /// Provides methods to check network connection. 
    /// </summary>
    public class NetworkConnectionModel
    {
        #region fields

        /// <summary>
        /// Instance of the NetworkConnectionService class.
        /// </summary>
        private INetworkConnectionService _service;

        #endregion

        #region methods

        /// <summary>
        /// NetworkConnectionModel class constructor.
        /// </summary>
        public NetworkConnectionModel()
        {
            _service = DependencyService.Get<INetworkConnectionService>();
        }

        /// <summary>
        /// Calls service to check whether the device is connected to the Internet or not.
        /// </summary>
        /// <returns>True if the device is connected to the Internet, false otherwise.</returns>
        public bool CheckIfNetworkConnected()
        {
            return _service.CheckIfNetworkConnected();
        }

        #endregion
    }
}