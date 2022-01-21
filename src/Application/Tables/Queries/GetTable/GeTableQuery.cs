using AutoMapper;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Tables.Queries.GetTable;

public class GetTableQuery : IRequest<TableDto>
{
    public Guid Id { get; set; }
}

public class GetTableQueryHandler : IRequestHandler<GetTableQuery, TableDto>
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

    public async Task<TableDto> Handle(GetTableQuery request, CancellationToken cancellationToken)
    {
        var table = await _context.Tables
            .Where(t => t.Id == request.Id && (t.OwnerId == _currentUserService.UserId || t.UsersWithAccess.Contains(_currentUserService.UserId)))
            .Include(t => t.Columns.OrderBy(c => c.Order))
            .ThenInclude(c => c.Items.OrderBy(c => c.Order))
            .FirstOrDefaultAsync(cancellationToken);

        if (table is null)
        {
            throw new NotFoundException(nameof(Table), request.Id);
        }

        return _mapper.Map<TableDto>(table);
    }
}
