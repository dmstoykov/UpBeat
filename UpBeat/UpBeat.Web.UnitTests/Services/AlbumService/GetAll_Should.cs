namespace UpBeat.Web.UnitTests.Services.AlbumService
{
    using System.Collections.Generic;
    using System.Linq;
    using Moq;
    using NUnit.Framework;
    using UpBeat.Data.Contracts;
    using UpBeat.Data.Models;
    using UpBeat.Services;

    [TestFixture]
    public class GetAll_Should
    {
        [Test]
        public void ReturnAlbumsCollection_WhenCalled()
        {
            // Arrange
            var albumRepositoryMock = new Mock<IGenericRepository<Album>>();
            var artistRepositoryMock = new Mock<IGenericRepository<Artist>>();
            var albumModel = new List<Album>()
            {
                new Album()
                {
                    Id = 1,
                    Name = "Sample album 1",
                    Images = new List<Image>()
                }
            }.AsQueryable();

            albumRepositoryMock.Setup(x => x.All).Returns(albumModel);

            // Act
            var albumService = new AlbumService(albumRepositoryMock.Object, artistRepositoryMock.Object);
            var result = albumService.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(albumModel.Count(), result.Count());
        }

        [Test]
        public void ReturnEmptyCollection_WhenNoAlbumsInRepository()
        {
            // Arrange
            var albumRepositoryMock = new Mock<IGenericRepository<Album>>();
            var artistRepositoryMock = new Mock<IGenericRepository<Artist>>();
            var albumModel = new List<Album>() { }.AsQueryable();

            albumRepositoryMock.Setup(x => x.All).Returns(albumModel);

            // Act
            var albumService = new AlbumService(albumRepositoryMock.Object, artistRepositoryMock.Object);
            var result = albumService.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }
    }
}
