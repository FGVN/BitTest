using Microsoft.AspNetCore.Mvc;
using BitTest.Core.Validators;
using BitTest.Persistance.Data;

namespace BitTest.Controllers;

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
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        var validationResult = _uploadValidator.Validate(file);
        if (!validationResult.IsValid)
        {
            TempData["ErrorMessage"] = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            return RedirectToAction(nameof(Index));
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

            TempData["SuccessMessage"] = "File uploaded successfully!";
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"An error occurred while uploading the file: {ex.Message}";
        }

        return RedirectToAction(nameof(Index));
    }

}
