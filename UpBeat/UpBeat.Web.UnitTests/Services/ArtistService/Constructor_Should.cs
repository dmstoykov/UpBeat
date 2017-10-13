namespace UpBeat.Web.UnitTests.Services.ArtistService
{
    using Moq;
    using NUnit.Framework;
    using System;
    using UpBeat.Data.Contracts;
    using UpBeat.Data.Models;
    using UpBeat.Services;

    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowException_WhenPassedNullArtistRepository()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ArtistService(null));
        }

        [Test]
        public void ReturnsAnInstance_WhenParametersAreNotNull()
        {
            // Arrange
            var artistRepositoryMock = new Mock<IGenericRepository<Artist>>();

            // Act
            var artistService = new ArtistService(artistRepositoryMock.Object);

            // Assert
            Assert.IsNotNull(artistService);
        }
    }
}
