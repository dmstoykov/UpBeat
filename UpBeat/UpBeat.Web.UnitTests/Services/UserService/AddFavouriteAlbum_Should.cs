namespace UpBeat.Web.UnitTests.Services.UserService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;
    using UpBeat.Services.Contracts;
    using UpBeat.Data.Models;
    using UpBeat.Data.Contracts;
    using Moq;
    using UpBeat.Services;
    using System.Web;
    using System.Security.Principal;
    using System.IO;

    [TestFixture]
    public class AddFavouriteAlbum_Should
    {
        [Test]
        public void ThrowArgumentException_WhenPassedNullAlbumId()
        {
            // Arrange
            var userRepositoryMock = new Mock<IGenericRepository<User>>();
            var albumServiceMock = new Mock<IAlbumService>();

            // Act
            var userService = new UserService(userRepositoryMock.Object, albumServiceMock.Object);

            // Assert
            Assert.Throws<ArgumentException>(() => userService.AddFavouriteAlbum(0));
        }

        [Test]
        public void NotAddAlbumToFavourites_WhenUserAlreadyHasAlbumInFavourites()
        {
            // Arrange
            var albumId = 3;
            var loggedInUserUsername = "test@gmail.com";
            var userRepositoryMock = new Mock<IGenericRepository<User>>();
            var albumServiceMock = new Mock<IAlbumService>();
            HttpContext.Current = new HttpContext(
                new HttpRequest("", "http://tempuri.org", ""),
                new HttpResponse(new StringWriter())
                );

            HttpContext.Current.User = new GenericPrincipal(
                new GenericIdentity(loggedInUserUsername),
                new string[0]);

            var albumDbModel = new Album()
            {
                Id = 3,
                Name = "Some album"
            };
            var userDbModel = new User()
            {
                FirstName = "John",
                LastName = "Doe",
                Email = loggedInUserUsername,
                UserName = loggedInUserUsername,
                FavouriteAlbums = new List<Album>() { albumDbModel }
            };

            var usersList = new List<User>() { userDbModel }.AsQueryable();

            albumServiceMock.Setup(x => x.GetById(albumId)).Returns(albumDbModel);
            userRepositoryMock.Setup(x => x.All).Returns(usersList);

            // Act
            var userService = new UserService(userRepositoryMock.Object, albumServiceMock.Object);
            userService.AddFavouriteAlbum(albumId);

            // Assert
            userRepositoryMock.Verify(x => x.Update(userDbModel), Times.Never);
        }

        [Test]
        public void AddAlbumToFavourites_WhenUserHasNotAlbumInFavourites()
        {
            // Arrange
            var albumId = 3;
            var loggedInUserUsername = "test@gmail.com";
            var userRepositoryMock = new Mock<IGenericRepository<User>>();
            var albumServiceMock = new Mock<IAlbumService>();
            HttpContext.Current = new HttpContext(
                new HttpRequest("", "http://tempuri.org", ""),
                new HttpResponse(new StringWriter())
                );

            HttpContext.Current.User = new GenericPrincipal(
                new GenericIdentity(loggedInUserUsername),
                new string[0]);

            var albumDbModel = new Album()
            {
                Id = 3,
                Name = "Some album"
            };
            var userDbModel = new User()
            {
                FirstName = "John",
                LastName = "Doe",
                Email = loggedInUserUsername,
                UserName = loggedInUserUsername,
                FavouriteAlbums = new List<Album>() { }
            };

            var usersList = new List<User>() { userDbModel }.AsQueryable();

            albumServiceMock.Setup(x => x.GetById(albumId)).Returns(albumDbModel);
            userRepositoryMock.Setup(x => x.All).Returns(usersList);

            // Act
            var userService = new UserService(userRepositoryMock.Object, albumServiceMock.Object);
            userService.AddFavouriteAlbum(albumId);

            // Assert
            userRepositoryMock.Verify(x => x.Update(userDbModel), Times.Once);
        }
    }
}
