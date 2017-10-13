namespace UpBeat.Web.UnitTests.Services.TrackService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NUnit.Framework;
    using UpBeat.Data.Contracts;
    using Moq;
    using UpBeat.Data.Models;
    using UpBeat.Services;

    [TestFixture]
    public class Add_Should
    {
        [Test]
        public void ThrowException_WhenPassedNullTrack()
        {
            // Arrange
            var trackRepositoryMock = new Mock<IGenericRepository<Track>>();
            var albumRepopositoryMock = new Mock<IGenericRepository<Album>>();

            // Act
            var trackService = new TrackService(trackRepositoryMock.Object, albumRepopositoryMock.Object);

            // Assert
            Assert.Throws<ArgumentNullException>(() => trackService.Add(null, "Sample text"));
        }

        [Test]
        public void ThrowException_WhenPassedEmptyAlbumName()
        {
            // Arrange
            var trackRepositoryMock = new Mock<IGenericRepository<Track>>();
            var albumRepopositoryMock = new Mock<IGenericRepository<Album>>();
            var trackModel = new Track()
            {
                Id = 1,
                Name = "Sample album 1",
            };

            // Act
            var trackService = new TrackService(trackRepositoryMock.Object, albumRepopositoryMock.Object);

            // Assert
            Assert.Throws<ArgumentException>(() => trackService.Add(trackModel, string.Empty));
        }

        [Test]
        public void ThrowException_WhenPassedExistingTrack()
        {
            // Arrange
            var trackRepositoryMock = new Mock<IGenericRepository<Track>>();
            var albumRepopositoryMock = new Mock<IGenericRepository<Album>>();
            var trackModel = new Track()
            {
                Id = 1,
                Name = "Sample album 1",
            };

            var tracksList = new List<Track>() { trackModel }.AsQueryable();

            trackRepositoryMock.Setup(x => x.All).Returns(tracksList);

            // Act
            var trackService = new TrackService(trackRepositoryMock.Object, albumRepopositoryMock.Object);

            // Assert
            Assert.Throws<ArgumentException>(() => trackService.Add(trackModel, "Sample album"));
        }
    }
}
