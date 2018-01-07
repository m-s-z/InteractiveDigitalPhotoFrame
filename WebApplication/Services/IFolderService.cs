using System.Collections.Generic;
using System.Threading.Tasks;
using FlickrNet;
using WebApplication.Models;
using WebApplication.ViewModels;

namespace WebApplication.Services
{
    public interface IFolderService
    {
        Task<bool> AddCloudFolders(List<string> folders, int cloudId, int deviceId);
        Task<bool> deleteFolder(int folderId);
        Task<List<Folder>> GetDeviceDropboxFolders(int cloudId, int deviceId);
        Task<List<Photoset>> GetDeviceFlickrFolders(int cloudId, int deviceId);
        Task<List<UniversalFolder>> GetDropboxFolders(int cloudId, int deviceId);
        Task<List<UniversalFolder>> GetFlickrFolders(int cloudId, int deviceId);
        Task<List<Folder>> getFolders(int deviceId);
        Task<List<Folder>> RefreshDropboxFolders(int cloudId);
        Task<List<Folder>> RefreshFlickrFolders(int cloudId);
    }
}