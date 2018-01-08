using System.Collections.Generic;
using System.Threading.Tasks;
using FlickrNet;
using WebApplication.Models;

namespace WebApplication.Services
{
    public interface ICloudService
    {
        Task<bool> ChangePassword(string oldPassword, string newPassword, int cloudId);
        Task<bool> CreateDropBoxAccount(string token, string accountName, string username, string userId);
        Task<bool> CreateFlickerAccount(OAuthAccessToken token, string accountName, string username);
        Task<Cloud> GetCloud(int cloudId);
        Task<List<Cloud>> GetClouds(string username);
        Task<bool> removeCloud(int cloudId);
    }
}