using System;
using System.Security.Cryptography;
using System.Text;
using AAA.Droid.Services;
using Xamarin.Forms;
using AAA.Models;

[assembly: Dependency(typeof(CloudsConnectionsService))]

namespace AAA.Droid.Services
{
    /// <summary>
    /// Cloud providers connection service class.
    /// Implements ICloudsConnectionsService interface.
    /// </summary>
    public class CloudsConnectionsService : ICloudsConnectionsService
    {
        #region methods

        /// <summary>
        /// Counts encrypted signature for a given word and a key.
        /// </summary>
        /// <param name="secretKey">Key used in encryption.</param>
        /// <param name="signatureString">Word to encrypt.</param>
        /// <returns>Encrypted word.</returns>
        public string GetSignature(string secretKey, string signatureString)
        {
            var enc = Encoding.ASCII;
            HMACSHA1 hmac = new HMACSHA1(enc.GetBytes(secretKey));
            hmac.Initialize();
            byte[] buffer = enc.GetBytes(signatureString);
            return Convert.ToBase64String(hmac.ComputeHash(buffer)).Replace("-", "");
        }

        #endregion
    }
}