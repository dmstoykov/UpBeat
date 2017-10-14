namespace UpBeat.Web.UnitTests.Controllers.AdminPanelController
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NUnit.Framework;
    using AutoMapper;
    using Moq;
    using UpBeat.Services.Contracts;
    using UpBeat.Data.Models;
    using UpBeat.Web.Areas.Administration.Controllers;
    using UpBeat.Web.Areas.Administration.Models;
    using System.ComponentModel.DataAnnotations;

    [TestFixture]
    public class AddTrack_Post_Should
    {
        [Test]
        public void AddModel_WhenViewModelIsValid()
        {
            // Arrange 
            var mapperMock = new Mock<IMapper>();
            var albumServiceMock = new Mock<IAlbumService>();
            var trackServiceMock = new Mock<ITrackService>();
            var artistServiceMock = new Mock<IArtistService>();
            var trackViewModel = new TrackViewModel()
            {
                Name = "Some album",
                PreviewUrl = "http://some-source/some-file.mp3",
                AlbumName = "Some album name"
            };

            var trackDbModel = new Track()
            {
                Name = trackViewModel.Name,
                PreviewUrl = trackViewModel.PreviewUrl,
                Album = new Album() { Name = trackViewModel.AlbumName }
            };

            var validationContext =
                new System.ComponentModel.DataAnnotations.ValidationContext(trackViewModel, null, null);

            var results = new List<ValidationResult>();

            var isModelValid = Validator.TryValidateObject(trackViewModel, validationContext, results);

            mapperMock.Setup(x => x.Map<Track>(trackViewModel)).Returns(trackDbModel);

            // Act
            var adminPanelController = new AdminPanelController(mapperMock.Object, albumServiceMock.Object,
            trackServiceMock.Object, artistServiceMock.Object);
            adminPanelController.AddTrack(trackViewModel);

            // Assert
            Assert.IsTrue(isModelValid);
            trackServiceMock.Verify(x => x.Add(trackDbModel, trackViewModel.AlbumName), Times.Once);
        }

        [Test]
        public void ShowInvalidModelState_WhenPassedInvalidViewModel()
        {
            // Arrange 
            var mapperMock = new Mock<IMapper>();
            var albumServiceMock = new Mock<IAlbumService>();
            var trackServiceMock = new Mock<ITrackService>();
            var artistServiceMock = new Mock<IArtistService>();
            var trackViewModel = new TrackViewModel()
            {
                Name = "Some album",
                PreviewUrl = "http://some-source/some-file.mp3",
            };

            var validationContext =
                new System.ComponentModel.DataAnnotations.ValidationContext(trackViewModel, null, null);

            var results = new List<ValidationResult>();

            var isModelValid = Validator.TryValidateObject(trackViewModel, validationContext, results);

            // Act & Assert
            Assert.IsFalse(isModelValid);
        }
    }
}
