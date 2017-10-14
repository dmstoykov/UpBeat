namespace UpBeat.Web.UnitTests.Controllers.AdminPanelController
{
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;
    using AutoMapper;
    using Moq;
    using UpBeat.Services.Contracts;
    using UpBeat.Web.Areas.Administration.Controllers;
    using TestStack.FluentMVCTesting;
    using UpBeat.Data.Models;
    using UpBeat.Web.Areas.Administration.Models;

    [TestFixture]
    public class AddTrack_Should
    {
        [Test]
        public void RenderAddTrackPartialView_WhenCalled()
        {
            // Arrange 
            var actionResultViewName = "_AddTrackPartial";
            var mapperMock = new Mock<IMapper>();
            var albumServiceMock = new Mock<IAlbumService>();
            var trackServiceMock = new Mock<ITrackService>();
            var artistServiceMock = new Mock<IArtistService>();

            // Act
            var adminPanelController = new AdminPanelController(mapperMock.Object, albumServiceMock.Object,
            trackServiceMock.Object, artistServiceMock.Object);

            // Assert
            adminPanelController
                .WithCallTo(b => b.AddTrack())
                    .ShouldRenderPartialView(actionResultViewName);
        }

        [Test]
        public void RenderAddTrackPartialViewWithModelContainingSelectList_WhenCalled()
        {
            // Arrange 
            var actionResultViewName = "_AddTrackPartial";
            var mapperMock = new Mock<IMapper>();
            var albumServiceMock = new Mock<IAlbumService>();
            var trackServiceMock = new Mock<ITrackService>();
            var artistServiceMock = new Mock<IArtistService>();
            var albumModel = new Album()
            {
                Id = 1,
                Name = "Some album"
            };

            var albumsList = new List<Album>() { albumModel };
            albumServiceMock.Setup(x => x.GetAll()).Returns(albumsList);

            // Act
            var adminPanelController = new AdminPanelController(mapperMock.Object, albumServiceMock.Object,
            trackServiceMock.Object, artistServiceMock.Object);

            // Assert
            adminPanelController
                .WithCallTo(b => b.AddTrack())
                    .ShouldRenderPartialView(actionResultViewName)
                    .WithModel<TrackViewModel>(viewModel =>
                    {
                        Assert.AreEqual(viewModel.AlbumSelectList.FirstOrDefault().Text, albumModel.Name);
                    });
        }

        [Test]
        public void RenderAddTrackPartialViewWithModelContainingEmptySelectList_WhenNoAlbumsInDatabase()
        {
            // Arrange 
            var actionResultViewName = "_AddTrackPartial";
            var mapperMock = new Mock<IMapper>();
            var albumServiceMock = new Mock<IAlbumService>();
            var trackServiceMock = new Mock<ITrackService>();
            var artistServiceMock = new Mock<IArtistService>();

            var albumsList = new List<Album>() { };
            albumServiceMock.Setup(x => x.GetAll()).Returns(albumsList);

            // Act
            var adminPanelController = new AdminPanelController(mapperMock.Object, albumServiceMock.Object,
            trackServiceMock.Object, artistServiceMock.Object);

            // Assert
            adminPanelController
                .WithCallTo(b => b.AddTrack())
                    .ShouldRenderPartialView(actionResultViewName)
                    .WithModel<TrackViewModel>(viewModel =>
                    {
                        Assert.AreEqual(viewModel.AlbumSelectList.Count(), 0);
                    });
        }
    }
}
