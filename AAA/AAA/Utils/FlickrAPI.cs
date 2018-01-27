using System;
using System.Security.Cryptography;
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
        /// flickr key secret  72157663052282657-d8e008a8d9b3e92a
        /// </summary>
        public const string SharedSecret = "a1a67205cb7c23ae";

        public const string RequestTokenURL =
                "http://www.flickr.com/services/oauth/request_token?oauth_callback=https%3A%2F%2Fwww.idpf.azurewebsites.net&oauth_consumer_key=6d8b52db06178de4dacd9c8423851a8b&oauth_nonce=C2F26CD5C075BA9050AD8EE90644CF29&oauth_timestamp=1517062927&oauth_signature_method=HMAC-SHA1&oauth_version=1.0&oauth_signature=SZWW4MrswnTrnudTaU1pVr4RwdA%3D"
            ;

        public const string AuthorizeTokenURL = "https://www.flickr.com/services/oauth/authorize?oauth_token=";

        public string GetSignature()
        {
            var enc = Encoding.ASCII;
            HMACSHA1 hmac = new HMACSHA1(enc.GetBytes(secretKey));
            hmac.Initialize();

            byte[] buffer = enc.GetBytes(signatureString);
            return BitConverter.ToString(hmac.ComputeHash(buffer)).Replace("-", "").ToLower();
        }
    }
}