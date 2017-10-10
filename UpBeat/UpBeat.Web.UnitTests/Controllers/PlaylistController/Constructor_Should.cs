namespace UpBeat.Web.UnitTests.Controllers.PlaylistController
{
    using System;
    using NUnit.Framework;
    using Moq;
    using AutoMapper;
    using UpBeat.Services.Contracts;
    using Upbeat = UpBeat.Web.Controllers;

    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ReturnsAnInstance_WhenParameterIsNotNull()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            var albumService = new Mock<IAlbumService>();

            // Act & Assert
            Assert.DoesNotThrow(() => new Upbeat.PlaylistController(mapperMock.Object, albumService.Object));
        }

        [Test]
        public void ThrowException_WhenAlbumServiceIsNull()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => new Upbeat.PlaylistController(mapperMock.Object, null));
        }

        [Test]
        public void ThrowException_WhenMapperIsNull()
        {
            // Arrange
            var albumServiceMock = new Mock<IAlbumService>();

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => new Upbeat.PlaylistController(null, albumServiceMock.Object));
        }
    }
}
