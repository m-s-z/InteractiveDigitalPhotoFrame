﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication.Data;
using WebApplication.Models;

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
            var foundUser = await db.Accounts.FirstOrDefaultAsync( u => u.Login.Equals(username.ToLower()));
            if (foundUser != null)
            {
                if (Account.PasswordEquals(password, foundUser.Password) && foundUser.Login == username.ToLower())
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

        public async Task<string> RegisterAccount(string login, string password)
        {
            string result = "success";
            string hashedPassword = Account.HashPassword(password);
            string lowerCaseLogin = login.ToLower();
            List<Device> devices = new List<Device>();
            Account account = new Account(login, hashedPassword, devices);
            try
            {
                db.Accounts.Add(account);
                await db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                result = "An account with that username already exists please try another";
            }
            return result;
        }
    }
}