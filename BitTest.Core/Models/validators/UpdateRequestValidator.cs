using FluentValidation;

namespace BitTest.Models;

public class UpdateRequestValidator : AbstractValidator<UpdateRequest>
{
    public UpdateRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Invalid record ID.");

        RuleFor(x => x.Column)
            .NotEmpty().WithMessage("Column name is required.")
            .Must(BeAValidColumn).WithMessage("Invalid column name.");

        RuleFor(x => x.Value)
            .NotEmpty().WithMessage("Value cannot be empty.");
    }

    private bool BeAValidColumn(string columnName)
    {
        var validColumns = new[] { "Name", "DateOfBirth", "Married", "Phone", "Salary" };
        return validColumns.Contains(columnName);
    }
}


