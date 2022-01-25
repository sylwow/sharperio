using AutoMapper;
using SharperioBackend.Application.Common.Exceptions;
using SharperioBackend.Application.Common.Interfaces;
using SharperioBackend.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SharperioBackend.Application.Columns.Queries.GetColumn;

public class GetColumnQuery : IRequest<ColumnDto>
{
    public int Id { get; set; }
}

public class GetColumnQueryHandler : IRequestHandler<GetColumnQuery, ColumnDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetColumnQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<ColumnDto> Handle(GetColumnQuery request, CancellationToken cancellationToken)
    {
        var column = await _context.Columns
            .Where(c => c.Id == request.Id && 
                (c.Table.OwnerId == _currentUserService.UserId ||
                c.Table.Accesses.Any(a => a.UserId == _currentUserService.UserId)))
            .OrderBy(c => c.Order)
            .Include(c => c.Items.Where(i => !i.IsArhived).OrderBy(c => c.Order))
            .FirstOrDefaultAsync(cancellationToken);

        if(column is null)
        {
            throw new NotFoundException(nameof(Column), request.Id);
        }

        return _mapper.Map<ColumnDto>(column);
    }
}
