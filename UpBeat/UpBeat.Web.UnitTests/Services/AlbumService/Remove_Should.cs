namespace UpBeat.Web.UnitTests.Services.AlbumService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NUnit.Framework;
    using UpBeat.Data.Contracts;
    using Moq;
    using UpBeat.Data.Models;
    using UpBeat.Services;

    [TestFixture]
    public class Remove_Should
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
            Assert.Throws<ArgumentNullException>(() => albumService.Remove(null));
        }

        [Test]
        public void ThrowException_WhenPassedNonExistingAlbum()
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

            albumRepositoryMock.Setup(x => x.All).Returns(albumsList);

            // Act
            var albumService = new AlbumService(albumRepositoryMock.Object, artistRepositoryMock.Object);

            // Assert
            Assert.Throws<ArgumentException>(() => albumService.Remove(albumModel));
        }

        [Test]
        public void RemoveAlbum_WhenPassedCorrectParameters()
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
            albumRepositoryMock.Setup(x => x.Remove(albumModel)).Callback(() =>
            {
                // Making new setup because queryable cannot be altered ( removing items )
                albumRepositoryMock.Setup(x => x.All).Returns(new List<Album>() { }.AsQueryable());
            });

            // Act
            var albumService = new AlbumService(albumRepositoryMock.Object, artistRepositoryMock.Object);
            albumService.Remove(albumModel);

            // Assert
            Assert.IsNull(albumService.Data.All.FirstOrDefault());
        }
    }
}
