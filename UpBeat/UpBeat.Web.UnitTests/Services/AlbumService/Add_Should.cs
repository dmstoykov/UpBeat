namespace UpBeat.Web.UnitTests.Services.AlbumService
{
    using Moq;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using UpBeat.Data.Contracts;
    using UpBeat.Data.Models;
    using UpBeat.Services;

    [TestFixture]
    public class Add_Should
    {
        [Test]
        public void ThrowException_WhenPassedNullAlbum()
        {
            // Arrange
            var albumRepositoryMock = new Mock<IGenericRepository<Album>>();
            var artistRepositoryMock = new Mock<IGenericRepository<Artist>>();

            // Act
            var albumService = new AlbumService(albumRepositoryMock.Object, artistRepositoryMock.Object);

            // Assert
            Assert.Throws<ArgumentNullException>(() => albumService.Add(null, "Sample text"));
        }

        [Test]
        public void ThrowException_WhenPassedEmptyArtistName()
        {
            // Arrange
            var albumRepositoryMock = new Mock<IGenericRepository<Album>>();
            var artistRepositoryMock = new Mock<IGenericRepository<Artist>>();
            var albumModel = new Album()
            {
                Id = 1,
                Name = "Sample album 1",
                Images = new List<Image>()
            };

            // Act
            var albumService = new AlbumService(albumRepositoryMock.Object, artistRepositoryMock.Object);

            // Assert
            Assert.Throws<ArgumentException>(() => albumService.Add(albumModel, ""));
        }

        [Test]
        public void ThrowException_WhenPassedExistingAlbum()
        {
            // Arrange
            var albumRepositoryMock = new Mock<IGenericRepository<Album>>();
            var artistRepositoryMock = new Mock<IGenericRepository<Artist>>();
            var albumModel = new Album()
            {
                Id = 1,
                Name = "Sample album 1",
                Images = new List<Image>()
            };

            var albumsList = new List<Album>() { albumModel }.AsQueryable();

            albumRepositoryMock.Setup(x => x.All).Returns(albumsList);

            // Act
            var albumService = new AlbumService(albumRepositoryMock.Object, artistRepositoryMock.Object);

            // Assert
            Assert.Throws<ArgumentException>(() => albumService.Add(albumModel, "Sample text"));
        }

        [Test]
        public void ThrowException_WhenPassedNonExistingArtist()
        {
            // Arrange
            var albumRepositoryMock = new Mock<IGenericRepository<Album>>();
            var artistRepositoryMock = new Mock<IGenericRepository<Artist>>();
            var albumModel = new Album()
            {
                Id = 1,
                Name = "Sample album 1",
                Images = new List<Image>()
            };

            var albumsList = new List<Album>() { }.AsQueryable();

            artistRepositoryMock.Setup(x => x.All).Returns(new List<Artist>().AsQueryable());
            albumRepositoryMock.Setup(x => x.All).Returns(albumsList);

            // Act
            var albumService = new AlbumService(albumRepositoryMock.Object, artistRepositoryMock.Object);

            // Assert
            Assert.Throws<ArgumentNullException>(() => albumService.Add(albumModel, "John Doe"));
        }

        [Test]
        public void AddNewAlbum_WhenPassedCorrectParameters()
        {
            // Arrange
            var albumRepositoryMock = new Mock<IGenericRepository<Album>>();
            var artistRepositoryMock = new Mock<IGenericRepository<Artist>>();
            var albumModel = new Album()
            {
                Id = 1,
                Name = "Sample album 1",
                Images = new List<Image>()
            };

            var artistModel = new Artist()
            {
                Id = 1,
                Name = "John Doe",
                Albums = new List<Album> { albumModel }
            };

            var albumsList = new List<Album>() { }.AsQueryable();
            var artistList = new List<Artist>() { artistModel }.AsQueryable();

            artistRepositoryMock.Setup(x => x.All).Returns(artistList);
            albumRepositoryMock.Setup(x => x.All).Returns(albumsList);
            albumRepositoryMock.Setup(x => x.Add(albumModel)).Callback(() =>
            {
                // Making new setup because queryable cannot be altered ( adding items )
                albumRepositoryMock.Setup(x => x.All).Returns(new List<Album>() { albumModel }.AsQueryable());
            });

            // Act
            var albumService = new AlbumService(albumRepositoryMock.Object, artistRepositoryMock.Object);
            albumService.Add(albumModel, "John Doe");

            // Assert
            Assert.AreEqual(albumService.Data.All.FirstOrDefault().Name, albumModel.Name);
        }
    }
}
