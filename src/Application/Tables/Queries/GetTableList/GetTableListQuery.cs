using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Tables.Queries.GetTableList;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Tables.Queries.GetTableList;

public class GetTableListQuery : IRequest<TableDtoList?>
{
}

public class GetTableListQueryHandler : IRequestHandler<GetTableListQuery, TableDtoList?>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetTableListQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<TableDtoList?> Handle(GetTableListQuery request, CancellationToken cancellationToken)
    {
        var tables =  await _context.Tables
            .Where(u => u.OwnerId == _currentUserService.UserId || u.UsersWithAccess.Contains(_currentUserService.UserId))
            .ToListAsync(cancellationToken);

        return _mapper.Map<TableDtoList?>(tables);
    }
}
