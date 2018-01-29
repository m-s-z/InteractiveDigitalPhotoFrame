using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IDPFLibrary.DTO.AAA.Folder.Request;

namespace AAA.Utils
{
    public class RestRequester
    {
        private const string IDPF_CORE_WEB_ADRESS = "https://idpf.azurewebsites.net{0}";
        public static async Task<string> SendRequest(string urlEndpoint, HttpMethod httpMethod, string json)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = string.Format(IDPF_CORE_WEB_ADRESS, urlEndpoint);

                    var request = new HttpRequestMessage()
                    {
                        RequestUri = new Uri(url),
                        Method = httpMethod
                    };
                    if (httpMethod != HttpMethod.Get)
                    {
                        request.Content = new StringContent(json,
                            Encoding.UTF8,
                            "application/json");
                    }

                    var response = await client.SendAsync(request);
                    var contents = await response.Content.ReadAsStringAsync();
                    return contents;
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}