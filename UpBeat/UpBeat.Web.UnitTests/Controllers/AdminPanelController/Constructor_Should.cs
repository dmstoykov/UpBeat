namespace UpBeat.Web.UnitTests.Controllers.AdminPanelController
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
            new AdminPanelController(null, null, null, null)).Message;

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
            new AdminPanelController(mapperMock.Object, null, null, null)).Message;

            // Assert
            Assert.IsTrue(errorMessage.Contains("IAlbumService"));
        }

        [Test]
        public void ThrowException_WhenPassedNullTrackService()
        {
            // Arrange 
            var mapperMock = new Mock<IMapper>();
            var albumServiceMock = new Mock<IAlbumService>();

            // Act
            var errorMessage = Assert.Throws<ArgumentNullException>(() =>
            new AdminPanelController(mapperMock.Object, albumServiceMock.Object, null, null)).Message;

            // Assert
            Assert.IsTrue(errorMessage.Contains("ITrackService"));
        }

        [Test]
        public void ThrowException_WhenPassedNullArtistService()
        {
            // Arrange 
            var mapperMock = new Mock<IMapper>();
            var albumServiceMock = new Mock<IAlbumService>();
            var trackServiceMock = new Mock<ITrackService>();

            // Act
            var errorMessage = Assert.Throws<ArgumentNullException>(() =>
            new AdminPanelController(mapperMock.Object, albumServiceMock.Object,
            trackServiceMock.Object, null)).Message;

            // Assert
            Assert.IsTrue(errorMessage.Contains("IArtistService"));
        }

        [Test]
        public void CreateInstance_WhenPassedAllDependencies()
        {
            // Arrange 
            var mapperMock = new Mock<IMapper>();
            var albumServiceMock = new Mock<IAlbumService>();
            var trackServiceMock = new Mock<ITrackService>();
            var artistServiceMock = new Mock<IArtistService>();

            // Act & Assert
            Assert.DoesNotThrow(() => new AdminPanelController(mapperMock.Object, albumServiceMock.Object,
            trackServiceMock.Object, artistServiceMock.Object));
        }
    }
}
