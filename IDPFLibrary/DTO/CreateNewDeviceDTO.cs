using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDPFLibrary.DTO
{
    public class CreateNewDeviceDTO
    {
        public CreateNewDeviceDTO()
        {

        }
        public CreateNewDeviceDTO(int deviceId, string deviceToken)
        {
            DeviceId = deviceId;
            DeviceToken = deviceToken;
        }

        public int DeviceId { get; set; }
        public string DeviceToken { get; set; }
    }
}
