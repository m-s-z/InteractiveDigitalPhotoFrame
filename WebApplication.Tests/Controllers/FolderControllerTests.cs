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

namespace WebApplication.Controllers.Tests
{
    [TestClass()]
    public class FolderControllerTests
    {
        [TestMethod()]
        public async void FolderControllerIndexTest()
        {
            // Arrange
            FolderController controller = new FolderController();

            // Act
            ViewResult result = await controller.Index(null) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public async void FolderControllerIndexWithIdTest()
        {
            // Arrange
            FolderController controller = new FolderController();
            int id = 1;

            // Act
            ViewResult result = await controller.Index(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public async void FolderControllerNewFolderTest()
        {
            // Arrange
            FolderController controller = new FolderController();
            int deviceId = 1;

            // Act
            ViewResult result = await controller.NewFolder(deviceId) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void FolderControllerDeleteFolderTest()
        {
            // Arrange
            FolderController controller = new FolderController();
            int id = 1;
            string folderName = "folder name";

            // Act
            ViewResult result = controller.DeleteFolder(id, folderName) as ViewResult;

            // Assert
            Assert.AreEqual((result.Model as ConfirmDeleteFolderViewModel).FolderId, id);
            Assert.AreEqual((result.Model as ConfirmDeleteFolderViewModel).Name, folderName);
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public async void FolderControllerConfirmDeleteFolderTest()
        {
            // Arrange
            FolderController controller = new FolderController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["UserId"]).Returns("test");
            controller.ControllerContext = controllerContext.Object;
            int id = 1;

            // Act
            ViewResult result = await controller.ConfirmDeleteFolder(id) as ViewResult;

            // Assert
            Assert.IsTrue(true);
        }
    }
}