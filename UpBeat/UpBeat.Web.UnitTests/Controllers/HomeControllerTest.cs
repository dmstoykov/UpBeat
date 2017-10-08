using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UpBeat.Web;
using UpBeat.Web.Controllers;
using AutoMapper;
using Moq;

namespace UpBeat.Web.UnitTests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            var mockedMapper = new Mock<IMapper>();
            HomeController controller = new HomeController(mockedMapper.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            var mockedMapper = new Mock<IMapper>();
            HomeController controller = new HomeController(mockedMapper.Object);

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            var mockedMapper = new Mock<IMapper>();
            HomeController controller = new HomeController(mockedMapper.Object);

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
