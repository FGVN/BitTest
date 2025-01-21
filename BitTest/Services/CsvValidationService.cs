using System.Reflection;

namespace BitTest.Services;

public class CsvValidationService
{
    public string ValidateCsvFile(IFormFile file, Type modelType)
    {
        if (file == null || file.Length == 0)
        {
            return "Please select a valid file.";
        }

        if (!file.FileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
        {
            return "Only CSV files are allowed.";
        }

        try
        {
            using (var stream = file.OpenReadStream())
            using (var reader = new StreamReader(stream))
            {
                var headerLine = reader.ReadLine();
                if (headerLine == null)
                {
                    return "The file is empty.";
                }

                var headers = headerLine.Split(',').Select(h => h.Trim()).Select(line => line.Replace(" ", "")).ToList();

                var requiredFields = modelType
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Select(p => p.Name)
                    .ToList();

                var missingFields = requiredFields.Except(headers, StringComparer.OrdinalIgnoreCase).ToList();
                if (missingFields.Any())
                {
                    return $"The following fields are missing in the CSV: {string.Join(", ", missingFields)}";
                }
            }
        }
        catch (Exception ex)
        {
            return $"An error occurred while validating the file: {ex.Message}";
        }

        return null; 
    }
}
