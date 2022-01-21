using FluentValidation;

namespace CleanArchitecture.Application.Columns.Commands.UpdateItem;

public class UpdateItemCommandValidator : AbstractValidator<UpdateItemCommand>
{
    public UpdateItemCommandValidator()
    {
    }
}
