﻿using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Columns.Commands.DeleteColumn;

public class DeleteColumnCommand : IRequest
{
    public int Id { get; set; }
}

public class DeleteColumnCommandHandler : IRequestHandler<DeleteColumnCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteColumnCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteColumnCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Columns
            .Where(t => t.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Column), request.Id);
        }

        _context.Columns.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}