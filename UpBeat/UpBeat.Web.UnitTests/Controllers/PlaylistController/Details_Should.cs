namespace UpBeat.Web.UnitTests.Controllers.PlaylistController
{
    using AutoMapper;
    using Moq;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using TestStack.FluentMVCTesting;
    using UpBeat.Data.Models;
    using UpBeat.Services.Contracts;
    using UpBeat.Web.Models;
    using UpBeat = UpBeat.Web.Controllers;

    [TestFixture]
    public class Details_Should
    {
        [Test]
        public void ReturnViewWithModelWithCorrectProperties_WhenThereIsAModelWithThePassedId()
        {
            // Arrange
            var viewModelName = "AlbumDetails";
            var mapperMock = new Mock<IMapper>();
            var albumServiceMock = new Mock<IAlbumService>();
            var userServiceMock = new Mock<IUsersService>();

            var albumModel = new Album()
            {
                Id = 1,
                Artists = new List<Artist>(),
                Name = "Some album"
            };

            var albumViewModel = new AlbumViewModel()
            {
                Id = 1,
                ArtistNames = new List<string>(),
                Name = "Some album"
            };

            albumServiceMock.Setup(x => x.GetById(albumModel.Id)).Returns(albumModel);
            mapperMock.Setup(x => x.Map<AlbumViewModel>(albumModel)).Returns(albumViewModel);

            // Act
            var playlistController =
                new UpBeat.PlaylistController(mapperMock.Object, albumServiceMock.Object, userServiceMock.Object);

            // Act & Assert
            playlistController
                .WithCallTo(b => b.Details(albumModel.Id))
                    .ShouldRenderView(viewModelName)
                    .WithModel<AlbumViewModel>(viewModel =>
                    {
                        Assert.AreEqual(albumViewModel.Id, viewModel.Id);
                        Assert.AreEqual(albumViewModel.ArtistNames, viewModel.ArtistNames);
                    });
        }

    }
}
