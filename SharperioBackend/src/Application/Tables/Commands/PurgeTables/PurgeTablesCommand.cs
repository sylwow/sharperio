using SharperioBackend.Application.Common.Interfaces;
using SharperioBackend.Application.Common.Security;
using MediatR;

namespace SharperioBackend.Application.Tables.Commands.PurgeTables;

[Authorize(Roles = "Administrator")]
[Authorize(Policy = "CanPurge")]
public class PurgeTablesCommand : IRequest
{
}

public class PurgeTablesCommandHandler : IRequestHandler<PurgeTablesCommand>
{
    private readonly IApplicationDbContext _context;

    public PurgeTablesCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(PurgeTablesCommand request, CancellationToken cancellationToken)
    {
        _context.Tables.RemoveRange(_context.Tables);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
