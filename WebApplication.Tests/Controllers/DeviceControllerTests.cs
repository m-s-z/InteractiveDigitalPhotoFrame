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
using IDPFLibrary.DTO;
using System.Net;

namespace WebApplication.Controllers.Tests
{
    [TestClass()]
    public class DeviceControllerTests
    {
        [TestMethod()]
        public async Task DeviceControllerIndexTest()
        {
            // Arrange
            DeviceName devName = new DeviceName();
            Device dev = new Device();
            int deviceId = 1;
            dev.DeviceId = deviceId;
            devName.Device = dev;
            List<DeviceName> devList = new List<DeviceName>() { devName };
            string username = "test";

            var auth = new Mock<IAuthenticationService>();
            auth.Setup(m => m.getLoggedInUsername(It.IsAny<HttpSessionStateBase>())).Returns(username);
            var deviceService = new Mock<IDeviceService>();
            deviceService.Setup(m => m.GetDevices(It.IsAny<string>())).Returns(Task.FromResult(devList));

            DeviceController controller = new DeviceController(deviceService.Object, auth.Object);

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["UserId"]).Returns(username);
            controller.ControllerContext = controllerContext.Object;

            // Act
            ViewResult result = await controller.Index() as ViewResult;

            // Assert
            DeviceViewModel vm = result.Model as DeviceViewModel;
            Assert.AreEqual(devList, vm.Devices);
            deviceService.Verify(m => m.GetDevices(username));
            auth.Verify(m => m.getLoggedInUsername(It.IsAny<HttpSessionStateBase>()));
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void DeviceControllerNewDeviceTest()
        {
            // Arrange
            DeviceController controller = new DeviceController();

            // Act
            ViewResult result = controller.NewDevice() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public async Task DeviceControllerGeneratePairCodeTest()
        {
            // Arrange
            string code = "test";
            var deviceService = new Mock<IDeviceService>();
            deviceService.Setup(m => m.GeneratePairCode(It.IsAny<int>(), It.IsAny<string>())).Returns(Task.FromResult(code));
            DeviceController controller = new DeviceController(deviceService.Object);


            // Act
            JsonResult result = await controller.GeneratePairCode(1, "") as JsonResult;

            // Assert
            Assert.AreEqual(code, result.Data);
        }

        [TestMethod()]
        public void DeviceControllerDeleteDeviceTest()
        {
            // Arrange
            var auth = new Mock<IAuthenticationService>();
            auth.Setup(m => m.IsAuthenticated(It.IsAny<HttpSessionStateBase>())).Returns(true);
            DeviceController controller = new DeviceController(auth.Object);
            int id = 1;
            String name = "device name";

            // Act
            ViewResult result = controller.DeleteDevice(id, name) as ViewResult;

            // Assert
            Assert.AreEqual((result.Model as ConfirmDeleteDeviceViewModel).Id, id);
            Assert.AreEqual((result.Model as ConfirmDeleteDeviceViewModel).Name, name);
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public async Task DeviceControllerConfirmDeleteDeviceTest()
        {
            // Arrange
            int id = 1;
            string username = "test";
            var auth = new Mock<IAuthenticationService>();
            auth.Setup(m => m.IsAuthenticated(It.IsAny<HttpSessionStateBase>())).Returns(true);
            auth.Setup(m => m.getLoggedInUsername(It.IsAny<HttpSessionStateBase>())).Returns(username);
            var deviceService = new Mock<IDeviceService>();
            deviceService.Setup(m => m.UnpairDevice(It.IsAny<int>(), It.IsAny<string>())).Returns(Task.FromResult(true));
            DeviceController controller = new DeviceController(deviceService.Object, auth.Object);

            // Act
            ViewResult result = await controller.ConfirmDeleteDevice(id) as ViewResult;

            // Assert
            auth.Verify(m => m.getLoggedInUsername(It.IsAny<HttpSessionStateBase>()));
            auth.Verify(m => m.IsAuthenticated(It.IsAny<HttpSessionStateBase>()));
            deviceService.Verify(m => m.UnpairDevice(id, username));
        }

        [TestMethod()]
        public async Task DeviceControllerPairDeviceTest()
        {
            // Arrange
            string paircode = "code";
            string name = "new Name";
            string responseInformation = "Success!";
            string username = "username";

            var auth = new Mock<IAuthenticationService>();
            auth.Setup(m => m.IsAuthenticated(It.IsAny<HttpSessionStateBase>())).Returns(true);
            auth.Setup(m => m.getLoggedInUsername(It.IsAny<HttpSessionStateBase>())).Returns(username);
            var deviceService = new Mock<IDeviceService>();
            deviceService.Setup(m => m.PairDevice(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(responseInformation));

            DeviceController controller = new DeviceController(deviceService.Object, auth.Object);

            // Act
            ViewResult result = await controller.PairDevice(paircode, name) as ViewResult;

            // Assert
            Assert.AreEqual((result.Model as PairDeviceViewModel).Result, responseInformation);
            auth.Verify(m => m.getLoggedInUsername(It.IsAny<HttpSessionStateBase>()));
            auth.Verify(m => m.IsAuthenticated(It.IsAny<HttpSessionStateBase>()));
            deviceService.Verify(m => m.PairDevice(paircode.ToUpper(), name, username));
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public async Task DeviceControllerCreateNewDeviceTest()
        {
            // Arrange
            int id = 1;
            string token = "token";
            CreateNewDeviceDTO dto = new CreateNewDeviceDTO(id, token);
            var deviceService = new Mock<IDeviceService>();
            deviceService.Setup(m => m.CreateDevice(It.IsAny<string>())).Returns(Task.FromResult(dto));
            DeviceController controller = new DeviceController(deviceService.Object);


            // Act
            JsonResult result = await controller.CreateNewDevice("") as JsonResult;

            // Assert
            Assert.AreEqual(dto, result.Data as CreateNewDeviceDTO);
        }

        [TestMethod()]
        public async Task DeviceControllerGetDeviceAccountsTest()
        {
            // Arrange
            GetDeviceAccountsDTO dto = new GetDeviceAccountsDTO();
            int id = 1;
            string token = "token";
            var deviceService = new Mock<IDeviceService>();
            deviceService.Setup(m => m.GetDeviceAccounts(It.IsAny<int>(), It.IsAny<string>())).Returns(Task.FromResult(dto));
            DeviceController controller = new DeviceController(deviceService.Object);


            // Act
            JsonResult result = await controller.GetDeviceAccounts(id, token) as JsonResult;

            // Assert
            Assert.AreEqual(dto, result.Data as GetDeviceAccountsDTO);
        }

        [TestMethod()]
        public async Task DeviceControllerUnpairDeviceTest()
        {
            // Arrange
            int id = 1;
            string token = "token";
            string username = "username";
            var deviceService = new Mock<IDeviceService>();
            deviceService.Setup(m => m.UnpairDevice(It.IsAny<int>(), It.IsAny<string>())).Returns(Task.FromResult(true));
            deviceService.Setup(m => m.DeviceIsAuthenticated(id, token)).Returns(Task.FromResult(true));
            var auth = new Mock<IAuthenticationService>();
            auth.Setup(m => m.GetAccountLogin(It.IsAny<int>())).Returns(Task.FromResult(username));

            DeviceController controller = new DeviceController(deviceService.Object, auth.Object);


            // Act
            HttpStatusCodeResult result = await controller.UnpairDevice(id, token, id) as HttpStatusCodeResult;

            // Assert
            Assert.AreEqual(200, result.StatusCode);
            deviceService.Verify(m => m.UnpairDevice(id, username));
            deviceService.Verify(m => m.DeviceIsAuthenticated(id, token));
            auth.Verify(m => m.GetAccountLogin(id));
            //Assert.AreEqual("An error occurred whilst processing your request.", result.StatusDescription);
        }

        [TestMethod()]
        public async Task DeviceControllerUnpairDeviceWrongDeviceTokenTest()
        {
            // Arrange
            int id = 1;
            string token = "token";
            var deviceService = new Mock<IDeviceService>();
            deviceService.Setup(m => m.DeviceIsAuthenticated(id, token)).Returns(Task.FromResult(false));

            DeviceController controller = new DeviceController(deviceService.Object);


            // Act
            HttpStatusCodeResult result = await controller.UnpairDevice(id, token, id) as HttpStatusCodeResult;

            // Assert
            Assert.AreEqual(403, result.StatusCode);
            deviceService.Verify(m => m.DeviceIsAuthenticated(id, token));
            Assert.AreEqual("invalid device token", result.StatusDescription);
        }

        [TestMethod()]
        public async Task DeviceControllerUnpairDeviceAccountIdCannotBeFoundTest()
        {
            // Arrange
            int id = 1;
            int accountId = 2;
            string token = "token";
            string username = "username";
            var deviceService = new Mock<IDeviceService>();
            deviceService.Setup(m => m.DeviceIsAuthenticated(id, token)).Returns(Task.FromResult(true));
            deviceService.Setup(m => m.UnpairDevice(It.IsAny<int>(), It.IsAny<string>())).Returns(Task.FromResult(false));

            var auth = new Mock<IAuthenticationService>();
            auth.Setup(m => m.GetAccountLogin(It.IsAny<int>())).Returns(Task.FromResult(username));

            DeviceController controller = new DeviceController(deviceService.Object, auth.Object);


            // Act
            HttpStatusCodeResult result = await controller.UnpairDevice(id, token, accountId) as HttpStatusCodeResult;

            // Assert
            Assert.AreEqual(404, result.StatusCode);
            deviceService.Verify(m => m.UnpairDevice(id, username));
            deviceService.Verify(m => m.DeviceIsAuthenticated(id, token));
            auth.Verify(m => m.GetAccountLogin(accountId));
            Assert.AreEqual("accountId not found", result.StatusDescription);
        }

        [TestMethod()]
        public async Task DeviceControllerGetAllPhotosUrlTest()
        {
            // Arrange
            GetAllFlickrPhotosURLResponseDTO dto = new GetAllFlickrPhotosURLResponseDTO();
            int id = 1;
            List<int> accounts = new List<int>() { 1 };
            string token = "token";
            var deviceService = new Mock<IDeviceService>();
            deviceService.Setup(m => m.DeviceIsAuthenticated(id, token)).Returns(Task.FromResult(true));
            deviceService.Setup(m => m.GetAllPhotosUrl(It.IsAny<List<int>>(), It.IsAny<int>())).Returns(Task.FromResult(dto));
            DeviceController controller = new DeviceController(deviceService.Object);


            // Act
            JsonResult result = await controller.GetAllPhotosUrl(id, token, accounts) as JsonResult;

            // Assert
            deviceService.Verify(m => m.GetAllPhotosUrl(accounts, id));
            deviceService.Verify(m => m.DeviceIsAuthenticated(id, token));
            Assert.AreEqual(dto, result.Data as GetAllFlickrPhotosURLResponseDTO);
        }
    }
}