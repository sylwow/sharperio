using SharperioBackend.Application.Common.Mappings;
using SharperioBackend.Domain.Entities;

namespace SharperioBackend.Application.Columns.Queries.GetColumn;

public class ColumnDto : IMapFrom<Column>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsArhived { get; set; }
    public int Order { get; set; }
    public List<ItemDto> Items { get; set; } = new();
}
