using BitTest.Models;
using CsvHelper;
using System.Globalization;

namespace BitTest.Data;

public class CsvLoader
{
    private readonly CsvDbContext _context;

    public CsvLoader(CsvDbContext context)
    {
        _context = context;
    }
    public async Task LoadCsvToDatabaseAsync(string filePath)
    {
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        csv.Context.RegisterClassMap<CsvRecordDtoMap>();

        var dtoRecords = csv.GetRecords<CsvRecordDto>().ToList();
        var entities = new List<CsvRecord>();

        foreach (var dto in dtoRecords)
        {
            var validationErrors = new List<string>
        {
            CsvRecordValidator.ValidateField("Name", dto.Name),
            CsvRecordValidator.ValidateField("DateOfBirth", dto.DateOfBirth.ToString()),
            CsvRecordValidator.ValidateField("Married", dto.Married.ToString()),
            CsvRecordValidator.ValidateField("Phone", dto.Phone),
            CsvRecordValidator.ValidateField("Salary", dto.Salary.ToString())
        }.Where(error => !string.IsNullOrEmpty(error)).ToList();

            if (validationErrors.Any())
            {
                throw new Exception($"Validation failed for record: {string.Join(", ", validationErrors)}");
            }

            entities.Add(new CsvRecord(dto.Name, dto.DateOfBirth, dto.Married, dto.Phone, dto.Salary));
        }

        await _context.CsvRecords.AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }
}
