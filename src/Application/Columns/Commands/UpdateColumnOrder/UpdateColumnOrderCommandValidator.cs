using FluentValidation;

namespace CleanArchitecture.Application.Columns.Commands.UpdateColumnOrder;

public class UpdateColumnOrderCommandValidator : AbstractValidator<UpdateColumnOrderCommand>
{
    public UpdateColumnOrderCommandValidator()
    {
    }
}
