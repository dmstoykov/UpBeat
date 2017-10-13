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
        public void ReturnsAnInstance_WhenParameterIsNotNull()
        {
            // Arrange
            var genericAlbumRepositoryMock = new Mock<IGenericRepository<Album>>();
            var genericArtistRepopositoryMock = new Mock<IGenericRepository<Artist>>();

            // Act & Assert
            Assert.DoesNotThrow(() => new AlbumService(genericAlbumRepositoryMock.Object, genericArtistRepopositoryMock.Object));
        }

        [Test]
        public void ThrowException_WhenGenericRepositoryIsNull()
        {
            // Arrange, Act & Assert
            Assert.Throws<NullReferenceException>(() => new AlbumService(null, null));
        }
    }
}
