namespace UpBeat.Web.UnitTests.Services.AlbumService
{
    using System;
    using Moq;
    using NUnit.Framework;
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
            var albumRepositoryMock = new Mock<IGenericRepository<Album>>();
            var artistRepopositoryMock = new Mock<IGenericRepository<Artist>>();

            // Act & Assert
            Assert.DoesNotThrow(() => new AlbumService(albumRepositoryMock.Object, artistRepopositoryMock.Object));
        }

        [Test]
        public void ThrowException_WhenAlbumRepositoryIsNull()
        {
            // Arrange
            var albumRepositoryMock = new Mock<IGenericRepository<Album>>();

            //  Act & Assert
            Assert.Throws<ArgumentNullException>(() => new AlbumService(albumRepositoryMock.Object, null));
        }

        [Test]
        public void ThrowException_WhenArtistRepositoryIsNull()
        {
            // Arrange
            var artistRepopositoryMock = new Mock<IGenericRepository<Artist>>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new AlbumService(null, artistRepopositoryMock.Object));
        }
    }
}
