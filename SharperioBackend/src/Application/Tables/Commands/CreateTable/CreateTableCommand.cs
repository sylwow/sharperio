using SharperioBackend.Application.Common.Exceptions;
using SharperioBackend.Application.Common.Interfaces;
using SharperioBackend.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SharperioBackend.Application.Tables.Commands.CreateTable;

public class CreateTableCommand : IRequest<Guid>
{
    public string Title { get; set; }
    public Guid? WorkspaceId { get; set; }
}

public class CreateTableCommandHandler : IRequestHandler<CreateTableCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreateTableCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(CreateTableCommand request, CancellationToken cancellationToken)
    {
        var access = await _context.Accesses
            .Where(a => a.UserId == _currentUserService.UserId)
            .FirstOrDefaultAsync();

        if (access is null)
        {
            access = new Access { UserId = _currentUserService.UserId };
            _context.Accesses.Add(access);
        }

        var entity = new Table
        {
            OwnerId = _currentUserService.UserId,
            Title = request.Title,
        };

        access.Tables.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
