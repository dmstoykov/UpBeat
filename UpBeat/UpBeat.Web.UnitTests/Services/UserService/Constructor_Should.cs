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
    using UpBeat.Data.Models;
    using UpBeat.Services.Contracts;
    using UpBeat.Services;

    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPassedNullAlbumService()
        {
            // Arrange
            var expectedMessage = "IAlbumService";
            var userRepositoryMock = new Mock<IGenericRepository<User>>();

            // Act
            var errorMessage = Assert.Throws<ArgumentNullException>(() =>
            new UserService(userRepositoryMock.Object, null)).Message;

            // Assert
            Assert.IsTrue(errorMessage.Contains(expectedMessage));
        }

        [Test]
        public void ReturnInstanse_WhenPassedAllDependencies()
        {
            // Arrange
            var userRepositoryMock = new Mock<IGenericRepository<User>>();
            var albumServiceMock = new Mock<IAlbumService>();

            // Act
            var result = new UserService(userRepositoryMock.Object, albumServiceMock.Object);

            // Assert
            Assert.IsInstanceOf<IUsersService>(result);
        }
    }
}
