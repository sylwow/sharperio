using SharperioBackend.Application.Common.Exceptions;
using SharperioBackend.Application.Common.Interfaces;
using SharperioBackend.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SharperioBackend.Application.Tables.Commands.UpdateTable;

public class UpdateTableCommand : IRequest
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public bool? IsPrivate { get; set; }
    public bool? IsArhived { get; set; }
}

public class UpdateTableCommandHandler : IRequestHandler<UpdateTableCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public UpdateTableCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(UpdateTableCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Tables
            .Where(t => t.Id == request.Id &&
                (t.OwnerId == _currentUserService.UserId ||
                t.Accesses.Any(a => a.UserId == _currentUserService.UserId)))
            .FirstOrDefaultAsync();

        if (entity == null)
        {
            throw new NotFoundException(nameof(Table), request.Id);
        }

        entity.Title = request.Title ?? entity.Title;
        entity.IsPrivate = request.IsPrivate ?? entity.IsPrivate;
        entity.IsArhived = request.IsArhived ?? entity.IsArhived;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
