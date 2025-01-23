using System.Text.RegularExpressions;

namespace BitTest.Models;

public static class CsvRecordValidator
{
    private const string ValidPhoneRegex = @"^\+?[1-9]\d{1,14}$|^\d{3}-\d{3}-\d{4}$";

    public static string ValidateField(string columnName, string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return $"{columnName} cannot be empty.";
        }

        return columnName switch
        {
            "Name" => string.Empty, 

            "DateOfBirth" => DateTime.TryParse(value, out _) ? string.Empty : "Invalid date format. Please use a valid date.",

            "Married" => value.Equals("true", StringComparison.OrdinalIgnoreCase) ||
                         value.Equals("false", StringComparison.OrdinalIgnoreCase)
                            ? string.Empty
                            : "Invalid value for Married. Use 'true' or 'false'.",

            "Phone" => Regex.IsMatch(value, ValidPhoneRegex)
                        ? string.Empty
                        : "Invalid phone number format. Please use a valid format, e.g., +1234567890 or 123-456-7890.",

            "Salary" => decimal.TryParse(value, out var salary) && salary >= 0
                            ? string.Empty
                            : "Invalid salary. Please enter a positive number.",

            _ => "Invalid column name."
        };
    }
}
