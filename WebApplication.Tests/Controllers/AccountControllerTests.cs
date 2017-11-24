using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers.Tests
{
    [TestClass()]
    public class AccountControllerTests
    {
        [TestMethod()]
        public void AccountControllerIndexTest()
        {
            // Arrange
            AccountController controller = new AccountController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public async Task AccountControllerChangeLoginTest()
        {
            // Arrange
            AccountController controller = new AccountController();
            Account account = new Account();
            int id = 1;
            // Act
            ViewResult result = await controller.ChangeLogin(account,id) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}