using System;
using System.Net;
using Android.Net;
using DPF.Droid.Services;
using DPF.Models;
using Xamarin.Forms;
using Application = Android.App.Application;

[assembly: Dependency(typeof(NetworkConnectionService))]

namespace DPF.Droid.Services
{
    public class NetworkConnectionService : INetworkConnectionService
    {
        public event ErrorOccurredDelegate ErrorOccured;

        public bool CheckIfNetworkConnected()
        {
            string CheckUrl = "http://google.com";

            try
            {
                HttpWebRequest iNetRequest = (HttpWebRequest)WebRequest.Create(CheckUrl);
                iNetRequest.Timeout = 5000;
                WebResponse iNetResponse = iNetRequest.GetResponse();
                iNetResponse.Close();

                return true;

            }
            catch (WebException ex)
            {
                return false;
            }
        }

    }
}