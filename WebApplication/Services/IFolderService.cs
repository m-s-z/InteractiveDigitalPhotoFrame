using System.Collections.Generic;
using System.Threading.Tasks;
using FlickrNet;
using WebApplication.Models;
using WebApplication.ViewModels;

namespace WebApplication.Services
{
    /// <summary>
    /// interface for folder service
    /// </summary>
    public interface IFolderService
    {
        #region methods
        /// <summary>
        /// method for adding folders
        /// </summary>
        /// <param name="folders">list of string (folder names)</param>
        /// <param name="cloudId"> cloud id</param>
        /// <param name="deviceId"> device id</param>
        /// <returns>
        /// true on succes
        /// false otherwise
        /// </returns>
        Task<bool> AddCloudFolders(List<string> folders, int cloudId, int deviceId);
        /// <summary>
        /// method for deleting folder
        /// </summary>
        /// <param name="folderId">folder id</param>
        /// <returns>
        /// true on success
        /// false if folder cannot be found
        /// </returns>
        Task<bool> deleteFolder(int folderId);
        /// <summary>
        /// returns all selected Dropbox folders. If a folder has been removed on the side of the provider we do not return it
        /// </summary>
        /// <param name="cloudId">cloud id</param>
        /// <param name="deviceId">device id</param>
        /// <returns>
        /// List of Folder model class
        /// </returns>
        Task<List<Folder>> GetDeviceDropboxFolders(int cloudId, int deviceId);
        /// <summary>
        /// returns all selected Flickr folders. If a folder has been removed on the side of the provider we do not return it
        /// </summary>
        /// <param name="cloudId">cloud id</param>
        /// <param name="deviceId">device id</param>
        /// <returns>
        /// List of Photoset class (FlickrNet class)
        /// </returns>
        Task<List<Photoset>> GetDeviceFlickrFolders(int cloudId, int deviceId);
        /// <summary>
        /// returns all Dropbox folders that have not been choosen yet. If token to cloud has been revoked we remove the cloud
        /// </summary>
        /// <param name="cloudId">cloud id</param>
        /// <param name="deviceId">device id</param>
        /// <returns>
        /// List of UniversalFolder object
        /// null if token has been removed
        /// </returns>
        Task<List<UniversalFolder>> GetDropboxFolders(int cloudId, int deviceId);
        /// <summary>
        /// returns all Flickr folders that have not been choosen yet. If token to cloud has been revoked we remove the cloud
        /// </summary>
        /// <param name="cloudId">cloud id</param>
        /// <param name="deviceId">device id</param>
        /// <returns>
        /// List of UniversalFolder object
        /// null if token has been removed
        /// </returns>
        Task<List<UniversalFolder>> GetFlickrFolders(int cloudId, int deviceId);
        /// <summary>
        /// method for retrieving all folders connected to device
        /// </summary>
        /// <param name="deviceId">device id</param>
        /// <returns>
        /// list of Folder model class
        /// empty list if device cannot be found
        /// </returns>
        Task<List<Folder>> getFolders(int deviceId);
        /// <summary>
        /// method for checking if any of the Dropbox Folders have been removed if they have we delete them from our db and return the actual list of folders
        /// </summary>
        /// <param name="cloudId">cloud id from which we retrieve the folders</param>
        /// <returns>
        /// List of Folder model class
        /// </returns>
        Task<List<Folder>> RefreshDropboxFolders(int cloudId);
        /// <summary>
        /// method for checking if any of the Flickr Folders have been removed if they have we delete them from our db and return the actual list of folders
        /// </summary>
        /// <param name="cloudId">cloud id from which we retrieve the folders</param>
        /// <returns>
        /// List of Folder model class
        /// </returns>
        Task<List<Folder>> RefreshFlickrFolders(int cloudId);
        #endregion methods
    }
}