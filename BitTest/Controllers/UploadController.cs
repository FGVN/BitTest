using Microsoft.AspNetCore.Mvc;
using BitTest.Data;
using BitTest.Models;

namespace BitTest.Controllers
{
    public class UploadController : Controller
    {
        private readonly CsvLoader _csvLoader;
        private readonly UploadValidator _uploadValidator;

        public UploadController(CsvLoader csvLoader, UploadValidator uploadValidator)
        {
            _csvLoader = csvLoader;
            _uploadValidator = uploadValidator;
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var validationResult = _uploadValidator.Validate(file);
            if (!validationResult.IsValid)
            {
                ViewBag.ErrorMessage = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                return View();
            }

            try
            {
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                var filePath = Path.Combine(uploadPath, file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                await _csvLoader.LoadCsvToDatabaseAsync(filePath);

                ViewBag.SuccessMessage = "File uploaded successfully!";
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"An error occurred while uploading the file: {ex.Message}";
            }

            return View();
        }
    }
}
