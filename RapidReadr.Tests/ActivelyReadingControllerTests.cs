using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RapidReadr.Server.Controllers;
using RapidReadr.Server.Models;
using RapidReadr.Server.Repository.Interfaces;
using RapidReadr.Server.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RapidReadr.Tests
{
    [TestFixture]
    public class ActivelyReadingControllerTests
    {
        private Mock<IActivelyReadingRepository> _mockRepository;
        private ActivelyReadingService _mockService;
        private ActivelyReadingController _controller;

        [SetUp]
        public void Setup()
        {
            // Mock the repository
            _mockRepository = new Mock<IActivelyReadingRepository>();

            // Create the service with the mocked repository
            _mockService = new ActivelyReadingService(_mockRepository.Object);

            // Create the controller with the service
            _controller = new ActivelyReadingController(_mockService);
        }

        [Test]
        public async Task GetAll_ReturnsOkResult_WithListOfActivelyReading()
        {
            // Arrange
            var expectedData = new List<ActivelyReading>
            {
                new ActivelyReading { Id = 1, path = "c:/path/test.pdf", userId = "user@example.com", dateUploaded = new DateTime(), timestamp = 0 },
                new ActivelyReading { Id = 2, path = "c:/path/test.pdf", userId = "user@example.com", dateUploaded = new DateTime(), timestamp = 0 }
            };

            // Setup mock repository to return the expected data
            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(expectedData);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = result as IEnumerable<ActivelyReading>;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(2, okResult?.Count());
        }

        [Test]
        public async Task GetById_ReturnsOkResult_WithActivelyReading()
        {
            // Arrange
            var expectedReading = new ActivelyReading { Id = 1, path = "c:/path/test.pdf", userId = "user@example.com", dateUploaded = new DateTime(), timestamp = 0 };

            // Setup mock repository to return the expected data
            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(expectedReading);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            Assert.IsInstanceOf<ActivelyReading>(result);
            Assert.AreEqual(expectedReading, result);
        }

        [Test]
        public async Task GetAllByUserId_ReturnsOkResult_WithListOfActivelyReading()
        {
            // Arrange
            var expectedData = new List<ActivelyReading>
            {
                new ActivelyReading { Id = 1, path = "c:/path/test.pdf", userId = "user@example.com", dateUploaded = new DateTime(), timestamp = 0 },
                new ActivelyReading { Id = 2, path = "c:/path/test.pdf", userId = "user@example.com", dateUploaded = new DateTime(), timestamp = 0 }
            };
            var userId = "user@example.com";

            // Setup mock repository to return the expected data
            _mockRepository.Setup(repo => repo.GetAllByUserIdAsync(userId)).ReturnsAsync(expectedData);

            // Simulate an authenticated user
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Email, userId)  // Mock the email claim
                    }, "mock"))
                }
            };

            // Act
            var result = await _controller.GetAllByUserId();

            // Assert
            var okResult = result as IEnumerable<ActivelyReading>;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(2, okResult?.Count());
        }

    [Test]
        public async Task Add_ReturnsOkResult_WhenDataIsValid()
        {
            // Arrange
            var newReading = new ActivelyReading { Id = 3, path = "c:/path/test.pdf", userId = "user@example.com", dateUploaded = new DateTime(), timestamp = 0 };

            // Act
            await _controller.Add(newReading);

            // Assert
            _mockRepository.Verify(repo => repo.AddAsync(It.IsAny<ActivelyReading>()), Times.Once);
        }

        [Test]
        public async Task Update_ReturnsOkResult_WhenDataIsValid()
        {
            // Arrange
            var updatedReading = new ActivelyReading { Id = 1, path = "c:/path/test.pdf", userId = "user@example.com", dateUploaded = new DateTime(), timestamp = 0 };

            // Act
            await _controller.Update(updatedReading);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<ActivelyReading>()), Times.Once);
        }

        [Test]
        public async Task Delete_ReturnsOkResult_WhenDataIsDeleted()
        {
            // Arrange
            var readingId = 1;

            // Act
            await _controller.Delete(readingId);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(It.IsAny<int>()), Times.Once);
        }
    }
}
