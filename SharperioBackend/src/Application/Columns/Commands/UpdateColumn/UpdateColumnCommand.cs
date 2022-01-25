using SharperioBackend.Application.Common.Exceptions;
using SharperioBackend.Application.Common.Interfaces;
using SharperioBackend.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SharperioBackend.Application.Columns.Commands.UpdateColumn;

public class UpdateColumnCommand : IRequest
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public bool? IsArhived { get; set; }
    public int? Order { get; set; }
}

public class UpdateColumnCommandHandler : IRequestHandler<UpdateColumnCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public UpdateColumnCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(UpdateColumnCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Columns
            .Where(c => c.Id == request.Id &&
                (c.Table.OwnerId == _currentUserService.UserId ||
                c.Table.Accesses.Any(a => a.UserId == _currentUserService.UserId)))
            .FirstOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Column), request.Id);
        }

        entity.Title = request.Title ?? entity.Title;
        entity.IsArhived = request.IsArhived ?? entity.IsArhived;
        entity.Order = request.Order ?? entity.Order;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
