using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IDPFLibrary.DTO.AAA.Folder.Request;

namespace AAA.Utils
{
    /// <summary>
    /// RestRequester class.
    /// Provides method to build and send a request.
    /// </summary>
    public class RestRequester
    {
        #region fields

        /// <summary>
        /// Core part of web adress to request.
        /// </summary>
        private const string IDPF_CORE_WEB_ADRESS = "https://idpf.azurewebsites.net{0}";

        #endregion

        #region methods

        /// <summary>
        /// Method which creates a request, sends it and receives response.
        /// </summary>
        /// <param name="urlEndpoint">Endpoint of an url to request.</param>
        /// <param name="httpMethod">Type of HTTP method.</param>
        /// <param name="json">String containing data to send.</param>
        /// <returns>String containing received data.</returns>
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

        #endregion
    }
}