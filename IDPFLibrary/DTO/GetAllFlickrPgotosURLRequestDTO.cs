using System.Collections.Generic;

namespace IDPFLibrary.DTO
{
    public class GetAllFlickrPgotosURLRequestDTO
    {
        public GetAllFlickrPgotosURLRequestDTO()
        {

        }
        public GetAllFlickrPgotosURLRequestDTO(int deviceId, string deviceToken, List<int> accountIds)
        {
            DeviceId = deviceId;
            DeviceToken = deviceToken;
            AccountIds = accountIds;
        }

        public int DeviceId { get; set; }
        public string DeviceToken { get; set; }

        public List<int> AccountIds { get; set; }
    }
}