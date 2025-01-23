namespace BitTest.Core.Models;

public class CsvRecord
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool Married { get; set; }
    public string Phone { get; set; }
    public decimal Salary { get; set; }

    public CsvRecord(int id, string name, DateTime dateOfBirth, bool married, string phone, decimal salary)
    {
        Id = id;
        Name = name;
        DateOfBirth = dateOfBirth;
        Married = married;
        Phone = phone;
        Salary = salary;
    } 

    public CsvRecord(string name, DateTime dateOfBirth, bool married, string phone, decimal salary)
    {
        Name = name;
        DateOfBirth = dateOfBirth;
        Married = married;
        Phone = phone;
        Salary = salary;
    }

    public CsvRecord() 
    {
        Name = string.Empty;
        Phone = string.Empty;
    }
}
