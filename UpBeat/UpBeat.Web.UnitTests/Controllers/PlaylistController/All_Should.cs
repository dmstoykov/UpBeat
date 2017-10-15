namespace UpBeat.Web.UnitTests.Controllers.PlaylistController
{
    using System.Collections.Generic;
    using AutoMapper;
    using Moq;
    using NUnit.Framework;
    using UpBeat.Services.Contracts;
    using UpBeat.Data.Models;
    using UpBeat.Web.Models;
    using UpBeat = UpBeat.Web.Controllers;
    using TestStack.FluentMVCTesting;
    using System.Linq;

    [TestFixture]
    public class All_Should
    {
        [Test]
        public void ReturnViewWithModelWithCorrectProperties_WhenCalled()
        {
            // Arrange
            var resultViewName = "Index";
            var mapperMock = new Mock<IMapper>();
            var albumServiceMock = new Mock<IAlbumService>();
            var userServiceMock = new Mock<IUsersService>();

            var albumModel = new Album()
            {
                Id = 1,
                Artists = new List<Artist>(),
                Name = "Some album"
            };

            var playlistModel = new AlbumViewModel()
            {
                Id = 1,
                ArtistNames = new List<string>(),
                Name = "Some album"
            };

            albumServiceMock.Setup(x => x.GetAll()).Returns(new List<Album>() { albumModel });
            mapperMock.Setup(x => x.Map<AlbumViewModel>(albumModel)).Returns(playlistModel);

            // Act
            var playlistController =
                new UpBeat.PlaylistController(mapperMock.Object, albumServiceMock.Object, userServiceMock.Object);

            // Act & Assert
            playlistController
                .WithCallTo(b => b.All(1))
                    .ShouldRenderView(resultViewName)
                    .WithModel<PlaylistViewModel>(viewModel =>
                    {
                        Assert.AreEqual(playlistModel.Id, viewModel.Albums.OfType<AlbumViewModel>().FirstOrDefault().Id);
                        Assert.AreEqual(playlistModel.ArtistNames, viewModel.Albums.OfType<AlbumViewModel>().FirstOrDefault().ArtistNames);
                    });
        }

        [Test]
        public void ReturnViewWithModelWithAlbums_WhenCalledWithNullPageNumber()
        {
            // Arrange
            var resultViewName = "Index";
            var mapperMock = new Mock<IMapper>();
            var albumServiceMock = new Mock<IAlbumService>();
            var userServiceMock = new Mock<IUsersService>();

            var albumModel = new Album()
            {
                Id = 1,
                Artists = new List<Artist>(),
                Name = "Some album"
            };

            var playlistModel = new AlbumViewModel()
            {
                Id = 1,
                ArtistNames = new List<string>(),
                Name = "Some album"
            };

            albumServiceMock.Setup(x => x.GetAll()).Returns(new List<Album>() { albumModel });
            mapperMock.Setup(x => x.Map<AlbumViewModel>(albumModel)).Returns(playlistModel);

            // Act
            var playlistController =
                new UpBeat.PlaylistController(mapperMock.Object, albumServiceMock.Object, userServiceMock.Object);

            // Act & Assert
            playlistController
                .WithCallTo(b => b.All(null))
                    .ShouldRenderView(resultViewName)
                    .WithModel<PlaylistViewModel>(viewModel =>
                    {
                        Assert.AreEqual(playlistModel.Id, viewModel.Albums.OfType<AlbumViewModel>().FirstOrDefault().Id);
                        Assert.AreEqual(playlistModel.ArtistNames, viewModel.Albums.OfType<AlbumViewModel>().FirstOrDefault().ArtistNames);
                    });
        }
    }
}
