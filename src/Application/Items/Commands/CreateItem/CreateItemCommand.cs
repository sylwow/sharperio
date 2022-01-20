using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Items.Commands.CreateItem;

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
            .Where(t => t.Id == request.ColumnId && (t.Table.OwnerId == _currentUserService.UserId || t.Table.UsersWithAccess.Contains(_currentUserService.UserId)))
            .Include(t => t.Items)
            .FirstOrDefault();

        if (column is null)
        {
            throw new NotFoundException(nameof(Column), request.ColumnId);
        }

        var entity = new Item
        {
            Title = request.Title,
            Order = column.Items.Count(),
        };

        column.Items.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
