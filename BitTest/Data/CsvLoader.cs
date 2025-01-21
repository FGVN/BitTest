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

        var entities = dtoRecords.Select(dto => new CsvRecord
        {
            Name = dto.Name,
            DateOfBirth = dto.DateOfBirth,
            Married = dto.Married,
            Phone = dto.Phone,
            Salary = dto.Salary
        }).ToList();

        await _context.CsvRecords.AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }
}
