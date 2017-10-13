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
    public class Update_Should
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
            Assert.Throws<ArgumentNullException>(() => albumService.Update(null));
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
            Assert.Throws<ArgumentException>(() => albumService.Update(albumModel));
        }

        [Test]
        public void UpdateAlbum_WhenPassedCorrectParameters()
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
            albumRepositoryMock.Setup(x => x.Update(albumModel)).Callback(() =>
            {
                // Making new setup because queryable cannot be altered ( updating items )
                albumModel.Name = "Sample album updated";

                albumRepositoryMock.Setup(x => x.All).Returns(new List<Album>() {albumModel }.AsQueryable());
            });

            // Act
            var albumService = new AlbumService(albumRepositoryMock.Object, artistRepositoryMock.Object);
            albumService.Update(albumModel);

            // Assert
            Assert.IsTrue(albumService.Data.All.FirstOrDefault().Name.Contains("updated"));
        }
    }
}
