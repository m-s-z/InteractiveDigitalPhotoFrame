using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApplication.Services;
using Moq;
using System.Web;
using WebApplication.ViewModels;

namespace WebApplication.Controllers.Tests
{
    [TestClass()]
    public class LoginControllerTests
    {
        [TestMethod()]
        public void LoginControllerIndexTest()
        {
            // Arrange
            LoginController controller = new LoginController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public async Task LoginControllerLoginTest()
        {
            // Arrange
            var auth = new Mock<IAuthenticationService>();
            auth.Setup(m => m.IsAuthenticated(It.IsAny<HttpSessionStateBase>())).Returns(true);
            LoginController controller = new LoginController(auth.Object);

            // Act
            RedirectToRouteResult result = await controller.Login("", "") as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod()]
        public void LoginControllerLogOutTest()
        {
            // Arrange
            var auth = new Mock<IAuthenticationService>();
            auth.Setup(m => m.IsAuthenticated(It.IsAny<HttpSessionStateBase>())).Returns(true);
            LoginController controller = new LoginController(auth.Object);
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["UserId"]).Returns("test");
            controller.ControllerContext = controllerContext.Object;

            // Act
            ViewResult result = controller.LogOut() as ViewResult;

            // Assert
            auth.Verify(m => m.IsAuthenticated(It.IsAny<HttpSessionStateBase>()));
        }

        [TestMethod()]
        public async Task LoginControllerRegisterTestPasswordsMatch()
        {
            // Arrange
            string password1 = "1";
            string password2 = "1";
            string account = "account";
            string registrationResult = "Success!";
            var auth = new Mock<IAuthenticationService>();
            auth.Setup(m => m.RegisterAccount(account,password1)).Returns(Task.FromResult(registrationResult));
            LoginController controller = new LoginController(auth.Object);

            // Act
            ViewResult result = await controller.RegisterConfirm(account, password1, password2) as ViewResult;

            // Assert
            RegisterConfirmViewModel vm = result.Model as RegisterConfirmViewModel;
            Assert.AreEqual(registrationResult, vm.Message);
            auth.Verify(m => m.RegisterAccount(account,password1));
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public async Task LoginControllerRegisterTestPasswordsDoNotMatch()
        {
            // Arrange
            string password1 = "1";
            string password2 = "2";
            string account = "account";
            LoginController controller = new LoginController();

            // Act
            ViewResult result = await controller.RegisterConfirm(account, password1, password2) as ViewResult;

            // Assert
            RegisterConfirmViewModel vm = result.Model as RegisterConfirmViewModel;
            Assert.AreEqual("passwords do not match", vm.Message);
            Assert.IsNotNull(result);
        }
    }
}