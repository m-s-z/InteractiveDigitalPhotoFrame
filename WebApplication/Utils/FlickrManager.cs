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
    class FlickrManager
    {
        private ApplicationContext db = new ApplicationContext();
        public const string ApiKey = "6d8b52db06178de4dacd9c8423851a8b";
        public const string SharedSecret = "a1a67205cb7c23ae";
        public FlickrManager()
        {

        }
        public static Flickr GetInstance()
        {
            return new Flickr(ApiKey, SharedSecret);
        }

        public async Task<Flickr> GetAuthInstance(int cloudId)
        {
            var f = new Flickr(ApiKey, SharedSecret);
            Cloud cloud = await db.Clouds.FindAsync(cloudId);
            //get token and secret token from db and assign to f.OAuthAccessToken and f.OAuthAccessTokenSecret
            f.OAuthAccessToken = cloud.Token;
            f.OAuthAccessTokenSecret = cloud.TokenSecret;
            return f;
        }

    }
}