using SharperioBackend.Application.Common.Exceptions;
using SharperioBackend.Application.Common.Interfaces;
using SharperioBackend.Domain.Entities;
using MediatR;

namespace SharperioBackend.Application.Columns.Commands.UpdateItem;

public class UpdateItemCommand : IRequest
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Note { get; set; }
    public int? Order { get; set; }
    public bool? IsArhived { get; set; }
}

public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public UpdateItemCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Items
                    .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Item), request.Id);
        }

        entity.Title = request.Title ?? entity.Title;
        entity.Note = request.Note ?? entity.Note;
        entity.Order = request.Order ?? entity.Order;
        entity.IsArhived = request.IsArhived ?? entity.IsArhived;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
