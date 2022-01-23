using SharperioBackend.Application.Tables.Commands.CreateTable;
using FluentValidation;

namespace SharperioBackend.Application.Items.Commands.CreateItem;

public class CreateItemCommandValidator : AbstractValidator<CreateItemCommand>
{
    public CreateItemCommandValidator()
    {
        RuleFor(v => v.Title)
            .NotNull()
            .MaximumLength(200)
            .NotEmpty();

        RuleFor(v => v.Note)
            .NotNull()
            .MaximumLength(500);
    }
}
