using SharperioBackend.Application.Common.Exceptions;
using SharperioBackend.Application.Common.Interfaces;
using SharperioBackend.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SharperioBackend.Application.Columns.Commands.DeleteColumn;

public class DeleteColumnCommand : IRequest
{
    public int Id { get; set; }
}

public class DeleteColumnCommandHandler : IRequestHandler<DeleteColumnCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public DeleteColumnCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(DeleteColumnCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Columns
            .Where(c => c.Id == request.Id &&
                (c.Table.OwnerId == _currentUserService.UserId ||
                c.Table.Accesses.Any(a => a.UserId == _currentUserService.UserId)))
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
