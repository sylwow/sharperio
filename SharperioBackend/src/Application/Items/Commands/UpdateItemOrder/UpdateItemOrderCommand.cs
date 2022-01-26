using SharperioBackend.Application.Common.Interfaces;
using SharperioBackend.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharperioBackend.Domain.Events;

namespace SharperioBackend.Application.Items.Commands.UpdateItemOrder;

public class UpdateItemOrderCommand : IRequest
{
    public int Id { get; set; }
    public int Index { get; set; }
    public int PreviousColumnId { get; set; }
    public int NewColumnId { get; set; }
}

public class UpdateItemOrderCommandHandler : IRequestHandler<UpdateItemOrderCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public UpdateItemOrderCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(UpdateItemOrderCommand request, CancellationToken cancellationToken)
    {
        var columns = _context.Columns
            .Where(c => (c.Id == request.NewColumnId || c.Id == request.PreviousColumnId)&&
                (c.Table.OwnerId == _currentUserService.UserId ||
                c.Table.Accesses.Any(a => a.UserId == _currentUserService.UserId)))
            .Where(c => !c.IsArhived)
            .Include(c => c.Table)
            .Include(c => c.Items.Where(i => !i.IsArhived).OrderBy(i => i.Order))
            .ToList();

        if (columns.Count != 2 && columns.Count != 1)
        {
            return Unit.Value;
        }
        var prevColumn = columns.First(c => c.Id == request.PreviousColumnId);
        var currColumn = columns.First(c => c.Id == request.NewColumnId);

        var item = prevColumn.Items.FirstOrDefault(c => c.Id == request.Id);
        if (item is null)
        {
            return Unit.Value;
        }
        prevColumn.Items.Remove(item);
        currColumn.Items.Insert(request.Index, item);

        foreach (var col in columns)
        {
            var order = 1;
            foreach (var it in col.Items)
            {
                it.Order = order++;
            }
        }

        item.DomainEvents.Add(new ItemOrderChangedEvent
        {
            TableId = currColumn.Table.Id,
            ItemId = request.Id,
            Index = request.Index,
            NewColumnId = currColumn.Id,
            PreviousColumnId = prevColumn.Id,
        });

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
