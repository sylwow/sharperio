using FluentValidation;

namespace SharperioBackend.Application.Items.Commands.UpdateItemOrder;

public class UpdateItemOrderCommandValidator : AbstractValidator<UpdateItemOrderCommand>
{
    public UpdateItemOrderCommandValidator()
    {
    }
}
