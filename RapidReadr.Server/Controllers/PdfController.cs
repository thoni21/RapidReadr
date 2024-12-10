using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RapidReadr.Server.Data;
using RapidReadr.Server.Models;
using System.Security.Claims;

namespace RapidReadr.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        // Implement cloud storage instead
        private readonly string _pdfDirectory;
        private readonly ApplicationDbContext _dbContext;

        public PdfController(IConfiguration configuration, ApplicationDbContext dbcontext)
        {
            if (configuration["FileStorage:BasePath"] is not string pdfDirectory) {
                throw new ArgumentNullException("Path to PDF directory cannot be null");
            }

            _pdfDirectory = pdfDirectory;
            _dbContext = dbcontext;

            if (!Directory.Exists(_pdfDirectory))
            {
                Directory.CreateDirectory(_pdfDirectory);
            }
        }

        [Authorize]
        [HttpPost("upload")]
        public async Task<IActionResult> UploadPdf(IFormFile file)
        {
            if (User.FindFirstValue(ClaimTypes.Email) is not string _userId)
            {
                return BadRequest("No user found.");
            }

            if (file == null || file.Length == 0 || Path.GetExtension(file.FileName).ToLower() != ".pdf")
            {
                return BadRequest("Invalid PDF file.");
            }

            // Extract the original file name and extension
            var originalFileName = Path.GetFileNameWithoutExtension(file.FileName);
            var fileExtension = Path.GetExtension(file.FileName);

            var filePath = Path.Combine(_pdfDirectory, file.FileName);
            int count = 1;

            // Check if the file already exists and add a number to the name if it does
            while (System.IO.File.Exists(filePath))
            {
                var newFileName = $"{originalFileName}({count}){fileExtension}";
                filePath = Path.Combine(_pdfDirectory, newFileName);
                count++;
            }

            // Save the file to the determined file path
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Add db entty for file upload
            var fileMetadata = new ActivelyReading
            {
                path = filePath,
                dateUploaded = DateTime.UtcNow,
                timestamp = 0,
                userId = _userId
            };

            _dbContext.ActivelyReadings.Add(fileMetadata);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "PDF uploaded successfully.", fileName = Path.GetFileName(filePath) });
        }
    }
}
