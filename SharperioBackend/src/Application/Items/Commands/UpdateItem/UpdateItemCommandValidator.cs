using FluentValidation;

namespace SharperioBackend.Application.Columns.Commands.UpdateItem;

public class UpdateItemCommandValidator : AbstractValidator<UpdateItemCommand>
{
    public UpdateItemCommandValidator()
    {
        When(v => v.Title is not null, () =>
        {

            RuleFor(v => v.Title)
            .NotNull()
            .MaximumLength(200)
            .NotEmpty();
        });

        When(v => v.Note is not null, () => { 

            RuleFor(v => v.Note)
                .NotNull()
                .MaximumLength(500);
        });
    }
}
