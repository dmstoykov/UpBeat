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
using TestStack.FluentMVCTesting;
using UpBeat.Common.Constants;

namespace UpBeat.Web.UnitTests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        [Test]
        public void Index_ShouldRenderDefaultView_WhenCalled()
        {
            // Arrange
            HomeController homeController = new HomeController(); 

            // Act
            ViewResult testResult = homeController.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(testResult);
        }

        [Test]
        public void HomePage_ShouldReturnPartialView_WhenCalled()
        {
            // Arrange
            HomeController homeController = new HomeController();

            // Act & Assert
            homeController
                .WithCallTo(x => x.HomePage())
                .ShouldRenderPartialView(Views.HomePagePartial);
        }

        [Test]
        public void About_ShouldReturnAboutView_WhenCalled()
        {
            // Arrange
            HomeController homeController = new HomeController();

            // Act & Assert
            homeController
                .WithCallTo(x => x.About())
                .ShouldRenderDefaultView();
        }

        [Test]
        public void AboutContent_ShouldReturnPartialView_WhenCalled()
        {
            // Arrange
            HomeController homeController = new HomeController();

            // Act & Assert
            homeController
                .WithCallTo(x => x.AboutContent())
                .ShouldRenderPartialView(Views.AboutContentPartial);
        }

        [Test]
        public void AboutContent_ShouldPassPageTitleInViewbag_WhenCalled()
        {
            // Arrange
            var expectedTitle = "About the creator";
            HomeController homeController = new HomeController();

            // Act 
            var result = homeController.AboutContent() as PartialViewResult;

            // Assert
            Assert.AreEqual(expectedTitle, result.ViewBag.Title);
        }
    }
}
