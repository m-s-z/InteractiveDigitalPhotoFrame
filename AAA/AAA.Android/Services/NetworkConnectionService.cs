using System.Net;
using AAA.Droid.Services;
using AAA.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(NetworkConnectionService))]

namespace AAA.Droid.Services
{
    /// <summary>
    /// Network connection service class.
    /// Implements INetworkConnectionService interface.
    /// </summary>
    public class NetworkConnectionService : INetworkConnectionService
    {
        #region methods

        /// <summary>
        /// Checks whether the device is connected to the Internet or not.
        /// </summary>
        /// <returns>True if the device is connected to the Internet, false otherwise.</returns>
        public bool CheckIfNetworkConnected()
        {
            string CheckUrl = "http://google.com";

            try
            {
                HttpWebRequest iNetRequest = (HttpWebRequest)WebRequest.Create(CheckUrl);
                iNetRequest.Timeout = 5000;
                WebResponse iNetResponse = iNetRequest.GetResponse();
                iNetResponse.Close();
                return true;
            }
            catch (WebException ex)
            {
                return false;
            }
        }

        #endregion
    }
}