namespace UpBeat.Web.UnitTests.Services.TrackService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NUnit.Framework;
    using Moq;
    using UpBeat.Data.Contracts;
    using UpBeat.Data.Models;
    using UpBeat.Services;

    [TestFixture]
    public class Update_Should
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
            Assert.Throws<ArgumentNullException>(() => trackService.Update(null));
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
            Assert.Throws<ArgumentException>(() => trackService.Update(trackModel));
        }

        [Test]
        public void UpdateTrack_WhenPassedCorrectParameters()
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
            trackRepositoryMock.Setup(x => x.Update(trackModel)).Callback(() =>
            {
                trackModel.Name = "Sample track updated";

                trackRepositoryMock.Setup(x => x.All).Returns(new List<Track>() { trackModel }.AsQueryable());
            });

            // Act
            var trackService = new TrackService(trackRepositoryMock.Object, albumRepositoryMock.Object);
            trackService.Update(trackModel);

            // Assert
            Assert.IsTrue(trackService.Data.All.FirstOrDefault().Name.Contains("updated"));
        }
    }
}
