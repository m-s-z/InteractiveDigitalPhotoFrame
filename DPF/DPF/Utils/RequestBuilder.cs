using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using DPF.Models;
using FlickrNet;

namespace DPF.Utils
{
    public class RequestBuilder
    {
        public static async Task<ObservableCollection<CloudFolder>> GetPhotosFromFolder(AccountCloud accountCloud)
        {
            ObservableCollection<CloudFolder> tempCollection = new ObservableCollection<CloudFolder>();
            FlickrManager flickerManager = new FlickrManager();
            Flickr flicker = await flickerManager.GetAuthInstance(accountCloud);
            List<Photoset> newFoldersList = flicker.PhotosetsGetList(accountCloud.FlickrUserId).ToList<Photoset>();
            //deleting all folders that have been removed on the side of Flickr
            foreach (var folder in newFoldersList)
            {
                tempCollection.Add(new CloudFolder(folder.Title, folder.NumberOfPhotos));
            }

            return tempCollection;
        }
    }
}