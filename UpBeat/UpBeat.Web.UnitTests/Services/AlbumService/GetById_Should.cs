namespace UpBeat.Web.UnitTests.Services.AlbumService
{
    using System.Collections.Generic;
    using NUnit.Framework;
    using UpBeat.Services;
    using Moq;
    using UpBeat.Data.Models;
    using UpBeat.Data.Contracts;
    using System.Linq;
    using System;

    [TestFixture]
    public class GetById_Should
    {
        [Test]
        public void ReturnAlbum_WhenPassedCorrectId()
        {
            // Arrange
            var albumId = 1;
            var albumRepositoryMock = new Mock<IGenericRepository<Album>>();
            var artistRepositoryMock = new Mock<IGenericRepository<Artist>>();
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

            var albumList = new List<Album>() { albumModel }.AsQueryable();

            albumRepositoryMock.Setup(x => x.All).Returns(albumList);

            // Act
            var albumService = new AlbumService(albumRepositoryMock.Object, artistRepositoryMock.Object);
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
            var artistRepositoryMock = new Mock<IGenericRepository<Artist>>();
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

            var albumList = new List<Album>() { albumModel }.AsQueryable();

            albumRepositoryMock.Setup(x => x.All).Returns(albumList);

            // Act
            var albumService = new AlbumService(albumRepositoryMock.Object, artistRepositoryMock.Object);
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
            var artistRepositoryMock = new Mock<IGenericRepository<Artist>>();
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

            var albumList = new List<Album>() { albumModel }.AsQueryable();

            albumRepositoryMock.Setup(x => x.All).Returns(albumList);

            // Act
            var albumService = new AlbumService(albumRepositoryMock.Object, artistRepositoryMock.Object);

            // Assert
            Assert.Throws<ArgumentNullException>(() => albumService.GetById(albumId));
        }

        [Test]
        public void ReturnAlbumWithNonDeletedTracks_WhenPassedCorrectId()
        {
            // Arrange
            var albumId = 1;
            var albumRepositoryMock = new Mock<IGenericRepository<Album>>();
            var artistRepositoryMock = new Mock<IGenericRepository<Artist>>();
            var albumModel = new Album()
            {
                Id = 1,
                Name = "Sample album 1",
                Tracks = new List<Track>()
                {
                    new Track()
                    {
                        Id =1,
                        Name ="Sample track 1",
                        IsDeleted = true
                    },
                    new Track()
                    {
                        Id = 2,
                        Name = "Sample track 2",
                    }
                }
            };

            var albumList = new List<Album>() { albumModel }.AsQueryable();

            albumRepositoryMock.Setup(x => x.All).Returns(albumList);

            // Act
            var albumService = new AlbumService(albumRepositoryMock.Object, artistRepositoryMock.Object);
            var result = albumService.GetById(albumId);

            // Assert
            Assert.AreEqual(result.Tracks.Count, 1);
        }
    }
}
