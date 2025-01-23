using BitTest.Core.Models;

namespace BitTest.ViewModels;

public class TableViewModel
{
    public IEnumerable<CsvRecord> records { get; set; } = new List<CsvRecord>();
}
