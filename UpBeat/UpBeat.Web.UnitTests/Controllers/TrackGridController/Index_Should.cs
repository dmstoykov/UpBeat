namespace UpBeat.Web.UnitTests.Controllers.TrackGridController
{
    using NUnit.Framework;
    using UpBeat.Web.Areas.Administration.Controllers;
    using Moq;
    using AutoMapper;
    using UpBeat.Services.Contracts;
    using TestStack.FluentMVCTesting;

    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void ReturnDefaultView_WhenCalled()
        {
            // Arrange 
            var mapperMock = new Mock<IMapper>();
            var trackServiceMock = new Mock<ITrackService>();

            // Act
            var trackGridController = new TrackGridController(mapperMock.Object, trackServiceMock.Object);

            // Assert
            trackGridController
                .WithCallTo(b => b.Index())
                    .ShouldRenderDefaultView();
        }
    }
}
