using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Columns.Commands.UpdateColumnOrder;

public class UpdateColumnOrderCommand : IRequest
{
    public int Id { get; set; }
    public int Order { get; set; }
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

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
