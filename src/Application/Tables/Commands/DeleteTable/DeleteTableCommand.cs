using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Tables.Commands.DeleteTable;

public class DeleteTableCommand : IRequest
{
    public Guid Id { get; set; }
}

public class DeleteTableCommandHandler : IRequestHandler<DeleteTableCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteTableCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteTableCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Tables
            .Where(t => t.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Table), request.Id);
        }

        _context.Tables.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
