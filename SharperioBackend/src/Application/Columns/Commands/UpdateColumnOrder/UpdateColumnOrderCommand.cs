using SharperioBackend.Application.Common.Interfaces;
using SharperioBackend.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SharperioBackend.Application.Columns.Commands.UpdateColumnOrder;

public class UpdateColumnOrderCommand : IRequest
{
    public int Id { get; set; }
    public int Index { get; set; }
}

public class UpdateColumnOrderCommandHandler : IRequestHandler<UpdateColumnOrderCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public UpdateColumnOrderCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(UpdateColumnOrderCommand request, CancellationToken cancellationToken)
    {
        var columns = _context.Columns
            .Where(c => c.Id == request.Id &&
                (c.Table.OwnerId == _currentUserService.UserId ||
                c.Table.Accesses.Any(a => a.UserId == _currentUserService.UserId)))
            .SelectMany(c => c.Table.Columns)
            .Where(c => !c.IsArhived)
            .OrderBy(c => c.Order)
            .ToList();

        if(columns.Count <= 1)
        {
            return Unit.Value;
        }

        var column = columns.FirstOrDefault(c => c.Id == request.Id);
        if (column is null)
        {
            return Unit.Value;
        }
        columns.Remove(column);
        columns.Insert(request.Index, column);

        var order = 1;
        foreach(var col in columns)
        {
            col.Order = order++;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
