using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApplication.Models;
using Moq;
using System.Web;
using WebApplication.ViewModels;
using WebApplication.Services;

namespace WebApplication.Controllers.Tests
{
    [TestClass()]
    public class AccountControllerTests
    {
        [TestMethod()]
        public void AccountControllerIndexTest()
        {
            // Arrange
            string username = "username";
            AccountController controller = new AccountController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["UserId"]).Returns(username);
            controller.ControllerContext = controllerContext.Object;

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            AccountViewModel vm = result.Model as AccountViewModel;
            Assert.AreEqual(username, vm.Account.Login);
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public async Task AccountControllerChangePasswordTest()
        {
            // Arrange
            string oldPassword = "1";
            string newPassword = "2";
            string newPasswordRepeated = "2";
            int id = 1;
            var auth = new Mock<IAuthenticationService>();
            auth.Setup(m => m.IsAuthenticated(It.IsAny<HttpSessionStateBase>())).Returns(true);
            auth.Setup(m => m.ChangePassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(true));
            AccountController controller = new AccountController(auth.Object);
            
            // Act
            ViewResult result = await controller.ChangePassword(oldPassword, newPassword, newPasswordRepeated, id) as ViewResult;

            // Assert
            ChangePasswordViewModel vm = result.Model as ChangePasswordViewModel;
            Assert.AreEqual("Success", vm.Message);
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public async Task AccountControllerChangePasswordPasswordDoesNotMatchTest()
        {
            // Arrange
            string oldPassword = "1";
            string newPassword = "2";
            string newPasswordRepeated = "3";
            int id = 1;
            var auth = new Mock<IAuthenticationService>();
            auth.Setup(m => m.IsAuthenticated(It.IsAny<HttpSessionStateBase>())).Returns(true);
            AccountController controller = new AccountController(auth.Object);

            // Act
            ViewResult result = await controller.ChangePassword(oldPassword, newPassword, newPasswordRepeated, id) as ViewResult;

            // Assert
            ChangePasswordViewModel vm = result.Model as ChangePasswordViewModel;
            Assert.AreEqual("new passwords do not match", vm.Message);
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public async Task AccountControllerChangePasswordOldPasswordDoesNotMatchTest()
        {
            // Arrange
            string oldPassword = "1";
            string newPassword = "2";
            string newPasswordRepeated = "2";
            int id = 1;
            var auth = new Mock<IAuthenticationService>();
            auth.Setup(m => m.IsAuthenticated(It.IsAny<HttpSessionStateBase>())).Returns(true);
            auth.Setup(m => m.ChangePassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(false));
            AccountController controller = new AccountController(auth.Object);

            // Act
            ViewResult result = await controller.ChangePassword(oldPassword, newPassword, newPasswordRepeated, id) as ViewResult;

            // Assert
            ChangePasswordViewModel vm = result.Model as ChangePasswordViewModel;
            Assert.AreEqual("Old password does not match", vm.Message);
            Assert.IsNotNull(result);
        }
    }
}