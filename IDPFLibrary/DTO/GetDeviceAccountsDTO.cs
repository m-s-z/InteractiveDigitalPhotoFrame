using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDPFLibrary.DTO
{
    public class SCloud
    {
        public SCloud(int cloudId, string token, string tokenSecret)
        {
            CloudId = cloudId;
            Token = token;
            TokenSecret = tokenSecret;
        }

        public int CloudId { get; set; }
        public String Token { get; set; }
        public String TokenSecret { get; set; }
    }
    public class SAccount
    {
        public SAccount(string name, List<SCloud> clouds, int accountId)
        {
            Name = name;
            this.clouds = clouds;
            AccountId = accountId;
        }

        public String Name { get; set; }
        public List<SCloud> clouds { get; set; }
        public int AccountId { get; set; }
    }
    public class GetDeviceAccountsDTO
    {
        public GetDeviceAccountsDTO(List<SAccount> accounts)
        {
            Accounts = accounts;
        }

        public List<SAccount> Accounts { get; set; }
    }
}
