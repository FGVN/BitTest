using BitTest.Data;
using Microsoft.AspNetCore.Mvc;

namespace BitTest.Controllers;

public class CsvController : Controller
{
    private readonly CsvDbContext _context;

    public CsvController(CsvDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var records = _context.CsvRecords.ToList(); 
        return View(records);
    }


}
