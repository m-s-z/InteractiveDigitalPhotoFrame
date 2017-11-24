﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            Account account = new Account("Tom123");
            AccountViewModel model = new AccountViewModel(account);
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> ChangeLogin(Account account, int id)
        {

            return View(account);
        }
    }
}