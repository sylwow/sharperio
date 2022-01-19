﻿using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Tables.Queries.GetTable;

public class TableDto : IMapFrom<Table>
{
    public Guid Id { get; set; }
    public string OwnerId { get; set; }
    public string Title { get; set; }
    public Cover? Cover { get; set; }
    public bool IsPrivate { get; set; }
    public List<ColumnDto> Columns { get; set; } = new();
    public List<string> UsersWithAccess { get; set; } = new();
}
