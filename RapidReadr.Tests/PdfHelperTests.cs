using RapidReadr.Server.Helpers;

namespace RapidReadr.Tests
{
    public class PdfHelperTests
    {
        public readonly PdfHelper pdfHelper = new PdfHelper();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void MapPdf_MapTextFromPdfToObject()
        {
            // Arrange

            // Act
            var result = pdfHelper.MapPDF("C:/SharedResources/PDFs/PdfToUpload.pdf");

            // Assert

            Assert.IsInstanceOf<string>(result);
        }
    }
}