using SharperioBackend.Application.Common.Interfaces;
using SharperioBackend.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SharperioBackend.Application.Items.Commands.UpdateItemOrder;

public class UpdateItemOrderCommand : IRequest
{
    public int Id { get; set; }
    public int Index { get; set; }
    public int previousColumnId { get; set; }
    public int newColumnId { get; set; }
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
            .Where(c => (c.Id == request.newColumnId || c.Id == request.previousColumnId)&&
                (c.Table.OwnerId == _currentUserService.UserId ||
                c.Table.Accesses.Any(a => a.UserId == _currentUserService.UserId)))
            .Where(c => !c.IsArhived)
            .Include(c => c.Items.Where(i => !i.IsArhived).OrderBy(i => i.Order))
            .ToList();

        if(columns.Count == 1)
        {
            var column = columns.First();
            var item = column.Items.FirstOrDefault(c => c.Id == request.Id);
            if (item is null)
            {
                return Unit.Value;
            }
            column.Items.Remove(item);
            column.Items.Insert(request.Index, item);
        }
        else if (columns.Count == 2)
        {
            var prevColumn = columns.First(c => c.Id == request.previousColumnId);
            var currColumn = columns.First(c => c.Id == request.newColumnId);

            var item = prevColumn.Items.FirstOrDefault(c => c.Id == request.Id);
            if (item is null)
            {
                return Unit.Value;
            }
            prevColumn.Items.Remove(item);
            currColumn.Items.Insert(request.Index, item);
        }
        else
        {
            return Unit.Value;
        }

        foreach (var col in columns)
        {
            var order = 1;
            foreach (var item in col.Items)
            {
                item.Order = order++;
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
