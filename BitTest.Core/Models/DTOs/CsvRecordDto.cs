﻿namespace BitTest.Core.Models;

public record CsvRecordDto
{
    public string Name { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public bool Married { get; set; }
    public string Phone { get; set; } = string.Empty;
    public decimal Salary { get; set; }
}
