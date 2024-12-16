using Moq;
using RapidReadr.Server.Controllers;
using RapidReadr.Server.Models;
using RapidReadr.Server.Service;
using RapidReadr.Server.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using RapidReadr.Server.Repository.Interfaces;
using Microsoft.Extensions.Configuration;

namespace RapidReadr.Tests
{
    [TestFixture]
    public class PdfControllerTests
    {
        private Mock<IActivelyReadingRepository> _mockRepository;
        private ActivelyReadingService _mockService;
        private Mock<PdfHelper> _mockPdfHelper;
        private PdfController _controller;

        private string solutionDirectory;

        [SetUp]
        public void Setup()
        {
            // Mock the repository and helper
            _mockRepository = new Mock<IActivelyReadingRepository>();
            _mockService = new ActivelyReadingService(_mockRepository.Object);
            _mockPdfHelper = new Mock<PdfHelper>();

            // test directory path
            solutionDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
            string testDirectory = Path.Combine(solutionDirectory, "testPdfs");

            // Mock the configuration for the PDF directory
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(config => config["FileStorage:BasePath"]).Returns(testDirectory);

            // Instantiate the controller
            _controller = new PdfController(mockConfiguration.Object, _mockService, _mockPdfHelper.Object);   
        }

        [TearDown]
        public void TearDown()
        {
            string testFilePath = Path.Combine(solutionDirectory, "testPdfs", "test.pdf");

            if (File.Exists(testFilePath))
            {
                try
                {
                    File.Delete(testFilePath);
                }
                catch (IOException ex)
                {
                    TestContext.WriteLine($"Failed to delete file: {ex.Message}");
                }
            }
        }

        [Test]
        public async Task GetPdfText_ReturnsTextOfPdf()
        {
            // Arrange
            string pdfPath = Path.Combine(solutionDirectory, "testPdfs", "PdfToUpload.pdf");

            var activelyReading = new ActivelyReading { Id = 1, path = pdfPath, userId = "user@example.com" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(activelyReading);

            // Act
            var result = await _controller.GetPdfText(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ActionResult<string>>(result);
            Assert.That(result.Value.Length, Is.EqualTo(4408));
        }

        [Test]
        public async Task UploadPdf_ReturnsOk_WithExpectedMessage()
        {
            // Arrange
            var userId = "user@example.com";
            var fileMock = new Mock<IFormFile>();
            var content = "Fake PDF content";
            var fileName = "test.pdf";

            // Simulate a PDF file
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;

            fileMock.Setup(f => f.OpenReadStream()).Returns(ms);
            fileMock.Setup(f => f.FileName).Returns(fileName);
            fileMock.Setup(f => f.Length).Returns(ms.Length);

            // Simulate the user identity
            var claims = new List<Claim> { new Claim(ClaimTypes.Email, userId) };
            var identity = new ClaimsIdentity(claims);
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = claimsPrincipal }
            };

            // Act
            var result = await _controller.UploadPdf(fileMock.Object);

            // Assert
            Assert.IsNotNull(result, "Result should not be null.");

            // Cast result to OkObjectResult and extract the Value
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult, "Result should be an OkObjectResult.");

            var responseValue = okResult.Value;
            Assert.IsNotNull(responseValue, "OkObjectResult.Value should not be null.");

            // Dynamically check the properties of the anonymous object
            var responseType = responseValue.GetType();
            var messageProperty = responseType.GetProperty("message");
            var fileNameProperty = responseType.GetProperty("fileName");

            Assert.IsNotNull(messageProperty, "Response should contain a 'message' property.");
            Assert.IsNotNull(fileNameProperty, "Response should contain a 'fileName' property.");

            Assert.That(messageProperty.GetValue(responseValue), Is.EqualTo("PDF uploaded successfully."));
            Assert.That(fileNameProperty.GetValue(responseValue), Is.EqualTo(fileName));
        }

        [Test]
        public async Task UploadPdf_ReturnsError_WhenFileIsEmpty()
        {
            // Arrange
            var emptyFileMock = new Mock<IFormFile>();
            emptyFileMock.Setup(f => f.Length).Returns(0);
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, "user@example.com") }));
            _controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = claimsPrincipal } };

            // Act
            var result = await _controller.UploadPdf(emptyFileMock.Object);

            // Assert
            var badResult = result as BadRequestObjectResult;

            Assert.IsNotNull(badResult, "Result should be a BadRequestObjectResult.");
            Assert.That(badResult.Value, Is.EqualTo("Invalid PDF file."), "Error message does not match expected value.");
        }


    }
}
