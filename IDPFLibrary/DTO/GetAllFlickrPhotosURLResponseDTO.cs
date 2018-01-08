using System;
using System.Collections.Generic;

namespace IDPFLibrary.DTO

{
    /// <summary>
    /// Urls class.
    /// Contains metadata of a photo.
    /// </summary>
    public class Urls
    {
        #region properties

        /// <summary>
        /// Link to the photo.
        /// </summary>
        public String Link { get; set; }

        /// <summary>
        /// ID of the photo.
        /// </summary>
        public String PhotoId { get; set; }

        /// <summary>
        /// Cloud provider type.
        /// </summary>
        public CloudProviderType CloudProvider { get; set; }

        /// <summary>
        /// Upload date.
        /// </summary>
        public DateTime UploadDate { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Urls class constructor.
        /// </summary>
        public Urls()
        {

        }

        /// <summary>
        /// Urls class constructor.
        /// </summary>
        /// <param name="link">Link to the photo</param>
        /// <param name="photoId">ID of the photo.</param>
        /// <param name="myProperty">Cloud provider type.</param>
        /// <param name="uploadDate">Upload date.</param>
        public Urls(string link, string photoId, CloudProviderType myProperty, DateTime uploadDate)
        {
            Link = link;
            PhotoId = photoId;
            CloudProvider = myProperty;
            UploadDate = uploadDate;
        }

        #endregion       
    }

    /// <summary>
    /// GetAllFlickrPhotosURLResponseDTO class.
    /// </summary>
    public class GetAllFlickrPhotosURLResponseDTO
    {
        #region properties

        /// <summary>
        /// List of Urls (metadata of photos).
        /// </summary>
        public List<Urls> Urls { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// GetAllFlickrPhotosURLResponseDTO class constructor.
        /// </summary>
        public GetAllFlickrPhotosURLResponseDTO()
        {

        }

        /// <summary>
        /// GetAllFlickrPhotosURLResponseDTO class constructor.
        /// </summary>
        /// <param name="urls">List of Urls (metadata of photos).</param>
        public GetAllFlickrPhotosURLResponseDTO(List<Urls> urls)
        {
            Urls = urls;
        }

        #endregion
    }
}
