namespace AAA.Models
{
    /// <summary>
    /// Cloud providers connection service interface.
    /// </summary>
    public interface ICloudsConnectionsService
    {
        #region methods

        /// <summary>
        /// Counts encrypted signature for a given word and a key.
        /// </summary>
        /// <param name="secretKey">Key used in encryption.</param>
        /// <param name="signatureString">Word to encrypt.</param>
        /// <returns>Encrypted word.</returns>
        string GetSignature(string secretKey, string signatureString);

        #endregion
    }
}