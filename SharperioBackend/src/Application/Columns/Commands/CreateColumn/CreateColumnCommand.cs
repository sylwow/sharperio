using SharperioBackend.Application.Common.Exceptions;
using SharperioBackend.Application.Common.Interfaces;
using SharperioBackend.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SharperioBackend.Application.Columns.Commands.CreateColumn;

public class CreateColumnCommand : IRequest<int>
{
    public Guid TableId { get; set; }
    public string Title { get; set; }
}

public class CreateColumnCommandHandler : IRequestHandler<CreateColumnCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreateColumnCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<int> Handle(CreateColumnCommand request, CancellationToken cancellationToken)
    {
        var table = _context.Tables
            .Where(t => t.Id == request.TableId && (t.OwnerId == _currentUserService.UserId || t.UsersWithAccess.Contains(_currentUserService.UserId)))
            .Include(t => t.Columns)
            .FirstOrDefault();

        if (table is null)
        {
            throw new NotFoundException(nameof(Table), request.TableId);
        }

        var entity = new Column
        {
            Title = request.Title,
            Order = table.Columns.Count(),
        };

        table.Columns.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
