using CleanArchitecture.Application.Tables.Commands.CreateTable;
using FluentValidation;

namespace CleanArchitecture.Application.Items.Commands.CreateItem;

public class CreateItemCommandValidator : AbstractValidator<CreateItemCommand>
{
    public CreateItemCommandValidator()
    {
        RuleFor(v => v.Title)
            .NotNull()
            .MaximumLength(200)
            .NotEmpty();

        RuleFor(v => v.Title)
            .NotNull()
            .MaximumLength(500);
    }
}
