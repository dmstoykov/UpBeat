namespace UpBeat.Web.UnitTests.Services.AlbumService
{
    using System.Collections.Generic;
    using NUnit.Framework;
    using UpBeat.Services;
    using Moq;
    using UpBeat.Data.Models;
    using UpBeat.Data.Contracts;

    [TestFixture]
    public class GetById_Should
    {
        [Test]
        public void ReturnAlbum_WhenPassedCorrectId()
        {
            // Arrange
            var albumId = 1;
            var albumRepositoryMock = new Mock<IGenericRepository<Album>>();
            var albumModel = new Album()
            {
                Id = 1,
                Name = "Sample album 1",
                Images = new List<Image>()
                {
                    new Image()
                    {
                        Width = 60,
                        Height = 60,
                        Url = "https://google.com/images.jpg"
                    }
                }
            };

            albumRepositoryMock.Setup(x => x.Get(albumModel.Id)).Returns(albumModel);

            // Act
            var albumService = new AlbumService(albumRepositoryMock.Object);
            var result = albumService.GetById(albumId);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ReturnAlbumWithId_WhenPassedCorrectId()
        {
            // Arrange
            var albumId = 1;
            var albumRepositoryMock = new Mock<IGenericRepository<Album>>();
            var albumModel = new Album()
            {
                Id = 1,
                Name = "Sample album 1",
                Images = new List<Image>()
                {
                    new Image()
                    {
                        Width = 60,
                        Height = 60,
                        Url = "https://google.com/images.jpg"
                    }
                }
            };

            albumRepositoryMock.Setup(x => x.Get(albumModel.Id)).Returns(albumModel);

            // Act
            var albumService = new AlbumService(albumRepositoryMock.Object);
            var result = albumService.GetById(albumId);

            // Assert
            Assert.AreEqual(albumModel.Id, albumModel.Id);
        }

        [Test]
        public void ReturnNull_WhenPassedNonExistantId()
        {
            // Arrange
            var albumId = 100;
            var albumRepositoryMock = new Mock<IGenericRepository<Album>>();
            var albumModel = new Album()
            {
                Id = 1,
                Name = "Sample album 1",
                Images = new List<Image>()
                {
                    new Image()
                    {
                        Width = 60,
                        Height = 60,
                        Url = "https://google.com/images.jpg"
                    }
                }
            };

            albumRepositoryMock.Setup(x => x.Get(albumModel.Id)).Returns(albumModel);

            // Act
            var albumService = new AlbumService(albumRepositoryMock.Object);
            var result = albumService.GetById(albumId);

            // Assert
            Assert.IsNull(result);
        }
    }
}
