using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Columns.Queries.GetColumn;

public class ItemDto : IMapFrom<Item>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Note { get; set; }
    public Cover Cover { get; set; }
    public List<Label> Labels { get; set; } = new();
    public List<Comment> Comments { get; set; } = new();
}
