using SharperioBackend.Application.Tables.Commands.CreateTable;
using FluentValidation;

namespace SharperioBackend.Application.Columns.Commands.CreateColumn;

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
