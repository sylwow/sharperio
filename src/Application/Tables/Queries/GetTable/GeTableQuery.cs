using AutoMapper;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Tables.Queries.GetTable;

public class GetTableQuery : IRequest<TableDto?>
{
    public Guid TableId { get; set; }
}

public class GetTableQueryHandler : IRequestHandler<GetTableQuery, TableDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetTableQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<TableDto?> Handle(GetTableQuery request, CancellationToken cancellationToken)
    {
        var tables = await _context.Tables
            .Where(t => t.Id == request.TableId && (t.OwnerId == _currentUserService.UserId || t.UsersWithAccess.Contains(_currentUserService.UserId)))
            .Include(t => t.Columns.OrderBy(c => c.Order))
            .ThenInclude(c => c.Items.OrderBy(c => c.Order))
            .FirstOrDefaultAsync(cancellationToken);

        return _mapper.Map<TableDto?>(tables);
    }
}
