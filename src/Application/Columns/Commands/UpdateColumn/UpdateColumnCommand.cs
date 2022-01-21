using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Columns.Commands.UpdateColumn;

public class UpdateColumnCommand : IRequest
{
    public int Id { get; set; }

    public string? Title { get; set; }
}

public class UpdateColumnCommandHandler : IRequestHandler<UpdateColumnCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateColumnCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateColumnCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Columns
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Column), request.Id);
        }

        entity.Title = request.Title;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
