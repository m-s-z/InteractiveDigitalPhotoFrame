using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication.Data;

namespace WebApplication.Services
{
    public class AuthenticationService
    {
        private ApplicationContext db = new ApplicationContext();
        public AuthenticationService()
        {

        }

        public async Task<bool> Login(string username, string password)
        {
            var foundUser = await db.Accounts.FirstOrDefaultAsync( u => u.Login == username);
            if (foundUser != null)
            {
                if (foundUser.Password == password)
                {
                    return true;
                }
            }
            return false;
        }
        public bool IsAuthenticated(HttpSessionStateBase Session)
        {
            if (Session["UserId"] == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}