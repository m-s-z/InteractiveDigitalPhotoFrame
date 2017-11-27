using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Services
{
    public class AuthenticationService
    {
        public AuthenticationService()
        {

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