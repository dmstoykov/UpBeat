namespace UpBeat.Web.UnitTests.Controllers.AlbumGridController
{
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;
    using AutoMapper;
    using Moq;
    using UpBeat.Services.Contracts;
    using UpBeat.Web.Areas.Administration.Controllers;
    using UpBeat.Data.Models;
    using UpBeat.Web.Areas.Administration.Models;
    using Kendo.Mvc.UI;
    using System.Web.Mvc;

    [TestFixture]
    public class ListAlbums_Should
    {
        [Test]
        public void ReturnJsonContainingAlbums_WhenCalled()
        {
            // Arrange 
            var mapperMock = new Mock<IMapper>();
            var albumServiceMock = new Mock<IAlbumService>();
            var dataSourceRequest = new DataSourceRequest();

            var albumModel = new Album()
            {
                Id = 1,
                Name = "Sample album"
            };

            var albumGridViewModel = new AlbumGridViewModel()
            {
                Id = 1,
                Name = "Sample album"
            };

            var albumsList = new List<Album>() { albumModel };
            albumServiceMock.Setup(x => x.GetAll()).Returns(albumsList);

            mapperMock.Setup(x => x.Map<Album, AlbumGridViewModel>(albumModel)).Returns(albumGridViewModel);

            // Act
            var albumGridController = new AlbumGridController(mapperMock.Object, albumServiceMock.Object);
            var jsonResult = albumGridController.ListAlbums(dataSourceRequest) as JsonResult;
            var dataSourceResult = (jsonResult.Data as DataSourceResult).Data.GetEnumerator();
            dataSourceResult.MoveNext();

            // Assert
            Assert.AreSame(dataSourceResult.Current, albumGridViewModel);
        }

        [Test]
        public void ReturnJsonWithNoAlbums_WhenDatabaseIsEmpty()
        {
            // Arrange 
            var mapperMock = new Mock<IMapper>();
            var albumServiceMock = new Mock<IAlbumService>();
            var dataSourceRequest = new DataSourceRequest();

            var albumModel = new Album()
            {
                Id = 1,
                Name = "Sample album"
            };

            var albumGridViewModel = new AlbumGridViewModel()
            {
                Id = 1,
                Name = "Sample album"
            };

            var albumsList = new List<Album>() { };
            albumServiceMock.Setup(x => x.GetAll()).Returns(albumsList);

            mapperMock.Setup(x => x.Map<Album, AlbumGridViewModel>(albumModel)).Returns(albumGridViewModel);

            // Act
            var albumGridController = new AlbumGridController(mapperMock.Object, albumServiceMock.Object);
            var jsonResult = albumGridController.ListAlbums(dataSourceRequest) as JsonResult;
            var dataSourceResult = (jsonResult.Data as DataSourceResult).Data.Cast<object>().ToList();

            // Assert
            Assert.AreEqual(dataSourceResult.Count, 0);
        }
    }
}
