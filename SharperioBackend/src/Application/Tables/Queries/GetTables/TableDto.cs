using SharperioBackend.Application.Common.Mappings;
using SharperioBackend.Domain.Entities;

namespace SharperioBackend.Application.Tables.Queries.GetTables;

public class TableDto : IMapFrom<Table>
{
    public Guid Id { get; set; }
    public string OwnerId { get; set; }
    public string Title { get; set; }
    public Cover? Cover { get; set; }
    public bool IsPrivate { get; set; }
}
