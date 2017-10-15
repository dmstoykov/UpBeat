namespace UpBeat.Web.UnitTests.Controllers.PlaylistController
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NUnit.Framework;
    using UpBeat.Services.Contracts;
    using Moq;
    using AutoMapper;
    using UpBeat.Web.Controllers;
    using TestStack.FluentMVCTesting;

    [TestFixture]
    public class AddToFavourites_Should
    {
        [Test]
        public void CallUserService_WithProvidedAlbumId()
        {
            // Arrange
            var albumId = 3;
            var mapperMock = new Mock<IMapper>();
            var albumService = new Mock<IAlbumService>();
            var userServiceMock = new Mock<IUsersService>();

            // Act
            var playlistController = new PlaylistController(mapperMock.Object, albumService.Object, userServiceMock.Object);
            playlistController.AddToFavourites(albumId);
            // Assert

            userServiceMock.Verify(x => x.AddFavouriteAlbum(albumId), Times.Once);
        }

        [Test]
        public void ReturnStringContent_WhenCalled()
        {
            // Arrange
            var albumId = 3;
            var responseMessage = "Unfavourite";

            var mapperMock = new Mock<IMapper>();
            var albumService = new Mock<IAlbumService>();
            var userServiceMock = new Mock<IUsersService>();

            // Act
            var playlistController = new PlaylistController(mapperMock.Object, albumService.Object, userServiceMock.Object);
            playlistController.AddToFavourites(albumId);
            // Assert

            playlistController
                .WithCallTo(x => x.AddToFavourites(albumId))
                .ShouldReturnContent(content: responseMessage);
        }
    }
}
