using Microsoft.AspNetCore.Mvc;
using BitTest.Models;
using BitTest.Services;
using BitTest.Data;

namespace BitTest.Controllers;

public class FileUploadController : Controller
{
    private readonly CsvValidationService _csvValidationService;
    private readonly CsvLoader _csvLoader;

    public FileUploadController(CsvValidationService csvValidationService, CsvLoader csvLoader)
    {
        _csvValidationService = csvValidationService;
        _csvLoader = csvLoader;
    }

    [HttpGet]
    public IActionResult Upload()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        var validationError = _csvValidationService.ValidateCsvFile(file, typeof(CsvRecordDto));
        if (!string.IsNullOrEmpty(validationError))
        {
            ViewBag.ErrorMessage = validationError;
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
                file.CopyTo(stream);
            }

            ViewBag.SuccessMessage = "File uploaded successfully!";
            await _csvLoader.LoadCsvToDatabaseAsync(filePath);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = $"An error occurred while uploading the file: {ex.Message}";
        }

        return View();
    }
}
