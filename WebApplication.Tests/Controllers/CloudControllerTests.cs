using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApplication.ViewModels;
using Moq;
using WebApplication.Services;
using WebApplication.Models;
using System.Net.Http;
using FlickrNet;

namespace WebApplication.Controllers.Tests
{
    [TestClass()]
    public class CloudControllerTests
    {
        [TestMethod()]
        public async Task CloudControllerIndexTest()
        {
            // Arrange
            var service = new Mock<ICloudService>();
            List<Cloud> list = new List<Cloud>();
            service.Setup(m => m.GetClouds(It.IsAny<string>())).Returns(Task.FromResult(list));
            CloudController controller = new CloudController(service.Object);
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["UserId"]).Returns("test");
            controller.ControllerContext = controllerContext.Object;
            // Act
            ViewResult result = await controller.Index() as ViewResult;

            // Assert
            service.Verify(m => m.GetClouds(It.IsAny<string>()));
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void CloudControllerNewCloudTest()
        {
            // Arrange
            CloudController controller = new CloudController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["UserId"]).Returns("test");
            controller.ControllerContext = controllerContext.Object;

            // Act
            ViewResult result = controller.NewCloud() as ViewResult;

            // Assert
            Assert.AreEqual(2, controller.ViewBag.Providers.Count);
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void CloudControllerDeleteCloudTest()
        {
            // Arrange
            int id = 1;
            CloudController controller = new CloudController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["UserId"]).Returns("test");
            controller.ControllerContext = controllerContext.Object;

            // Act
            ViewResult result = controller.DeleteCloud(id) as ViewResult;

            // Assert
            Assert.AreEqual((result.Model as ConfirmDeleteCloudViewModel).CloudId, id);
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public async Task CloudControllerConfirmDeleteCloudTest()
        {
            // Arrange
            var service = new Mock<ICloudService>();
            service.Setup(m => m.removeCloud(It.IsAny<int>())).Returns(Task.FromResult(true));
            CloudController controller = new CloudController(service.Object);
            int id = 1;
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["UserId"]).Returns("test");
            controller.ControllerContext = controllerContext.Object;
            // Act
            ViewResult result = await controller.ConfirmDeleteCloud(id) as ViewResult;

            // Assert
            service.Verify(m => m.removeCloud(It.IsAny<int>()));
        }

        //the problem here is i cant mock this.Request request is readonly
        [TestMethod()]
        public void CloudControllerConnectWithProviderTestWithFlickr()
        {
            // Arrange
            CloudController controller = new CloudController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["UserId"]).Returns("test");
            controller.ControllerContext = controllerContext.Object;
            
            // Act
            ViewResult result = controller.ConnectWithProvider(ProviderType.Flicker, "account name") as ViewResult;

            // Assert
            Assert.AreNotEqual(null, controller.HttpContext.Session["RequestToken"]);
        }

        //does not work because i cant insert the flickr mock
        [TestMethod()]
        public async Task CloudControllerConfirmFlickrConnectionTest()
        {
            // Arrange
            var service = new Mock<ICloudService>();
            var flickrService = new Mock<Flickr>();
            flickrService.Setup(m => m.OAuthGetAccessToken(It.IsAny<OAuthRequestToken>(), It.IsAny<string>())).Returns(new OAuthAccessToken());
            service.Setup(m => m.CreateFlickerAccount(It.IsAny<OAuthAccessToken>(), It.IsAny<string>(), It.IsAny<string>()
                )).Returns(Task.FromResult(true));
            CloudController controller = new CloudController(service.Object);
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["UserId"]).Returns("test");
            controllerContext.SetupGet(p => p.HttpContext.Session["RequestToken"]).Returns(new OAuthRequestToken());

            controller.ControllerContext = controllerContext.Object;
            // Act
            ViewResult result = await controller.ConfirmFlickrConnection("test") as ViewResult;

            // Assert
            service.Verify(m => m.CreateFlickerAccount(It.IsAny<OAuthAccessToken>(), It.IsAny<string>(), It.IsAny<string>()));
        }

        //the problem here is i cant mock this.Request request is readonly
        [TestMethod()]
        public async Task CloudControllerConfirmDropBoxConnectionTest()
        {
            // Arrange
            string code = "test";
            string state = "test2";
            string newCloudName = "cloud";
            var service = new Mock<ICloudService>();
            service.Setup(m => m.CreateFlickerAccount(It.IsAny<OAuthAccessToken>(), It.IsAny<string>(), It.IsAny<string>()
                )).Returns(Task.FromResult(true));
            CloudController controller = new CloudController(service.Object);
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["UserId"]).Returns("test");
            controllerContext.SetupGet(p => p.HttpContext.Session["DropBoxState"]).Returns(state);
            controllerContext.SetupGet(p => p.HttpContext.Session["NewCloudName"]).Returns(newCloudName);

            controller.ControllerContext = controllerContext.Object;
            
            // Act
            ViewResult result = await controller.ConfirmDropBoxConnection(code, state) as ViewResult;

            // Assert
            service.Verify(m => m.CreateDropBoxAccount(It.IsAny<string>(), newCloudName, "test", It.IsAny<string>()));
        }
    }
}