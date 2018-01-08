using FlickrNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Utils
{
    /// <summary>
    /// class to help create flickr class instances and store tokens
    /// </summary>
    class FlickrManager
    {
        #region properties
        /// <summary>
        /// instance database context
        /// </summary>
        private ApplicationContext db = new ApplicationContext();
        /// <summary>
        /// Flickr key
        /// </summary>
        public const string ApiKey = "6d8b52db06178de4dacd9c8423851a8b";
        /// <summary>
        /// flickr key secret
        /// </summary>
        public const string SharedSecret = "a1a67205cb7c23ae";
        #endregion properties
        /// <summary>
        /// constructor for flickr manager
        /// </summary>
        public FlickrManager()
        {

        }
        #region methods
        /// <summary>
        /// creates Flickr instance with initiated keys
        /// </summary>
        /// <returns>
        /// Flickr class
        /// </returns>
        public static Flickr GetInstance()
        {
            return new Flickr(ApiKey, SharedSecret);
        }

        /// <summary>
        /// creates authenticated instance of Flickr
        /// </summary>
        /// <param name="cloudId">cloudid from which we will take authentication toke from</param>
        /// <returns>
        /// Flickr object
        /// </returns>
        public async Task<Flickr> GetAuthInstance(int cloudId)
        {
            var f = new Flickr(ApiKey, SharedSecret);
            Cloud cloud = await db.Clouds.FindAsync(cloudId);
            //get token and secret token from db and assign to f.OAuthAccessToken and f.OAuthAccessTokenSecret
            f.OAuthAccessToken = cloud.Token;
            f.OAuthAccessTokenSecret = cloud.TokenSecret;
            return f;
        }
        #endregion methods

    }
}