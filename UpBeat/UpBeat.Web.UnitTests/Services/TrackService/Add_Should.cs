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
    public class Add_Should
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
            Assert.Throws<ArgumentNullException>(() => trackService.Add(null, "Sample text"));
        }

        [Test]
        public void ThrowException_WhenPassedEmptyAlbumName()
        {
            // Arrange
            var trackRepositoryMock = new Mock<IGenericRepository<Track>>();
            var albumRepositoryMock = new Mock<IGenericRepository<Album>>();
            var trackModel = new Track()
            {
                Id = 1,
                Name = "Sample track 1",
            };

            // Act
            var trackService = new TrackService(trackRepositoryMock.Object, albumRepositoryMock.Object);

            // Assert
            Assert.Throws<ArgumentException>(() => trackService.Add(trackModel, string.Empty));
        }

        [Test]
        public void ThrowException_WhenPassedExistingTrack()
        {
            // Arrange
            var trackRepositoryMock = new Mock<IGenericRepository<Track>>();
            var albumRepositoryMock = new Mock<IGenericRepository<Album>>();
            var trackModel = new Track()
            {
                Id = 1,
                Name = "Sample track 1",
            };

            var tracksList = new List<Track>() { trackModel }.AsQueryable();

            trackRepositoryMock.Setup(x => x.All).Returns(tracksList);

            // Act
            var trackService = new TrackService(trackRepositoryMock.Object, albumRepositoryMock.Object);


            // Assert
            Assert.Throws<ArgumentException>(() => trackService.Add(trackModel, "Sample album"));
        }

        [Test]
        public void ThrowException_WhenPassedNonExistingAlbumName()
        {
            // Arrange
            var trackRepositoryMock = new Mock<IGenericRepository<Track>>();
            var albumRepositoryMock = new Mock<IGenericRepository<Album>>();
            var trackModel = new Track()
            {
                Id = 1,
                Name = "Sample track 1",
            };

            var tracksList = new List<Track>() { }.AsQueryable();
            var albumsList = new List<Album>() { }.AsQueryable();

            albumRepositoryMock.Setup(x => x.All).Returns(albumsList);
            trackRepositoryMock.Setup(x => x.All).Returns(tracksList);

            // Act
            var trackService = new TrackService(trackRepositoryMock.Object, albumRepositoryMock.Object);

            // Assert
            Assert.Throws<ArgumentNullException>(() => trackService.Add(trackModel, "Sample album"));
        }

        [Test]
        public void AddTrack_WhenPassedCorrectParameters()
        {
            // Arrange
            var trackRepositoryMock = new Mock<IGenericRepository<Track>>();
            var albumRepositoryMock = new Mock<IGenericRepository<Album>>();
            var trackModel = new Track()
            {
                Id = 1,
                Name = "Sample track 1",
            };

            var albumModel = new Album()
            {
                Id = 1,
                Name = "Sample album 1"
            };

            var tracksList = new List<Track>() { }.AsQueryable();
            var albumsList = new List<Album>() { albumModel }.AsQueryable();

            albumRepositoryMock.Setup(x => x.All).Returns(albumsList);
            trackRepositoryMock.Setup(x => x.All).Returns(tracksList);
            trackRepositoryMock.Setup(x => x.Add(trackModel)).Callback(() =>
            {
                trackModel.Album = albumModel;

                trackRepositoryMock.Setup(x => x.All).Returns(new List<Track>() { trackModel }.AsQueryable());
            });

            // Act
            var trackService = new TrackService(trackRepositoryMock.Object, albumRepositoryMock.Object);
            trackService.Add(trackModel, "Sample album 1");

            // Assert
            Assert.AreSame(albumModel, trackService.Data.All.FirstOrDefault().Album);
        }
    }
}
