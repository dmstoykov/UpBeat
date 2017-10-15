namespace UpBeat.Web.UnitTests.Services.UserService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NUnit.Framework;
    using UpBeat.Data.Contracts;
    using Moq;
    using UpBeat.Services.Contracts;
    using UpBeat.Services;
    using UpBeat.Data.Models;

    [TestFixture]
    public class GetByUsername_Should
    {
        [Test]
        public void ReturnNull_WhenPassedNonExistantUsername()
        {
            // Arrange
            var userUsername = "test@gmail.com";
            var userRepositoryMock = new Mock<IGenericRepository<User>>();
            var albumServiceMock = new Mock<IAlbumService>();
            var userDbModel = new User()
            {
                FirstName = "John",
                LastName = "Doe",
                Email = userUsername,
                UserName = userUsername,
            };

            var usersList = new List<User>() { userDbModel }.AsQueryable();
            userRepositoryMock.Setup(x => x.All).Returns(usersList);

            // Act
            var userService = new UserService(userRepositoryMock.Object, albumServiceMock.Object);
            var result = userService.GetByUsername(userUsername + "whoops");
            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void ThrowArgumentException_WhenPassedEmptyString()
        {
            // Arrange
            var userRepositoryMock = new Mock<IGenericRepository<User>>();
            var albumServiceMock = new Mock<IAlbumService>();

            // Act
            var userService = new UserService(userRepositoryMock.Object, albumServiceMock.Object);

            // Assert
            Assert.Throws<ArgumentException>(() => userService.GetByUsername(string.Empty));
        }

        [Test]
        public void ReturnUser_WhenPassedCorrectUsername()
        {
            // Arrange
            var userUsername = "test@gmail.com";
            var userRepositoryMock = new Mock<IGenericRepository<User>>();
            var albumServiceMock = new Mock<IAlbumService>();
            var userDbModel = new User()
            {
                FirstName = "John",
                LastName = "Doe",
                Email = userUsername,
                UserName = userUsername,
            };

            var usersList = new List<User>() { userDbModel }.AsQueryable();
            userRepositoryMock.Setup(x => x.All).Returns(usersList);

            // Act
            var userService = new UserService(userRepositoryMock.Object, albumServiceMock.Object);
            var result = userService.GetByUsername(userUsername);
            // Assert
            Assert.IsInstanceOf<User>(result);
        }
    }
}
