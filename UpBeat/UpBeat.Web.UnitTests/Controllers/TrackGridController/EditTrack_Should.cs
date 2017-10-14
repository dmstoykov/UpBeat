namespace UpBeat.Web.UnitTests.Controllers.TrackGridController
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NUnit.Framework;
    using UpBeat.Data.Models;
    using AutoMapper;
    using Moq;
    using UpBeat.Services.Contracts;
    using UpBeat.Web.Areas.Administration.Controllers;
    using UpBeat.Web.Areas.Administration.Models;
    using System.Web.Mvc;

    [TestFixture]
    public class EditTrack_Should
    {
        [Test]
        public void NotCallTrackServiceUpdate_WhenPassedNullTrackGridViewModel()
        {
            // Arrange 
            var mapperMock = new Mock<IMapper>();
            var trackServiceMock = new Mock<ITrackService>();

            // Act
            var trackGridController = new TrackGridController(mapperMock.Object, trackServiceMock.Object);
            trackGridController.EditTrack(null);

            // Assert
            trackServiceMock.Verify(x => x.Update(It.IsAny<Track>()), Times.Never);
        }

        [Test]
        public void CallTrackServiceUpdate_WhenPassedTrackGridViewModel()
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
            trackGridController.EditTrack(trackGridViewModel);

            // Assert
            trackServiceMock.Verify(x => x.Update(trackDbModel), Times.Once);
        }

        [Test]
        public void ReturnJsonReturnWithViewModel_WhenPassedTrackGridViewModel()
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
            var jsonResult = trackGridController.EditTrack(trackGridViewModel) as JsonResult;
            var jsonResultModel = jsonResult.Data.GetType().GetProperty("trackViewModel").GetValue(jsonResult.Data, null);

            // Assert
            Assert.AreSame(jsonResultModel, trackGridViewModel);
        }
    }
}
