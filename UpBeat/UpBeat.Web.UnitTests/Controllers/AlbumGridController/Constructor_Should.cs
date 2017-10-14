namespace UpBeat.Web.UnitTests.Controllers.AlbumGridController
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NUnit.Framework;
    using UpBeat.Web.Areas.Administration.Controllers;
    using Moq;
    using AutoMapper;
    using UpBeat.Services.Contracts;

    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowException_WhenPassedNullMapper()
        {
            // Arrange & Act
            var errorMessage = Assert.Throws<ArgumentNullException>(() =>
            new AlbumGridController(null, null)).Message;

            // Assert
            Assert.IsTrue(errorMessage.Contains("IMapper"));
        }

        [Test]
        public void ThrowException_WhenPassedNullAlbumService()
        {
            // Arrange 
            var mapperMock = new Mock<IMapper>();

            // Act
            var errorMessage = Assert.Throws<ArgumentNullException>(() =>
            new AlbumGridController(mapperMock.Object, null)).Message;

            // Assert
            Assert.IsTrue(errorMessage.Contains("IAlbumService"));
        }

        [Test]
        public void CreateInstance_WhenPassedAllDependencies()
        {
            // Arrange 
            var mapperMock = new Mock<IMapper>();
            var albumServiceMock = new Mock<IAlbumService>();

            // Act & Assert
            Assert.DoesNotThrow(() => new AlbumGridController(mapperMock.Object, albumServiceMock.Object));
        }
    }
}
