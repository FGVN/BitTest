using BitTest.Core.Models;
using BitTest.Core.Requests;
using BitTest.Core.Validators;
using BitTest.Persistance.Data;
using BitTest.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BitTest.Controllers;

public partial class TableController : Controller
{
    private readonly CsvDbContext _context;

    public TableController(CsvDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var records = _context.CsvRecords.ToList(); 
        return View(new TableViewModel { records = records});
    }

    [HttpPost]
    public IActionResult Update([FromBody] UpdateRequest request)
    {
        var record = _context.CsvRecords.FirstOrDefault(r => r.Id == request.Id);
        if (record == null)
        {
            return NotFound(new { Error = "Record not found." });
        }

        string errorMessage = CsvRecordValidator.ValidateField(request.Column, request.Value);
        if (!string.IsNullOrEmpty(errorMessage))
        {
            return BadRequest(new
            {
                Error = errorMessage,
                OriginalValue = GetOriginalValue(record, request.Column)
            });
        }

        switch (request.Column)
        {
            case "Name":
                record.Name = request.Value;
                break;
            case "DateOfBirth":
                record.DateOfBirth = DateTime.Parse(request.Value);
                break;
            case "Married":
                record.Married = request.Value.Equals("true", StringComparison.OrdinalIgnoreCase);
                break;
            case "Phone":
                record.Phone = request.Value;
                break;
            case "Salary":
                record.Salary = decimal.Parse(request.Value);
                break;
            default:
                return BadRequest(new { Error = "Invalid column name." });
        }

        _context.SaveChanges();
        return Ok(new { Success = true, UpdatedValue = request.Value });
    }


    [HttpPost]
    public IActionResult Delete([FromBody] DeleteRequest request)
    {
        var record = _context.CsvRecords.FirstOrDefault(r => r.Id == request.Id);
        if (record == null)
        {
            return NotFound(new { Error = "Record not found." });
        }

        _context.CsvRecords.Remove(record);
        _context.SaveChanges();
        return Ok(new { Success = true });
    }


    private object GetOriginalValue(CsvRecord record, string column)
    {
        return column switch
        {
            "Name" => record.Name,
            "DateOfBirth" => record.DateOfBirth.ToString("dd-MM-yyyy"),
            "Married" => record.Married ? "Yes" : "No",
            "Phone" => record.Phone,
            "Salary" => record.Salary.ToString("C"),
            _ => new object()
        };
    }

}
