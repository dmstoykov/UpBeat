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
    using UpBeat.Web.Areas.Administration.Models;
    using UpBeat.Data.Models;

    [TestFixture]
    public class AddAlbum_Should
    {
        [Test]
        public void RenderAddAlbumPartialView_WhenCalled()
        {
            // Arrange 
            var actionResultViewName = "_AddAlbumPartial";
            var mapperMock = new Mock<IMapper>();
            var albumServiceMock = new Mock<IAlbumService>();
            var trackServiceMock = new Mock<ITrackService>();
            var artistServiceMock = new Mock<IArtistService>();

            // Act
            var adminPanelController = new AdminPanelController(mapperMock.Object, albumServiceMock.Object,
            trackServiceMock.Object, artistServiceMock.Object);

            // Assert
            adminPanelController
                .WithCallTo(b => b.AddAlbum())
                    .ShouldRenderPartialView(actionResultViewName);
        }

        [Test]
        public void RenderAddAlbumPartialViewWithModelContainingSelectList_WhenCalled()
        {
            // Arrange 
            var actionResultViewName = "_AddAlbumPartial";
            var mapperMock = new Mock<IMapper>();
            var albumServiceMock = new Mock<IAlbumService>();
            var trackServiceMock = new Mock<ITrackService>();
            var artistServiceMock = new Mock<IArtistService>();
            var artistModel = new Artist()
            {
                Id = 1,
                Name = "John Doe"
            };

            var artistsList = new List<Artist>() { artistModel };
            artistServiceMock.Setup(x => x.GetAll()).Returns(artistsList);

            // Act
            var adminPanelController = new AdminPanelController(mapperMock.Object, albumServiceMock.Object,
            trackServiceMock.Object, artistServiceMock.Object);

            // Assert
            adminPanelController
                .WithCallTo(b => b.AddAlbum())
                    .ShouldRenderPartialView(actionResultViewName)
                    .WithModel<AlbumViewModel>(viewModel =>
                    {
                        Assert.AreEqual(viewModel.ArtistSelectList.FirstOrDefault().Text, artistModel.Name);
                    });
        }

        [Test]
        public void RenderAddAlbumPartialViewWithModelContainingEmptySelectList_WhenNoArtistsInDatabase()
        {
            // Arrange 
            var actionResultViewName = "_AddAlbumPartial";
            var mapperMock = new Mock<IMapper>();
            var albumServiceMock = new Mock<IAlbumService>();
            var trackServiceMock = new Mock<ITrackService>();
            var artistServiceMock = new Mock<IArtistService>();

            var artistsList = new List<Artist>() { };
            artistServiceMock.Setup(x => x.GetAll()).Returns(artistsList);

            // Act
            var adminPanelController = new AdminPanelController(mapperMock.Object, albumServiceMock.Object,
            trackServiceMock.Object, artistServiceMock.Object);

            // Assert
            adminPanelController
                .WithCallTo(b => b.AddAlbum())
                    .ShouldRenderPartialView(actionResultViewName)
                    .WithModel<AlbumViewModel>(viewModel =>
                    {
                        Assert.AreEqual(viewModel.ArtistSelectList.Count(), 0);
                    });
        }
    }
}
