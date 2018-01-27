namespace AAA.Models
{
    public interface ICloudsConnectionsService
    {
        void ConnectWithFlickr();
        string GetSignature(string secretKey, string signatureString);
    }
}