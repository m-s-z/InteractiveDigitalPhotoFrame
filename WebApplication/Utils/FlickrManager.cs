using FlickrNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Utils
{
    class FlickrManager
    {
        public const string ApiKey = "6d8b52db06178de4dacd9c8423851a8b";
        public const string SharedSecret = "a1a67205cb7c23ae";

        public static Flickr GetInstance()
        {
            return new Flickr(ApiKey, SharedSecret);
        }

        public static Flickr GetAuthInstance(int cloudId)
        {
            var f = new Flickr(ApiKey, SharedSecret);
            //get token and secret token from db and assign to f.OAuthAccessToken and f.OAuthAccessTokenSecret
            
            return f;
        }

    }
}