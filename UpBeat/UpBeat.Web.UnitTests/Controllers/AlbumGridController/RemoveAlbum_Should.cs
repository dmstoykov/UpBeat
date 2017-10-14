namespace UpBeat.Web.UnitTests.Controllers.AlbumGridController
{
    using NUnit.Framework;
    using AutoMapper;
    using Moq;
    using UpBeat.Services.Contracts;
    using UpBeat.Web.Areas.Administration.Controllers;
    using UpBeat.Web.Areas.Administration.Models;
    using UpBeat.Data.Models;

    [TestFixture]
    public class RemoveAlbum_Should
    {
        [Test]
        public void NotCallAlbumServiceRemove_WhenPassedNullAlbumGridViewModel()
        {
            // Arrange 
            var mapperMock = new Mock<IMapper>();
            var albumServiceMock = new Mock<IAlbumService>();

            // Act
            var albumGridController = new AlbumGridController(mapperMock.Object, albumServiceMock.Object);
            albumGridController.RemoveAlbum(null);

            // Assert
            albumServiceMock.Verify(x => x.Remove(It.IsAny<Album>()), Times.Never);
        }

        [Test]
        public void CallAlbumServiceRemove_WhenPassedAlbumGridViewModel()
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
            albumGridController.RemoveAlbum(albumGridViewModel);

            // Assert
            albumServiceMock.Verify(x => x.Remove(albumDbModel), Times.Once);
        }
    }
}
