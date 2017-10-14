namespace UpBeat.Web.UnitTests.Controllers.AlbumGridController
{
    using NUnit.Framework;
    using UpBeat.Services.Contracts;
    using Moq;
    using AutoMapper;
    using UpBeat.Web.Areas.Administration.Controllers;
    using UpBeat.Data.Models;
    using UpBeat.Web.Areas.Administration.Models;
    using System.Web.Mvc;

    [TestFixture]
    public class EditAlbum_Should
    {
        [Test]
        public void NotCallAlbumServiceUpdate_WhenPassedNullAlbumGridViewModel()
        {
            // Arrange 
            var mapperMock = new Mock<IMapper>();
            var albumServiceMock = new Mock<IAlbumService>();

            // Act
            var albumGridController = new AlbumGridController(mapperMock.Object, albumServiceMock.Object);
            albumGridController.EditAlbum(null);

            // Assert
            albumServiceMock.Verify(x => x.Update(It.IsAny<Album>()), Times.Never);
        }

        [Test]
        public void CallAlbumServiceUpdate_WhenPassedAlbumGridViewModel()
        {
            // Arrange 
            var mapperMock = new Mock<IMapper>();
            var albumServiceMock = new Mock<IAlbumService>();
            var albumGridViewModel = new AlbumGridViewModel()
            {
                Id = 1,
                Name = "Some album grid model",
                ReleaseDate = "2017-12-02"
            };

            var albumDbModel = new Album()
            {
                Id = 1,
                Name = "Some album grid model",
                ReleaseDate = "2017-12-02"
            };
            mapperMock.Setup(x => x.Map<Album>(albumGridViewModel)).Returns(albumDbModel);

            // Act
            var albumGridController = new AlbumGridController(mapperMock.Object, albumServiceMock.Object);
            albumGridController.EditAlbum(albumGridViewModel);

            // Assert
            albumServiceMock.Verify(x => x.Update(albumDbModel), Times.Once);
        }

        [Test]
        public void ReturnJsonReturnWithViewModel_WhenPassedAlbumGridViewModel()
        {
            // Arrange 
            var mapperMock = new Mock<IMapper>();
            var albumServiceMock = new Mock<IAlbumService>();
            var albumGridViewModel = new AlbumGridViewModel()
            {
                Id = 1,
                Name = "Some album grid model",
                ReleaseDate = "2017-12-02"
            };

            var albumDbModel = new Album()
            {
                Id = 1,
                Name = "Some album grid model",
                ReleaseDate = "2017-12-02"
            };
            mapperMock.Setup(x => x.Map<Album>(albumGridViewModel)).Returns(albumDbModel);

            // Act
            var albumGridController = new AlbumGridController(mapperMock.Object, albumServiceMock.Object);
            var jsonResult = albumGridController.EditAlbum(albumGridViewModel) as JsonResult;
            var jsonResultModel = jsonResult.Data.GetType().GetProperty("albumViewModel").GetValue(jsonResult.Data, null);

            // Assert
            Assert.AreSame(jsonResultModel, albumGridViewModel);
        }
    }
}
