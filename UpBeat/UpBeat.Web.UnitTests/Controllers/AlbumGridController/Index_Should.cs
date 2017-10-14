namespace UpBeat.Web.UnitTests.Controllers.AlbumGridController
{
    using NUnit.Framework;
    using Moq;
    using AutoMapper;
    using UpBeat.Services.Contracts;
    using UpBeat.Web.Areas.Administration.Controllers;
    using TestStack.FluentMVCTesting;

    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void ReturnDefaultView_WhenCalled()
        {
            // Arrange 
            var mapperMock = new Mock<IMapper>();
            var albumServiceMock = new Mock<IAlbumService>();

            // Act
            var albumGridController = new AlbumGridController(mapperMock.Object, albumServiceMock.Object);

            // Assert
            albumGridController
                .WithCallTo(b => b.Index())
                    .ShouldRenderDefaultView();
        }
    }
}
