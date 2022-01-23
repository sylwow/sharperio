using AutoMapper;
using SharperioBackend.Application.Common.Exceptions;
using SharperioBackend.Application.Common.Interfaces;
using SharperioBackend.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SharperioBackend.Application.Tables.Queries.GetTables;

public class GetTablesQuery : IRequest<TablesDto>
{
}

public class GetTablesQueryHandler : IRequestHandler<GetTablesQuery, TablesDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetTablesQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<TablesDto> Handle(GetTablesQuery request, CancellationToken cancellationToken)
    {
        var tables =  await _context.Tables
            .Where(u => u.OwnerId == _currentUserService.UserId || u.UsersWithAccess.Contains(_currentUserService.UserId))
            .ToListAsync(cancellationToken);

        if (tables is null)
        {
            throw new NotFoundException(nameof(Table), _currentUserService.UserId);
        }

        return _mapper.Map<TablesDto>(tables);
    }
}
