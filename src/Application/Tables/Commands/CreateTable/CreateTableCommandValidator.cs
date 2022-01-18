﻿using FluentValidation;

namespace CleanArchitecture.Application.Tables.Commands.CreateTable;

public class CreateTableCommandValidator : AbstractValidator<CreateTableCommand>
{
    public CreateTableCommandValidator()
    {
        RuleFor(v => v.Title)
            .NotNull()
            .MaximumLength(200)
            .NotEmpty();
    }
}
