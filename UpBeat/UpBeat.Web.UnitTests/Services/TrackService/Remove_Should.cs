
namespace UpBeat.Web.UnitTests.Services.TrackService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;
    using UpBeat.Data.Contracts;
    using Moq;
    using UpBeat.Data.Models;
    using UpBeat.Services;

    [TestFixture]
    public class Remove_Should
    {
        [Test]
        public void ThrowException_WhenPassedNullTrack()
        {
            // Arrange
            var trackRepositoryMock = new Mock<IGenericRepository<Track>>();
            var albumRepositoryMock = new Mock<IGenericRepository<Album>>();

            // Act
            var trackService = new TrackService(trackRepositoryMock.Object, albumRepositoryMock.Object);

            // Assert
            Assert.Throws<ArgumentNullException>(() => trackService.Remove(null));
        }

        [Test]
        public void ThrowException_WhenPassedNonExistingTrack()
        {
            // Arrange
            var trackRepositoryMock = new Mock<IGenericRepository<Track>>();
            var albumRepositoryMock = new Mock<IGenericRepository<Album>>();
            var trackModel = new Track()
            {
                Id = 1,
                Name = "Sample track 1",
            };

            var trackList = new List<Track>() { }.AsQueryable();

            trackRepositoryMock.Setup(x => x.All).Returns(trackList);

            // Act
            var trackService = new TrackService(trackRepositoryMock.Object, albumRepositoryMock.Object);

            // Assert
            Assert.Throws<ArgumentException>(() => trackService.Remove(trackModel));
        }

        [Test]
        public void RemoveTrack_WhenPassedCorrectParameters()
        {
            // Arrange
            var trackRepositoryMock = new Mock<IGenericRepository<Track>>();
            var albumRepositoryMock = new Mock<IGenericRepository<Album>>();
            var trackModel = new Track()
            {
                Id = 1,
                Name = "Sample track 1",
            };

            var trackList = new List<Track>() { trackModel }.AsQueryable();

            trackRepositoryMock.Setup(x => x.All).Returns(trackList);
            trackRepositoryMock.Setup(x => x.Remove(trackModel)).Callback(() =>
            {
                trackRepositoryMock.Setup(x => x.All).Returns(new List<Track>() { }.AsQueryable());
            });

            // Act
            var trackService = new TrackService(trackRepositoryMock.Object, albumRepositoryMock.Object);
            trackService.Remove(trackModel);

            // Assert
            Assert.AreEqual(trackService.Data.All.Count(), 0);
        }
    }
}
