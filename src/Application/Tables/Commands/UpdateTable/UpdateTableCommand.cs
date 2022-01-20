using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Tables.Commands.UpdateTable;

public class UpdateTableCommand : IRequest
{
    public Guid Id { get; set; }

    public string? Title { get; set; }
}

public class UpdateTableCommandHandler : IRequestHandler<UpdateTableCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTableCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateTableCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Tables
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Table), request.Id);
        }

        entity.Title = request.Title;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
