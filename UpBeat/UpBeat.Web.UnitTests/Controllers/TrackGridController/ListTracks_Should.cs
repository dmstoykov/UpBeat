namespace UpBeat.Web.UnitTests.Controllers.TrackGridController
{
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;
    using AutoMapper;
    using Moq;
    using UpBeat.Services.Contracts;
    using UpBeat.Web.Areas.Administration.Models;
    using UpBeat.Web.Areas.Administration.Controllers;
    using UpBeat.Data.Models;
    using System.Web.Mvc;
    using Kendo.Mvc.UI;

    [TestFixture]
    public class ListTracks_Should
    {
        [Test]
        public void ReturnJsonContainingTracks_WhenCalled()
        {
            // Arrange 
            var mapperMock = new Mock<IMapper>();
            var trackServiceMock = new Mock<ITrackService>();
            var dataSourceRequest = new DataSourceRequest();

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

            var tracksList = new List<Track>() { trackDbModel };
            trackServiceMock.Setup(x => x.GetAll()).Returns(tracksList);

            mapperMock.Setup(x => x.Map<Track, TrackGridViewModel>(trackDbModel)).Returns(trackGridViewModel);

            // Act
            var trackGridController = new TrackGridController(mapperMock.Object, trackServiceMock.Object);
            var jsonResult = trackGridController.ListTracks(dataSourceRequest) as JsonResult;
            var dataSourceResult = (jsonResult.Data as DataSourceResult).Data.GetEnumerator();
            dataSourceResult.MoveNext();

            // Assert
            Assert.AreSame(dataSourceResult.Current, trackGridViewModel);
        }

        [Test]
        public void ReturnJsonWithNoTracks_WhenDatabaseIsEmpty()
        {
            // Arrange 
            var mapperMock = new Mock<IMapper>();
            var trackServiceMock = new Mock<ITrackService>();
            var dataSourceRequest = new DataSourceRequest();

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

            var tracksList = new List<Track>() { };
            trackServiceMock.Setup(x => x.GetAll()).Returns(tracksList);

            mapperMock.Setup(x => x.Map<Track, TrackGridViewModel>(trackDbModel)).Returns(trackGridViewModel);

            // Act
            var trackGridController = new TrackGridController(mapperMock.Object, trackServiceMock.Object);
            var jsonResult = trackGridController.ListTracks(dataSourceRequest) as JsonResult;
            var dataSourceResult = (jsonResult.Data as DataSourceResult).Data.Cast<object>().ToList();

            // Assert
            Assert.AreEqual(dataSourceResult.Count, 0);
        }
    }
}
