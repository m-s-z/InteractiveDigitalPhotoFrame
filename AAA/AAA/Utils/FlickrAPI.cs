﻿using System;
using System.Text;

namespace AAA.Utils
{
    public class FlickrAPI
    {
        /// <summary>
        /// Flickr key
        /// </summary>
        public const string ApiKey = "6d8b52db06178de4dacd9c8423851a8b";
        /// <summary>
        /// flickr key secret.
        /// </summary>
        public const string SharedSecret = "a1a67205cb7c23ae";

        public const string RequestTokenURL =
                "http://www.flickr.com/services/oauth/request_token?oauth_callback=https%3A%2F%2Fwww.idpf.azurewebsites.net&oauth_consumer_key=6d8b52db06178de4dacd9c8423851a8b&oauth_nonce=C2F26CD5C075BA9050AD8EE90644CF29&oauth_timestamp={0}&oauth_signature_method=HMAC-SHA1&oauth_version=1.0&oauth_signature={1}"
            ;

        public const string AuthorizeTokenURL = "https://www.flickr.com/services/oauth/authorize?oauth_token={0}&perms=read";

        public const string AccessTokenURL =
                "https://www.flickr.com/services/oauth/access_token?oauth_consumer_key=6d8b52db06178de4dacd9c8423851a8b&oauth_nonce=C2F26CD5C075BA9050AD8EE90644CF29&oauth_signature={0}&oauth_signature_method=HMAC-SHA1&oauth_timestamp={1}&oauth_token={2}&oauth_verifier={3}&oauth_version=1.0"
            ;

        public static string GetRequestToSignature()
        {
            return
                "GET&http%3A%2F%2Fwww.flickr.com%2Fservices%2Foauth%2Frequest_token&oauth_callback%3Dhttps%253A%252F%252Fwww.idpf.azurewebsites.net%26oauth_consumer_key%3D6d8b52db06178de4dacd9c8423851a8b%26oauth_nonce%3DC2F26CD5C075BA9050AD8EE90644CF29%26oauth_signature_method%3DHMAC-SHA1%26oauth_timestamp%3D{0}%26oauth_version%3D1.0";
        }

        public static string GetAccessToSignature()
        {
            return
                "GET&https%3A%2F%2Fwww.flickr.com%2Fservices%2Foauth%2Faccess_token&oauth_consumer_key%3D6d8b52db06178de4dacd9c8423851a8b%26oauth_nonce%3DC2F26CD5C075BA9050AD8EE90644CF29%26oauth_signature_method%3DHMAC-SHA1%26oauth_timestamp%3D{0}%26oauth_token%3D{1}%26oauth_verifier%3D{2}%26oauth_version%3D1.0";
        }

        public static Tuple<string, string> GetRequestTokenFromText(string text)
        {
            if (text.Contains("oauth_token") && text.Contains("oauth_callback_confirmed") && text.Contains("&oauth_token_secret"))
            {
                var at = text.Replace("oauth_callback_confirmed=true&oauth_token=", "");
                var oauth_token = at.Substring(0, at.IndexOf('&'));
                var oauth_token_secret = at.Substring(at.IndexOf('&') + 20, at.Length - at.IndexOf('&') - 20);
                return Tuple.Create(oauth_token, oauth_token_secret);
            }
            return null;
        }

        public static Tuple<string, string> GetAuthorizationTokenFromUrl(string url)
        {
            if (url.Contains("oauth_token") && url.Contains("&oauth_verifier"))
            {
                var at = url.Replace("https://www.idpf.azurewebsites.net/?oauth_token=", "");
                var access = at.Substring(0, at.IndexOf('&'));
                var verifier = at.Substring(at.IndexOf('&') + 16, at.Length - at.IndexOf('&') - 16);
                return Tuple.Create(access, verifier);
            }
            return null;
        }

        public static string[] GetAccessTokenFromUrl(string url)
        {
            if (url.Contains("oauth_token") && url.Contains("&oauth_token_secret"))
            {
                var s = url;
                var i = s.Split('&');
                i[1] = i[1].Replace("oauth_token=", "");
                i[2] = i[2].Replace("oauth_token_secret=", "");
                i[3] = i[3].Replace("user_nsid=", "");
                return i;
            }
            return null;
        }
    }
}