using SharperioBackend.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace SharperioBackend.Application.Columns.Commands.UpdateColumn;

public class UpdateColumnCommandValidator : AbstractValidator<UpdateColumnCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateColumnCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        When(v => v.Title is not null, () => {

            RuleFor(v => v.Title)
                .NotNull()
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");
            //.MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");

        });
    }

    /*public async Task<bool> BeUniqueTitle(UpdateColumnCommand model, string title, CancellationToken cancellationToken)
    {
        return await _context.Columns
            .Where(l => l.Id != model.Id)
            .AllAsync(l => l.Title != title, cancellationToken);
    }*/
}
