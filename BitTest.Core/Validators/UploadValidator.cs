using BitTest.Core.Models;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace BitTest.Core.Validators;

public class UploadValidator : AbstractValidator<IFormFile>
{
    public UploadValidator()
    {
        RuleFor(file => file)
            .NotNull().WithMessage("File is required.")
            .Must(file => file.ContentType == "text/csv").WithMessage("Only CSV files are allowed.")
            .Must(file => file.Length > 0).WithMessage("File cannot be empty.")
            .Must(file => file.FileName.EndsWith(".csv")).WithMessage("File must have a .csv extension.")
            .Must((file) => ValidateCsvHeaders(file)).WithMessage("CSV file has missing or incorrect headers.");
    }

    private bool ValidateCsvHeaders(IFormFile file)
    {
        try
        {
            using (var stream = file.OpenReadStream())
            using (var reader = new StreamReader(stream))
            {
                var headerLine = reader.ReadLine();
                if (headerLine == null)
                {
                    return false; 
                }

                var headers = headerLine.Split(',')
                    .Select(h => h.Trim())
                    .Select(line => line.Replace(" ", ""))
                    .ToList();

                var requiredFields = typeof(CsvRecordDto)
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Select(p => p.Name)
                    .ToList();

                var missingFields = requiredFields.Except(headers, StringComparer.OrdinalIgnoreCase).ToList();
                if (missingFields.Any())
                {
                    return false; 
                }
            }
        }
        catch
        {
            return false; 
        }

        return true; 
    }
}
