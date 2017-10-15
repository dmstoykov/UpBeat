using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using UpBeat.Web;
using UpBeat.Web.Controllers;
using AutoMapper;
using Moq;
using NUnit.Framework;

namespace UpBeat.Web.UnitTests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        [Test]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController(); 

            // Act
            ViewResult testResult = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(testResult);
        }
    }
}
