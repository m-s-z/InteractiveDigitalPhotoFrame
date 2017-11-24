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
    public class FolderControllerTests
    {
        [TestMethod()]
        public void FolderControllerIndexTest()
        {
            // Arrange
            FolderController controller = new FolderController();

            // Act
            ViewResult result = controller.Index(null) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void FolderControllerIndexWithIdTest()
        {
            // Arrange
            FolderController controller = new FolderController();
            int id = 1;

            // Act
            ViewResult result = controller.Index(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void FolderControllerNewFolderTest()
        {
            // Arrange
            FolderController controller = new FolderController();

            // Act
            ViewResult result = controller.NewFolder() as ViewResult;

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
        public void FolderControllerConfirmDeleteFolderTest()
        {
            // Arrange
            FolderController controller = new FolderController();
            int id = 1;

            // Act
            ViewResult result = controller.ConfirmDeleteFolder(id) as ViewResult;

            // Assert
            Assert.IsTrue(true);
        }
    }
}