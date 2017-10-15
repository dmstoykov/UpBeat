namespace UpBeat.Web.UnitTests.Controllers.PlaylistController
{
    using AutoMapper;
    using Moq;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;
    using UpBeat.Services.Contracts;
    using UpBeat = UpBeat.Web.Controllers;


    public class Index_Should
    {
        [Test]
        public void ReturnDefaultView_WhenCalled()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            var albumServiceMock = new Mock<IAlbumService>();
            var userServiceMock = new Mock<IUsersService>();

            // Act
            var playlistController =
                new UpBeat.PlaylistController(mapperMock.Object, albumServiceMock.Object, userServiceMock.Object);

            // Act & Assert
            playlistController
                .WithCallTo(b => b.Index())
                    .ShouldRenderDefaultView();
        }
    }
}
