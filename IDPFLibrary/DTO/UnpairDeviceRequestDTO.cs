using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDPFLibrary.DTO
{
    public class UnpairDeviceRequestDTO
    {
        public UnpairDeviceRequestDTO()
        {

        }
        public UnpairDeviceRequestDTO(int deviceId, string deviceToken, int accountId)
        {
            DeviceId = deviceId;
            DeviceToken = deviceToken;
            AccountId = accountId;
        }

        public int DeviceId { get; set; }
        public string DeviceToken { get; set; }

        public int AccountId { get; set; }

    }
}
