namespace UpBeat.Web.UnitTests.Controllers.TrackGridController
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NUnit.Framework;
    using Moq;
    using AutoMapper;
    using UpBeat.Services.Contracts;
    using UpBeat.Web.Areas.Administration.Controllers;
    using UpBeat.Data.Models;
    using UpBeat.Web.Areas.Administration.Models;

    [TestFixture]
    public class RemoveTrack_Should
    {
        [Test]
        public void NotCallTrackServiceRemove_WhenPassedNullTrackGridViewModel()
        {
            // Arrange 
            var mapperMock = new Mock<IMapper>();
            var trackServiceMock = new Mock<ITrackService>();

            // Act
            var trackGridController = new TrackGridController(mapperMock.Object, trackServiceMock.Object);
            trackGridController.RemoveTrack(null);

            // Assert
            trackServiceMock.Verify(x => x.Remove(It.IsAny<Track>()), Times.Never);
        }

        [Test]
        public void CallTrackServiceRemove_WhenPassedTrackGridViewModel()
        {
            // Arrange 
            var mapperMock = new Mock<IMapper>();
            var trackServiceMock = new Mock<ITrackService>();
            var trackGridViewModel = new TrackGridViewModel()
            {
                Id = 1,
                Name = "Some track grid model",
                PreviewUrl = "https://source-site/source-file.mp3"
            };

            var trackDbModel = new Track()
            {
                Id = 1,
                Name = "Some track grid model",
                PreviewUrl = "https://source-site/source-file.mp3"
            };
            mapperMock.Setup(x => x.Map<Track>(trackGridViewModel)).Returns(trackDbModel);

            // Act
            var trackGridController = new TrackGridController(mapperMock.Object, trackServiceMock.Object);
            trackGridController.RemoveTrack(trackGridViewModel);

            // Assert
            trackServiceMock.Verify(x => x.Remove(trackDbModel), Times.Once);
        }
    }
}
