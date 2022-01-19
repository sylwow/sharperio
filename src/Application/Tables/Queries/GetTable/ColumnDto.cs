using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Tables.Queries.GetTable;

public class ColumnDto : IMapFrom<Column>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsArhived { get; set; }
    public int Order { get; set; }
    public List<Item> Items { get; set; } = new();
}
