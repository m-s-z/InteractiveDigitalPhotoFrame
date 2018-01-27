using System;
using System.Security.Cryptography;
using System.Text;
using AAA.Droid.Services;
using AAA.Models;
using AAA.Utils;
using AAA.Views;
using Android.Content;
using Xamarin.Forms;

[assembly: Dependency(typeof(CloudsConnectionsService))]

namespace AAA.Droid.Services
{
    public class CloudsConnectionsService : Android.App.Activity, ICloudsConnectionsService
    {
        public void ConnectWithFlickr()
        {
            


            //var webView = new WebView
            //{
            //    Source = "https://www.google.pl/"
            //};


            //var tempPage = new WebPage();
            //tempPage.Content = webView;
            //Application.Current.MainPage.Navigation.PushAsync(tempPage);



            // DependencyService.Get<ICloudsConnectionsService>().ConnectWithFlickr();
            var auth = new Xamarin.Auth.OAuth1Authenticator(FlickrAPI.ApiKey, FlickrAPI.SharedSecret, new Uri("https://www.flickr.com/services/oauth/request_token"), new Uri("https://www.flickr.com/services/oauth/authorize"), new Uri("https://www.flickr.com/services/oauth/access_token"), new Uri("https://idpf.azurewebsites.net/"));
            
            //HttpWebClientFrameworkType ta = HttpWebClientFrameworkType.HttpClient;
            //var auth = new Xamarin.Auth.OAuth2Authenticator(
            //    FlickrAPI.ApiKey, null, new Uri("https://www.flickr.com/services/oauth/authorize"), new Uri("https://www.youtube.com"));


            

            auth.Completed +=
                (s, ea) =>
                {
                    StringBuilder sb = new StringBuilder();

                    if (ea.Account != null && ea.Account.Properties != null)
                    {
                        sb.Append("Token = ").AppendLine($"{ea.Account.Properties["access_token"]}");
                    }
                    else
                    {
                        sb.Append("Not authenticated ").AppendLine($"Account.Properties does not exist");
                    }

                    

                    return;
                };

            auth.Error +=
                (s, ea) =>
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Error = ").AppendLine($"{ea.Message}");

                    
                    return;
                };
            global::Android.Content.Intent ui_object = auth.GetUI(this);

            Forms.Context.StartActivity(ui_object);


        }

        public string GetSignature(string secretKey, string signatureString)
        {
            var enc = Encoding.ASCII;
            HMACSHA1 hmac = new HMACSHA1(enc.GetBytes(secretKey));
            hmac.Initialize();

            byte[] buffer = enc.GetBytes(signatureString);
            //return BitConverter.ToString(hmac.ComputeHash(buffer)).Replace("-", "").ToLower();
            return Convert.ToBase64String(hmac.ComputeHash(buffer)).Replace("-", "");
        }

    }
}