namespace UpBeat.Web.UnitTests.Controllers.AdminPanelController
{
    using AutoMapper;
    using Moq;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using UpBeat.Data.Models;
    using UpBeat.Services.Contracts;
    using UpBeat.Web.Areas.Administration.Controllers;
    using UpBeat.Web.Areas.Administration.Models;

    [TestFixture]
    public class AddAlbum_Post_Should
    {
        [Test]
        public void AddModel_WhenViewModelIsValid()
        {
            // Arrange 
            var mapperMock = new Mock<IMapper>();
            var albumServiceMock = new Mock<IAlbumService>();
            var trackServiceMock = new Mock<ITrackService>();
            var artistServiceMock = new Mock<IArtistService>();
            var albumViewModel = new AlbumViewModel()
            {
                Name = "Some album",
                ReleaseDate = "1998-11-02",
                ArtistName = "Fernando"
            };

            var albumDbModel = new Album()
            {
                Name = albumViewModel.Name,
                ReleaseDate = albumViewModel.ReleaseDate,
            };

            var validationContext =
                new System.ComponentModel.DataAnnotations.ValidationContext(albumViewModel, null, null);

            var results = new List<ValidationResult>();

            var isModelValid = Validator.TryValidateObject(albumViewModel, validationContext, results);

            mapperMock.Setup(x => x.Map<Album>(albumViewModel)).Returns(albumDbModel);

            // Act
            var adminPanelController = new AdminPanelController(mapperMock.Object, albumServiceMock.Object,
            trackServiceMock.Object, artistServiceMock.Object);
            adminPanelController.AddAlbum(albumViewModel);

            // Assert
            Assert.IsTrue(isModelValid);
            albumServiceMock.Verify(x => x.Add(albumDbModel, albumViewModel.ArtistName), Times.Once);
        }

        [Test]
        public void ShowInvalidModelState_WhenPassedInvalidViewModel()
        {
            // Arrange 
            var mapperMock = new Mock<IMapper>();
            var albumServiceMock = new Mock<IAlbumService>();
            var trackServiceMock = new Mock<ITrackService>();
            var artistServiceMock = new Mock<IArtistService>();
            var albumViewModel = new AlbumViewModel()
            {
                Name = "Some album",
                ArtistName = "Fernando"
            };
            var validationContext = 
                new System.ComponentModel.DataAnnotations.ValidationContext(albumViewModel, null, null);

            var results = new List<ValidationResult>();

            var isModelValid = Validator.TryValidateObject(albumViewModel, validationContext, results);

            // Act & Assert
            Assert.IsFalse(isModelValid);
        }
    }
}
