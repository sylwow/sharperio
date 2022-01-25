using SharperioBackend.Application.Common.Exceptions;
using SharperioBackend.Application.Common.Interfaces;
using SharperioBackend.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SharperioBackend.Application.Items.Commands.CreateItem;

public class CreateItemCommand : IRequest<int>
{
    public int ColumnId { get; set; }
    public string Title { get; set; }
    public string Note { get; set; } = string.Empty;
}

public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreateItemCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<int> Handle(CreateItemCommand request, CancellationToken cancellationToken)
    {
        var column = _context.Columns
            .Where(c => c.Id == request.ColumnId &&
                (c.Table.OwnerId == _currentUserService.UserId ||
                c.Table.Accesses.Any(a => a.UserId == _currentUserService.UserId)))
            .Include(t => t.Items)
            .FirstOrDefault();

        if (column is null)
        {
            throw new NotFoundException(nameof(Column), request.ColumnId);
        }

        var entity = new Item
        {
            Title = request.Title,
            Note = request.Note,
            Order = column.Items.Count(),
        };

        column.Items.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
