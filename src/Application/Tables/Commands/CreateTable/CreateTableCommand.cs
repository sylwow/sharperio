using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Tables.Commands.CreateTable;

public class CreateTableCommand : IRequest<Guid>
{
    public string Title { get; set; }
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
        if (_currentUserService.UserId is null)
        {
            throw new InvalidOperationException();
        }

        var userTables = await _context.UserTables
            .Where(u => u.ExternalId == _currentUserService.UserId)
            .Include(u => u.Tables)
            .FirstOrDefaultAsync(cancellationToken);

        var entity = new Table
        {
            OwnerId = _currentUserService.UserId,
            Title = request.Title,
        };

        userTables?.Tables.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
