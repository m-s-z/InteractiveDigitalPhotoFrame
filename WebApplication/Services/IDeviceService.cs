using System.Collections.Generic;
using System.Threading.Tasks;
using IDPFLibrary.DTO;
using WebApplication.Models;

namespace WebApplication.Services
{
    public interface IDeviceService
    {
        Task<CreateNewDeviceDTO> CreateDevice(string code);
        Task<bool> DeviceIsAuthenticated(int deviceId, string deviceToken);
        Task<string> GeneratePairCode(int deviceId, string deviceToken);
        Task<GetAllFlickrPhotosURLResponseDTO> GetAllPhotosUrl(List<int> accountIds, int deviceId);
        Task<GetDeviceAccountsDTO> GetDeviceAccounts(int deviceId, string deviceToken);
        Task<List<DeviceName>> GetDevices(string username);
        Task<string> PairDevice(string code, string deviceName, string userName);
        string TrueRandomString(int lenght);
        Task<bool> UnpairDevice(int deviceId, string userName);
    }
}