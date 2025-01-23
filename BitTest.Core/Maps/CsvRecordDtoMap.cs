using BitTest.Core.Models;
using BitTest.Core.Utils;
using CsvHelper.Configuration;

namespace BitTest.Core.Maps;

public class CsvRecordDtoMap : ClassMap<CsvRecordDto>
{
    public CsvRecordDtoMap()
    {
        Map(m => m.Name).Name("Name");
        Map(m => m.DateOfBirth).Name("Date of birth");
        Map(m => m.Married).Name("Married");
        Map(m => m.Phone).Name("Phone");
        Map(m => m.Salary).Name("Salary").TypeConverter<DecimalConverter>();
    }
}

