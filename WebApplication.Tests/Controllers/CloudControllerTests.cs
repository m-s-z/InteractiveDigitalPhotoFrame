﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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

namespace WebApplication.Controllers.Tests
{
    [TestClass()]
    public class CloudControllerTests
    {
        [TestMethod()]
        public async void CloudControllerIndexTest()
        {
            // Arrange
            CloudController controller = new CloudController();

            // Act
            ViewResult result = await controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void CloudControllerNewCloudTest()
        {
            // Arrange
            CloudController controller = new CloudController();

            // Act
            ViewResult result = controller.NewCloud() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void CloudControllerDeleteCloudTest()
        {
            // Arrange
            int id = 1;
            var service = new Mock<CloudService>();
            service.Setup(m => m.removeCloud(It.IsAny<int>())).Returns(Task.FromResult(true));
            CloudController controller = new CloudController(service.Object);


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
            CloudController controller = new CloudController();
            int id = 1;
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["UserId"]).Returns("test");
            controller.ControllerContext = controllerContext.Object;
            // Act
            ViewResult result = await controller.ConfirmDeleteCloud(id) as ViewResult;

            // Assert
            Assert.IsTrue(true);
        }
    }
}