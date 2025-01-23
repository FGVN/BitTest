namespace BitTest.Models;

public class CsvRecordDto
{
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool Married { get; set; }
    public string Phone { get; set; }
    public decimal Salary { get; set; }

    public CsvRecordDto(string name, DateTime dateOfBirth, bool married, string phone, decimal salary)
    {
        Name = name;
        DateOfBirth = dateOfBirth;
        Married = married;
        Phone = phone;
        Salary = salary;
    }
}
