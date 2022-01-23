using SharperioBackend.Application.Common.Exceptions;
using SharperioBackend.Application.Common.Interfaces;
using SharperioBackend.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SharperioBackend.Application.Tables.Commands.DeleteTable;

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
