namespace UpBeat.Web.UnitTests.Services.TrackService
{
    using System;
    using NUnit.Framework;
    using Moq;
    using UpBeat.Data.Contracts;
    using UpBeat.Data.Models;
    using UpBeat.Services;

    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ReturnsAnInstance_WhenParametersAreNotNull()
        {
            // Arrange
            var trackRepositoryMock = new Mock<IGenericRepository<Track>>();
            var albumRepopositoryMock = new Mock<IGenericRepository<Album>>();

            // Act & Assert
            Assert.DoesNotThrow(() => new TrackService(trackRepositoryMock.Object, albumRepopositoryMock.Object));
        }
        [Test]
        public void ThrowException_WhenPassedNullTrackRepository()
        {
            // Arrange
            var albumRepopositoryMock = new Mock<IGenericRepository<Album>>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new TrackService(null, albumRepopositoryMock.Object));
        }

        [Test]
        public void ThrowException_WhenPassedNullAlbumRepository()
        {
            // Arrange
            var trackRepositoryMock = new Mock<IGenericRepository<Track>>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new TrackService(trackRepositoryMock.Object, null));
        }

    }
}
