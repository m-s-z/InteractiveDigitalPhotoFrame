using System.Threading.Tasks;
using DPF.Models;
using FlickrNet;

namespace DPF.Utils
{
    public class FlickrManager
    {
        public const string ApiKey = "3a68f22971d8d66b521b362c312c175c";
        public const string SharedSecret = "b2acf0fb7910be24";

        public static Flickr GetInstance()
        {
            return new Flickr(ApiKey, SharedSecret);
        }

        public async Task<Flickr> GetAuthInstance(AccountCloud accountCloud)
        {
            var f = new Flickr(ApiKey, SharedSecret);
            f.OAuthAccessToken = accountCloud.Token;
            f.OAuthAccessTokenSecret = accountCloud.TokenSecret;
            return f;
        }
    }
}