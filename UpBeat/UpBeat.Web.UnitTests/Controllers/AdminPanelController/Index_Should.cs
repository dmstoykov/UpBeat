namespace UpBeat.Web.UnitTests.Controllers.AdminPanelController
{
    using AutoMapper;
    using Moq;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;
    using UpBeat.Services.Contracts;
    using UpBeat.Web.Areas.Administration.Controllers;

    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void ReturnDefaultView_WhenCalled()
        {
            // Arrange 
            var mapperMock = new Mock<IMapper>();
            var albumServiceMock = new Mock<IAlbumService>();
            var trackServiceMock = new Mock<ITrackService>();
            var artistServiceMock = new Mock<IArtistService>();

            // Act
            var adminPanelController = new AdminPanelController(mapperMock.Object, albumServiceMock.Object,
            trackServiceMock.Object, artistServiceMock.Object);

            // Act & Assert
            adminPanelController
                .WithCallTo(b => b.Index())
                    .ShouldRenderDefaultView();
        }
    }
}
