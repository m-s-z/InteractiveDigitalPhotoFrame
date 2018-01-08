using System.Collections.Generic;
using System.Threading.Tasks;
using IDPFLibrary.DTO;
using WebApplication.Models;

namespace WebApplication.Services
{
    /// <summary>
    /// interface for device service
    /// </summary>
    public interface IDeviceService
    {
        #region methods
        /// <summary>
        /// method for creating a device
        /// </summary>
        /// <param name="code">secret application code</param>
        /// <returns>
        /// CreateNewDeviceDTO class
        /// </returns>
        Task<CreateNewDeviceDTO> CreateDevice(string code);
        /// <summary>
        /// method for chekcing if device token matches the database. Used for device authorization
        /// </summary>
        /// <param name="deviceId">device id</param>
        /// <param name="deviceToken">device token</param>
        /// <returns>
        /// true if token matches database token
        /// false otherwise
        /// </returns>
        Task<bool> DeviceIsAuthenticated(int deviceId, string deviceToken);
        /// <summary>
        /// method for generating a pair code of CODESIZE (7)
        /// </summary>
        /// <param name="deviceId">device id</param>
        /// <param name="deviceToken">device token for authorization</param>
        /// <returns>
        /// string paircode on success
        /// empty string token does not match the database (authorization failure)
        /// </returns>
        Task<string> GeneratePairCode(int deviceId, string deviceToken);
        /// <summary>
        /// method for getting all photo urls with thier respective metadata
        /// </summary>
        /// <param name="accountIds">list of accounts from which to extract photos from</param>
        /// <param name="deviceId">target device</param>
        /// <returns>
        /// GetAllFlickrPhotosURLResponseDTO class
        /// </returns>
        Task<GetAllFlickrPhotosURLResponseDTO> GetAllPhotosUrl(List<int> accountIds, int deviceId);
        /// <summary>
        /// method for getting all accounts connected to given device
        /// </summary>
        /// <param name="deviceId">device id</param>
        /// <param name="deviceToken"> device token</param>
        /// <returns>
        /// GetDeviceAccountsDTO class
        /// </returns>
        Task<GetDeviceAccountsDTO> GetDeviceAccounts(int deviceId, string deviceToken);
        /// <summary>
        /// this method will return all devices connected to the user with names that are connected to this account
        /// </summary>
        /// <param name="username">username of account from which to retrieve devices</param>
        /// <returns>
        /// List of DeviceName model class
        /// returns an empty list if account cannot be found
        /// </returns>
        Task<List<DeviceName>> GetDevices(string username);
        /// <summary>
        /// method for conecting a device to an account
        /// </summary>
        /// <param name="code">pair code from device</param>
        /// <param name="deviceName">custom device name specific to account</param>
        /// <param name="userName">account username</param>
        /// <returns>
        /// string message based on result
        /// </returns>
        Task<string> PairDevice(string code, string deviceName, string userName);
        /// <summary>
        /// method for creating a true random string
        /// </summary>
        /// <param name="lenght">string length</param>
        /// <returns>
        /// randomly generated string
        /// </returns>
        string TrueRandomString(int lenght);
        /// <summary>
        /// method for unpairing a device from an account
        /// </summary>
        /// <param name="deviceId">dedvice id</param>
        /// <param name="userName"> account username</param>
        /// <returns>
        /// true on success
        /// false if either device or user cannot be found
        /// </returns>
        Task<bool> UnpairDevice(int deviceId, string userName);
        #endregion methods
    }
}