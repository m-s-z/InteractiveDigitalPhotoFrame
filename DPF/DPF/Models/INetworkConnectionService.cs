namespace DPF.Models
{
    /// <summary>
    /// Network connection service interface.
    /// </summary>
    public interface INetworkConnectionService
    {
        #region methods

        /// <summary>
        /// Checks whether the device is connected to the Internet or not.
        /// </summary>
        /// <returns>True if the device is connected to the Internet, false otherwise.</returns>
        bool CheckIfNetworkConnected();

        #endregion
    }
}