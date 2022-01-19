using CleanArchitecture.Application.Tables.Commands.CreateTable;
using FluentValidation;

namespace CleanArchitecture.Application.Columns.Commands.CreateColumn;

public class CreateColumnCommandValidator : AbstractValidator<CreateTableCommand>
{
    public CreateColumnCommandValidator()
    {
        RuleFor(v => v.Title)
            .NotNull()
            .MaximumLength(200)
            .NotEmpty();
    }
}
