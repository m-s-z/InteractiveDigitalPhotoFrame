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
using System.Web;

namespace WebApplication.Controllers.Tests
{
    [TestClass()]
    public class FolderControllerTests
    {
        [TestMethod()]
        public async Task FolderControllerIndexTest()
        {
            // Arrange
            var cloudService = new Mock<ICloudService>();
            cloudService.Setup(m => m.GetClouds(It.IsAny<string>())).Returns(Task.FromResult(new List<Cloud>()));
            var deviceService = new Mock<IDeviceService>();
            deviceService.Setup(m => m.GetDevices(It.IsAny<string>())).Returns(Task.FromResult(new List<DeviceName>()));
            FolderController controller = new FolderController(deviceService.Object, cloudService.Object);
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["UserId"]).Returns("test");
            controller.ControllerContext = controllerContext.Object;
            // Act
            ViewResult result = await controller.Index(null) as ViewResult;

            // Assert
            FolderViewModel vm = result.Model as FolderViewModel;
            Assert.AreEqual(0, vm.Devices.Count);
            Assert.AreEqual(0, vm.Folders.Count);
        }

        [TestMethod()]
        public async Task FolderControllerIndexWithIdTest()
        {
            // Arrange
            DeviceName devName = new DeviceName();
            Device dev = new Device();
            int deviceId = 1;
            dev.DeviceId = deviceId;
            devName.Device = dev;
            List<DeviceName> devList = new List<DeviceName>() { devName };

            var cloudService = new Mock<ICloudService>();
            cloudService.Setup(m => m.GetClouds(It.IsAny<string>())).Returns(Task.FromResult(new List<Cloud>()));
            var deviceService = new Mock<IDeviceService>();
            deviceService.Setup(m => m.GetDevices(It.IsAny<string>())).Returns(Task.FromResult(devList));
            FolderController controller = new FolderController(deviceService.Object, cloudService.Object);
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["UserId"]).Returns("test");
            controller.ControllerContext = controllerContext.Object;


            // Act
            ViewResult result = await controller.Index(deviceId) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public async Task FolderControllerNewFolderTest()
        {
            // Arrange
            var cloudService = new Mock<ICloudService>();
            cloudService.Setup(m => m.GetClouds(It.IsAny<string>())).Returns(Task.FromResult(new List<Cloud>()));
            FolderController controller = new FolderController(cloudService.Object);
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["UserId"]).Returns("test");
            controller.ControllerContext = controllerContext.Object;
            int deviceId = 1;

            // Act
            ViewResult result = await controller.NewFolder(deviceId) as ViewResult;

            // Assert
            NewFolderViewModel vm = result.Model as NewFolderViewModel;
            Assert.IsNotNull(result);
            Assert.AreEqual(deviceId, vm.DeviceId);
            Assert.AreEqual(0, controller.ViewBag.Clouds.Count);

        }

        [TestMethod()]
        public void FolderControllerDeleteFolderTest()
        {
            // Arrange
            var auth = new Mock<IAuthenticationService>();
            auth.Setup(m => m.IsAuthenticated(It.IsAny<HttpSessionStateBase>())).Returns(true);
            FolderController controller = new FolderController(auth.Object);
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["UserId"]).Returns("test");
            controller.ControllerContext = controllerContext.Object;
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
        public async Task FolderControllerConfirmDeleteFolderTest()
        {
            // Arrange
            var auth = new Mock<IAuthenticationService>();
            auth.Setup(m => m.IsAuthenticated(It.IsAny<HttpSessionStateBase>())).Returns(true);
            var folderService = new Mock<IFolderService>();
            folderService.Setup(m => m.deleteFolder(It.IsAny<int>())).Returns(Task.FromResult(true));
            FolderController controller = new FolderController(auth.Object, folderService.Object);
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["UserId"]).Returns("test");
            controller.ControllerContext = controllerContext.Object;
            int id = 1;

            // Act
            ViewResult result = await controller.ConfirmDeleteFolder(id) as ViewResult;

            // Assert
            folderService.Verify(m => m.deleteFolder(id));
            auth.Verify(m => m.IsAuthenticated(It.IsAny<HttpSessionStateBase>()));
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public async Task FolderControllerSelectFolderTestFlickr()
        {
            // Arrange
            Cloud cloud = new Cloud();
            int cloudId = 1;
            int deviceId = 1;
            cloud.Id = cloudId;
            cloud.Provider = ProviderType.Flicker;
            var auth = new Mock<IAuthenticationService>();
            auth.Setup(m => m.IsAuthenticated(It.IsAny<HttpSessionStateBase>())).Returns(true);
            var folderService = new Mock<IFolderService>();
            folderService.Setup(m => m.GetFlickrFolders(It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(new List<UniversalFolder>()));
            var cloudService = new Mock<ICloudService>();
            cloudService.Setup(m => m.GetCloud(It.IsAny<int>())).Returns(Task.FromResult(cloud));
            FolderController controller = new FolderController(auth.Object, folderService.Object, cloudService.Object);

            // Act
            ViewResult result = await controller.SelectFolder(cloudId, deviceId) as ViewResult;

            // Assert
            folderService.Verify(m => m.GetFlickrFolders(cloudId, deviceId));
            auth.Verify(m => m.IsAuthenticated(It.IsAny<HttpSessionStateBase>()));
            SelectFolderViewModel vm = result.Model as SelectFolderViewModel;
            Assert.AreEqual(cloud, vm.SelectedCloud);
            Assert.AreEqual(0, vm.Folders.Count);
            Assert.AreEqual(deviceId, vm.DeviceId);
        }
        [TestMethod()]
        public async Task FolderControllerSelectFolderTestDropbox()
        {
            // Arrange
            Cloud cloud = new Cloud();
            int cloudId = 1;
            int deviceId = 1;
            cloud.Id = cloudId;
            cloud.Provider = ProviderType.DropBox;
            var auth = new Mock<IAuthenticationService>();
            auth.Setup(m => m.IsAuthenticated(It.IsAny<HttpSessionStateBase>())).Returns(true);
            var folderService = new Mock<IFolderService>();
            folderService.Setup(m => m.GetDropboxFolders(It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(new List<UniversalFolder>()));
            var cloudService = new Mock<ICloudService>();
            cloudService.Setup(m => m.GetCloud(It.IsAny<int>())).Returns(Task.FromResult(cloud));
            FolderController controller = new FolderController(auth.Object, folderService.Object, cloudService.Object);

            // Act
            ViewResult result = await controller.SelectFolder(cloudId, deviceId) as ViewResult;

            // Assert
            folderService.Verify(m => m.GetDropboxFolders(cloudId, deviceId));
            auth.Verify(m => m.IsAuthenticated(It.IsAny<HttpSessionStateBase>()));
            SelectFolderViewModel vm = result.Model as SelectFolderViewModel;
            Assert.AreEqual(cloud, vm.SelectedCloud);
            Assert.AreEqual(0, vm.Folders.Count);
            Assert.AreEqual(deviceId, vm.DeviceId);
        }

        [TestMethod()]
        public async Task FolderControllerConfirmAddFolderTest()
        {
            // Arrange
            int cloudId = 1;
            int deviceId = 1;
            SelectFolderViewModel vm = new SelectFolderViewModel();
            string f1 = "folder1";
            string f2 = "folder2";
            List<string> foldersToBeAdded = new List<string>() { f1, f2};
            vm.SelectedFolders = foldersToBeAdded;
            var auth = new Mock<IAuthenticationService>();
            auth.Setup(m => m.IsAuthenticated(It.IsAny<HttpSessionStateBase>())).Returns(true);
            var folderService = new Mock<IFolderService>();
            folderService.Setup(m => m.GetDropboxFolders(It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(new List<UniversalFolder>()));
           
            FolderController controller = new FolderController(auth.Object, folderService.Object);

            // Act
            ViewResult result = await controller.ConfirmAddFolder(vm, cloudId, deviceId) as ViewResult;

            // Assert
            auth.Verify(m => m.IsAuthenticated(It.IsAny<HttpSessionStateBase>()));
            folderService.Verify(m => m.AddCloudFolders(foldersToBeAdded, cloudId, deviceId));
        }
    }
}