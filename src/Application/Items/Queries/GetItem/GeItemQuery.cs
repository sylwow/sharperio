using AutoMapper;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Items.Queries.GetItem;

public class GetItemQuery : IRequest<ItemDto>
{
    public int Id { get; set; }
}

public class GetItemQueryHandler : IRequestHandler<GetItemQuery, ItemDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetItemQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<ItemDto> Handle(GetItemQuery request, CancellationToken cancellationToken)
    {
        var item = await _context.Items
            .Where(i => i.Id == request.Id && (i.Column.Table.OwnerId == _currentUserService.UserId || i.Column.Table.UsersWithAccess.Contains(_currentUserService.UserId)))
            .FirstOrDefaultAsync(cancellationToken);

        if(item is null)
        {
            throw new NotFoundException(nameof(Item), request.Id);
        }

        return _mapper.Map<ItemDto>(item);
    }
}
