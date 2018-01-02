using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DPF.Droid.Services;
using DPF.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(LocalStorageService))]

namespace DPF.Droid.Services
{
    public class LocalStorageService : ILocalStorageService
    {
        private const string PATH_TO_PICTURES_TEMPLATE = "{0}/pictures/{1}";
        private const string PATH_TO_DATA_TEMPLATE = "{0}/data/{1}";

        public event ErrorOccurredDelegate ErrorOccured;

        public void CreateImagesFolder()
        {
            Directory.CreateDirectory(string.Format(PATH_TO_PICTURES_TEMPLATE, Environment.GetFolderPath(Environment.SpecialFolder.Personal), ""));
            Directory.CreateDirectory(string.Format(PATH_TO_DATA_TEMPLATE, Environment.GetFolderPath(Environment.SpecialFolder.Personal), ""));
        }

        public List<string> GetImagesList()
        {
            var st = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Personal)).ToList();
            return st;
        }

        public void SaveImage()
        {
            System.Diagnostics.Debug.WriteLine("----------------------------------------------------------");
            System.Diagnostics.Debug.WriteLine(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
            System.Diagnostics.Debug.WriteLine(Directory.GetCurrentDirectory());
            
            var st = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
            foreach (string s in st)
            {
                System.Diagnostics.Debug.WriteLine(s);
            }
            System.Diagnostics.Debug.WriteLine("----------------------------------------------------------");
            st = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
            foreach (string s in st)
            {
                System.Diagnostics.Debug.WriteLine(s);
            }
            System.Diagnostics.Debug.WriteLine("----------------------------------------------------------");

            //var webClient = new WebClient();
            //webClient.DownloadDataCompleted += (s, e) =>
            //{
            //    var bytes = e.Result; // get the downloaded data
            //    string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            //    string localFilename = "downloaded.png";
            //    string localPath = Path.Combine(documentsPath, localFilename);
            //    File.WriteAllBytes(localPath, bytes); // writes to local storage
            //};

            //var url = new Uri("https://3.bp.blogspot.com/-vLUGceSS8r4/V8knCEU569I/AAAAAAAAAcg/pK2uc4kBiPUe2t9KwsozXYhYVnmFjq4xQCLcB/s1600/Screenshot_2016-09-02-13-01-53.png");
            //webClient.DownloadDataAsync(url);

            //webClient = new WebClient();
            //webClient.DownloadDataCompleted += (s, e) =>
            //{
            //    var bytes = e.Result; // get the downloaded data
            //    string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            //    string localFilename = "downloaded2.png";
            //    string localPath = Path.Combine(documentsPath, localFilename);
            //    File.WriteAllBytes(localPath, bytes); // writes to local storage
            //};

            //url = new Uri("https://qph.ec.quoracdn.net/main-qimg-aa72bafd48ebfcb5b6e14006ab9c48be");
            //webClient.DownloadDataAsync(url);
        }

        public string GetConnectedAccounts()
        {
            try
            {
                return File.ReadAllText(string.Format(PATH_TO_DATA_TEMPLATE,
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ConnectedAccounts.txt"));
            }
            catch (Exception exception)
            {
                return null;
            }

        }

        public string GetDeviceToken()
        {
            try
            {
                return File.ReadAllText(string.Format(PATH_TO_DATA_TEMPLATE,
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal), "DeviceToken.txt"));
            }
            catch (Exception exception)
            {
                return null;
            }
            
        }

        public void SaveConnectedAccounts(string json)
        {
            File.WriteAllText(string.Format(PATH_TO_DATA_TEMPLATE, Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ConnectedAccounts.txt"), json);
        }

        public void SaveDeviceToken(string json)
        {
            try
            {
                Directory.CreateDirectory(string.Format(PATH_TO_DATA_TEMPLATE, Environment.GetFolderPath(Environment.SpecialFolder.Personal), ""));
                File.WriteAllText(string.Format(PATH_TO_DATA_TEMPLATE, Environment.GetFolderPath(Environment.SpecialFolder.Personal), "DeviceToken.txt"), json);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
    }
}