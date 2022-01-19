using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Tables.Queries.GetUserTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Tables.Queries.GetUserTables;

public class GetUserTablesQuery : IRequest<List<UserTableDto>?>
{
}

public class GetUserTablesQueryHandler : IRequestHandler<GetUserTablesQuery, List<UserTableDto>?>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetUserTablesQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<List<UserTableDto>?> Handle(GetUserTablesQuery request, CancellationToken cancellationToken)
    {
        var tables =  await _context.Tables
            .Where(u => u.OwnerId == _currentUserService.UserId || u.UsersWithAccess.Contains(_currentUserService.UserId))
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<UserTableDto>>(tables);
    }
}
