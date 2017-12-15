﻿using System;
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
            var foundUser = await db.Accounts.FirstOrDefaultAsync( u => u.Login.Equals(username));
            if (foundUser != null)
            {
                if (foundUser.Password == password && foundUser.Login == username)
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

        public string getLoggedInUsername(HttpSessionStateBase session)
        {
            if (session["UserId"] != null)
            {
                return session["UserId"] as string;
            }else
            {
                return null;
            }
        }
    }
}