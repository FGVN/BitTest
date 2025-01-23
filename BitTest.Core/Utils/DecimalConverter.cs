using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace BitTest.Core.Utils;

public class DecimalConverter : DefaultTypeConverter
{
    public override object ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrWhiteSpace(text))
            return 0m;

        text = text.Replace("$", "").Trim(); 
        if (decimal.TryParse(text, out var result))
            return result;

        throw new Exception($"Invalid decimal value: '{text}'");
    }
}

