using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApplication.ViewModels;

namespace WebApplication.Controllers.Tests
{
    [TestClass()]
    public class DeviceControllerTests
    {
        [TestMethod()]
        public void DeviceControllerIndexTest()
        {
            // Arrange
            DeviceController controller = new DeviceController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
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
        public void DeviceControllerDeleteDeviceTest()
        {
            // Arrange
            DeviceController controller = new DeviceController();
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
        public void DeviceControllerConfirmDeleteDeviceTest()
        {
            // Arrange
            DeviceController controller = new DeviceController();
            int id = 1;

            // Act
            ViewResult result = controller.ConfirmDeleteDevice(id) as ViewResult;

            // Assert
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public async Task DeviceControllerPairDeviceTest()
        {
            // Arrange
            DeviceController controller = new DeviceController();
            String paircode = "device name";

            // Act
            ViewResult result = await controller.PairDevice(paircode) as ViewResult;

            // Assert
            Assert.AreEqual((result.Model as PairDeviceViewModel).Result, "Success");
            Assert.IsNotNull(result);
        }
    }
}