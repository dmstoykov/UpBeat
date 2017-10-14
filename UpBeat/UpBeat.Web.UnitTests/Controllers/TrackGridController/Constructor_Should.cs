namespace UpBeat.Web.UnitTests.Controllers.TrackGridController
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NUnit.Framework;
    using UpBeat.Web.Areas.Administration.Controllers;
    using AutoMapper;
    using Moq;
    using UpBeat.Services.Contracts;

    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowException_WhenPassedNullMapper()
        {
            // Arrange & Act
            var errorMessage = Assert.Throws<ArgumentNullException>(() =>
            new TrackGridController(null, null)).Message;

            // Assert
            Assert.IsTrue(errorMessage.Contains("IMapper"));
        }

        [Test]
        public void ThrowException_WhenPassedNullTrackService()
        {
            // Arrange 
            var mapperMock = new Mock<IMapper>();

            // Act
            var errorMessage = Assert.Throws<ArgumentNullException>(() =>
            new TrackGridController(mapperMock.Object, null)).Message;

            // Assert
            Assert.IsTrue(errorMessage.Contains("ITrackService"));
        }

        [Test]
        public void CreateInstance_WhenPassedAllDependencies()
        {
            // Arrange 
            var mapperMock = new Mock<IMapper>();
            var trackServiceMock = new Mock<ITrackService>();

            // Act & Assert
            Assert.DoesNotThrow(() => new TrackGridController(mapperMock.Object, trackServiceMock.Object));
        }
    }
}
